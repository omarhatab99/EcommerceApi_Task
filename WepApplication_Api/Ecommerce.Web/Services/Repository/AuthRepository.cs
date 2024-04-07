namespace Ecommerce.Web.Services.Repository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly JWT _jwt;

        public AuthRepository(UserManager<ApplicationUser> userManager , ApplicationDbContext applicationDbContext , IOptions<JWT> Jwt , IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._applicationDbContext = applicationDbContext;
            _jwt = Jwt.Value;
        }


        public async Task<AuthModel> RegisterAsync(RegisterUserDTO userDTO)
        {
            if (await _userManager.FindByEmailAsync(userDTO.Email) != null)
                return new AuthModel { Messages = new List<string> { "Email is already exsisted!" }};

            if (await _userManager.FindByNameAsync(userDTO.Username) != null)
                return new AuthModel { Messages = new List<string> { "Username is already exsisted!" } };

            //to sure that applicationUser stored in database with role 
            using var transaction = _applicationDbContext.Database.BeginTransaction();

            //mapping from RegisterUserDTO to User
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(userDTO);

            try
            {
                //create user in database using identity 
                IdentityResult userResult = await _userManager.CreateAsync(applicationUser, userDTO.Password);

                if (userResult.Succeeded) //user is already created successfully
                {
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(applicationUser, Roles.USER.ToString());
                    if (roleResult.Succeeded)
                    {
                        transaction.Commit();


                        //Create Token 
                        var jwtSecurityToken = await CreateJwtToken(applicationUser);

                        var refreshToken = GenerateRefreshToken();
                        applicationUser.RefreshTokens?.Add(refreshToken);
                        await _userManager.UpdateAsync(applicationUser);

                        //Handle Response
                        AuthModel response = new AuthModel()
                        {
                            Messages = new List<string> { "Account registered successfully" },
                            Email = applicationUser.Email!,
                            ExpiresOn = jwtSecurityToken.ValidTo,
                            IsAuthenticated = true,
                            Roles = new List<string> { "User" },
                            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            Username = applicationUser.UserName!,
                            RefreshToken = refreshToken.Token,
                            RefreshTokenExpiration = refreshToken.ExpirationOn
                        };


                        return response;
                    }
                    else
                    {
                        transaction.Rollback();

                        //Handle Response
                        AuthModel response = new AuthModel();

                        response.Messages.AddRange(roleResult.Errors.Select(x => x.Description));

                        return response;
                    }

                }
                else //something is wrong
                {
                    //Handle Response
                    AuthModel response = new AuthModel();

                    response.Messages.AddRange(userResult.Errors.Select(x => x.Description));

                    return response;
                }

            }
            catch (Exception ex)
            {

                transaction.Rollback();
                //Handle Response
                AuthModel response = new AuthModel();
                response.Messages.Add(ex.Message);
                return response;
            }
           
        }

        public async Task<AuthModel> GetTokenAsync(LoginUserDTO userDTO)
        {
            var authModel = new AuthModel();

            var applicationUser = await _userManager.FindByNameAsync(userDTO.Username);

            if (applicationUser == null || !await _userManager.CheckPasswordAsync(applicationUser, userDTO.Password))
            {
                authModel.Messages = new List<string> { "Email or Password is incorrect!" };
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(applicationUser);
            var rolesList = await _userManager.GetRolesAsync(applicationUser);

            authModel.Messages = new List<string> { "Account login successfully" };
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = applicationUser.Email!;
            authModel.Username = applicationUser.UserName!;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            if (applicationUser.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = applicationUser.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken!.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpirationOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpirationOn;
                applicationUser.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(applicationUser);
            }

            return authModel;
        }

        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser applicationUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);
            var userRoles = await _userManager.GetRolesAsync(applicationUser);
            var roleClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                roleClaims.Add(new Claim("Roles", role));
            }

            //create claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName!),
                new Claim(JwtRegisteredClaimNames.NameId, applicationUser.Id!),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            }
            .Union(userClaims)
            .Union(roleClaims).ToList();

            //create signingCredentials 
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //create token
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issur, //url web api
                audience: _jwt.Audiance, //url consumer angular 
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }

        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();

            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (applicationUser == null)
            {
                authModel.Messages = new List<string> { "Invalid token" };
                return authModel;
            }

            var refreshToken = applicationUser.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Messages = new List<string> { "Inactive token" };
                return authModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            applicationUser.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(applicationUser);

            var jwtSecurityToken = await CreateJwtToken(applicationUser);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Email = applicationUser.Email!;
            authModel.Username = applicationUser.UserName!;
            var roles = await _userManager.GetRolesAsync(applicationUser);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpirationOn;

            return authModel;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (applicationUser == null)
                return false;

            var refreshToken = applicationUser.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(applicationUser);

            return true;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpirationOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

	}
}
