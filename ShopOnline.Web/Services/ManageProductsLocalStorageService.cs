using Blazored.LocalStorage;
using ShopOnline.Models.DTO;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService _iLocalStorageService;
        private readonly IProductService _iProductService;

        private const string key = "ProductCollection";

        public ManageProductsLocalStorageService(ILocalStorageService iLocalStorageService,IProductService iProductService)
        {
            this._iLocalStorageService= iLocalStorageService;
            this._iProductService= iProductService;
        }
        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await this._iLocalStorageService.GetItemAsync<IEnumerable<ProductDto>>(key)
                ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this._iLocalStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await this._iProductService.GetItems();
            if(productCollection != null)
            {
                await this._iLocalStorageService.SetItemAsync(key, productCollection);
            }

            return productCollection;
        }
    }
}
