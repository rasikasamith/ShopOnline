using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Entities;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.DTO;

namespace ShopOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        public ProductController(IProductRepository iProductRepository)
        {
            _iProductRepository= iProductRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            
            try
            {
                var products = await _iProductRepository.GetItems();
                var categories = await _iProductRepository.GetCategories();

                if(products == null || categories==null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos= products.ConvertToDto(categories);
                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the databse");
                
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var  product =await _iProductRepository.GetItem(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var productCategory = await _iProductRepository.GetCategory(product.CategoryId);
                    var productDto = product.ConvertToDto(productCategory);
                    return Ok(productDto);
                }

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the databse");
            }
        }
    }
}
