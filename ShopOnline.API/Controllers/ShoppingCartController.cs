using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.DTO;

namespace ShopOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IShoppingCartRepository _iShoppingCartRepository;

        public ShoppingCartController(IProductRepository iProductRepository, IShoppingCartRepository iShoppingCartRepository)
        {
            _iProductRepository = iProductRepository;
            _iShoppingCartRepository = iShoppingCartRepository;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> Getitems(int userId)
        {
            try
            {
                var cartitems = await _iShoppingCartRepository.GetItems(userId);

                if (cartitems == null)
                {
                    return NoContent();
                }

                var products = await _iProductRepository.GetItems();

                if (products == null)
                {
                    throw new Exception("No products exist in the system");
                }

                var cartItemsDto = cartitems.ConvertToDto(products);
                return Ok(cartItemsDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var cartItem = await _iShoppingCartRepository.GetItem(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                var product = await _iProductRepository.GetItem(cartItem.Productid);
                if (product == null)
                {
                    return NotFound();
                }

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await _iShoppingCartRepository.AddItem(cartItemToAddDto);

                if (newCartItem == null)
                {
                    return NoContent();
                }
                var product = await _iProductRepository.GetItem(newCartItem.Productid);
                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrive product (productId:({cartItemToAddDto.ProductId})");
                }

                var newCartItemDto = newCartItem.ConvertToDto(product);
                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteCartItem(int id)
        {
            try
            {
                var cartItem = await this._iShoppingCartRepository.Deleteitem(id);
                if(cartItem == null)
                {
                    return NotFound();
                }
                var product = await this._iProductRepository.GetItem(cartItem.Productid);

                if (product == null)
                    return NotFound();

                var cartitemDto = cartItem.ConvertToDto(product);

                return Ok(cartitemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);               
            }
            
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(int id,CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var cartItem = await this._iShoppingCartRepository.UpdateQty(id, cartItemQtyUpdateDto);
                if(cartItem==null)
                {
                    return NotFound();
                }
                var product = await _iProductRepository.GetItem(cartItem.Productid);
                var carItemDto = cartItem.ConvertToDto(product);
                return Ok(carItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
