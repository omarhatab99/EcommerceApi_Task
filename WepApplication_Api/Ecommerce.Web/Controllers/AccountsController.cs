using AutoMapper;
using Ecommerce.Domain.Const;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Web.Core.Dtos;
using Ecommerce.Web.Services.Interfaces;

namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<ApplicationUser> userManager , IUnitOfWork unitOfWork
            , IMapper mapper , IConfiguration configuration)
        {
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registeration(RegisterUserDTO userDTO)
        {
            //check modalstate
            if(ModelState.IsValid)
            {

                var result = await _unitOfWork.AuthRepository.RegisterAsync(userDTO);
                if(result.IsAuthenticated)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }

            }
            else
            {
                //Handle Response
                AuthModel response = new AuthModel();

                IEnumerable<string> mSErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                response.Messages.AddRange(mSErrors);

                return Ok(response);
            }

           
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            //check modalstate
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.AuthRepository.GetTokenAsync(userDTO);
                if(result.IsAuthenticated)
                {

                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            else
            {
                //Handle Response
                AuthModel response = new AuthModel();

                IEnumerable<string> mSErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                response.Messages.AddRange(mSErrors);

                return Ok(response);
            }


        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RevokeToken revokeToken)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _unitOfWork.AuthRepository.RefreshTokenAsync(revokeToken.Token!);

            if(result.IsAuthenticated)
            {

                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(token))
            {
                var result = await _unitOfWork.AuthRepository.RevokeTokenAsync(token);
                if(result)
                {
                    return Ok();
                }
                else
                {
                    return Ok("Token is invalid!");
                }
            }
            else
            {
                return Ok("Token is required!");
            }
                
            
        }

    }
}
