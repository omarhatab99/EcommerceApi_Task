using AutoMapper;
using Ecommerce.Domain.Const;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Web.Core.Dtos;
using Ecommerce.Web.Services.Interfaces;
namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm]ProductFormDTO productDTO)
        {
            ApiResponse<ProductDTO> response = new ApiResponse<ProductDTO>();

            if (ModelState.IsValid) //check if modal state is valid
            {
                #region CheckImage
                //check if Form has Image
                if (Request.Form.Files.Count > 0)
                { //user send images

                    ResultFile result = await _unitOfWork.UploadImage.UploadFile(productDTO.File!, "products", true);
                    if (result.Successed)//successed
                    {
                        productDTO.Image = result.Url;
                        productDTO.Thumb = result.Thumb;
                    }
                    else
                    {
                        //Handle Response
                        response.Messages.Add(result.ErrorMessage);

                        return Ok(response);
                    }
                }
                #endregion

                //mapping from productDTO to Product
                Product product = _mapper.Map<Product>(productDTO);

                _unitOfWork.ProductRepository.Create(product);

                ProductDTO vmodel = _mapper.Map<ProductDTO>(product);

                //Handle Response
                response.Successed = true;
                response.Response = vmodel;

                //mapping from Product to ProductDTO

                return Ok(response);
            }
            else
            {
                //Handle Response

                IEnumerable<string> mSErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                response.Messages.AddRange(mSErrors);

                return Ok(response);
            }

        }

        [HttpPost("Update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromForm] ProductFormDTO productDTO)
        {

            Product product = _unitOfWork.ProductRepository.GetById(id);

            ApiResponse<ProductDTO> response = new ApiResponse<ProductDTO>();

            if (product != null)
            {


                if (ModelState.IsValid) //check if modal state is valid
                {

                    //check image found
                    #region CheckImage
                    if (Request.Form.Files.Count > 0)
                    {
                        ResultFile result = await _unitOfWork.UploadImage.UploadFile(productDTO.File!, "products", true);

                        if (result.Successed)
                        {

                            productDTO.Image = result.Url;
                            productDTO.Thumb = result.Thumb;

                            //if already have image
                            if (!string.IsNullOrEmpty(product.Image))
                            {
                                _unitOfWork.UploadImage.RemoveFile(product.Image, product.Thumb);
                            }

                        }
                        else
                        {
                            //Handle Response
                            response.Messages.Add(result.ErrorMessage);

                            return Ok(response);
                        }

                    }
                    else if (productDTO.Image is null && !string.IsNullOrEmpty(product.Image))
                    {
                        productDTO.Image = product.Image;
                        productDTO.Thumb = product.Thumb;
                    }
                    #endregion



                    //update book in sql
                    product = _mapper.Map(productDTO, product);
                    product.LastUpdatedAt = DateTime.Now;

                    int result2 = _unitOfWork.ProductRepository.Update(product);

                    ProductDTO vmodel = _mapper.Map<ProductDTO>(product);

                    //Handle Response
                    response.Successed = true;
                    response.Response = vmodel;

                    //mapping from Product to ProductDTO

                    return Ok(response);

                }
                else
                {
                    //Handle Response

                    IEnumerable<string> mSErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    response.Messages.AddRange(mSErrors);

                    return Ok(response);
                }

            }
            else
            {
                //Handle Response
                response.Messages.Add("Something is wrong..!!");

                return Ok(response);
            }


        }

        [HttpPost("Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            //get product by id
            Product product = _unitOfWork.ProductRepository.GetById(id);
            if(product != null)
            {
                //check if product has image 
                if (!string.IsNullOrEmpty(product.Image))
                {
                    //delete image
                    _unitOfWork.UploadImage.RemoveFile(product.Image , product.Thumb);
                }

                _unitOfWork.ProductRepository.Delete(product);

                ApiResponse<ProductDTO> result = new ApiResponse<ProductDTO>();
                result.Successed = true;
                result.Messages.Add("Product has been deleted");
                return Ok(result);

            }
            else
            {
				ApiResponse<ProductDTO> result = new ApiResponse<ProductDTO>();
				result.Messages.Add("Product not found..!!");
                return Ok(result);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll().ToList();

            //mapping from product to productDTO.
            List<ProductDTO> productDTOs = _mapper.Map<List<ProductDTO>>(products);


            return Ok(productDTOs);
        }

        [HttpGet("GetProductById/{id:int}")]
        public IActionResult GetProductById(int id)
        {
            Product product = _unitOfWork.ProductRepository.GetById(id);

            if(product != null)
            {
                //mapping from productDTO to product
                ProductDTO productDTOs = _mapper.Map<ProductDTO>(product);
                return Ok(productDTOs);
            }
            else
            {
                return NotFound("No Have Product With This Id..!!");
            }


            
        }

        [HttpGet("GetProductByName/{name:alpha}")]
        public IActionResult GetProductByName(string name)
        {
            Product product = _unitOfWork.ProductRepository.GetByName(name);

            if (product != null)
            {
                //mapping from productDTO to product
                ProductDTO productDTOs = _mapper.Map<ProductDTO>(product);
                return Ok(productDTOs);
            }
            else
            {
                return NotFound("No Have Product With This Name..!!");
            }



        }

        [HttpGet("GetProductByCode/{code:alpha}")]
        public IActionResult GetProductByCode(string code)
        {
            Product product = _unitOfWork.ProductRepository.GetByCode(code);

            if (product != null)
            {
                //mapping from productDTO to product
                ProductDTO productDTOs = _mapper.Map<ProductDTO>(product);
                return Ok(productDTOs);
            }
            else
            {
                return NotFound("No Have Product With This Code..!!");
            }



        }

        [HttpGet("Pagination/{page:int}")]
        public IActionResult Pagination([FromRoute] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _unitOfWork.ProductRepository.GetAll().AsQueryable();

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Products = query.ToList()
            };

            return Ok(result);
        }

    }
}
