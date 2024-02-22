using ShopOnline.API.Entities;
using ShopOnline.Models.DTO;

namespace ShopOnline.API.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id,CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> Deleteitem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
