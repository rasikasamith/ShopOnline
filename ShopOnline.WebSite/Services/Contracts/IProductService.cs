using ShopOnline.Models.DTO;

namespace ShopOnline.WebSite.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}
