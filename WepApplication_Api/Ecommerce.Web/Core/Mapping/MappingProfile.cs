namespace Ecommerce.Web.Core.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {

            //Product
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductFormDTO>().ReverseMap();

            //User
            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();

        }


    }
}
