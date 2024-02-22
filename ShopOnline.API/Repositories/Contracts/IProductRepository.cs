using ShopOnline.API.Entities;

namespace ShopOnline.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductCategory>> GetCategories();
        public Task<ProductCategory> GetCategory(int id);
        public Task<Product> GetItem(int id);
        public Task<IEnumerable<Product>> GetItems();

    }
}
