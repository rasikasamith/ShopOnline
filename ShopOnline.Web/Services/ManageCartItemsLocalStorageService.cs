using Blazored.LocalStorage;
using ShopOnline.Models.DTO;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService _iLocalStorageService;
        private readonly IShoppingCartService _iShoppingCartService;

        const string key = "CartItemCollection";

        public ManageCartItemsLocalStorageService(ILocalStorageService iLocalStorageService
                                                  ,IShoppingCartService iShoppingCartService)
        {
            _iLocalStorageService= iLocalStorageService;
            _iShoppingCartService= iShoppingCartService;
        }
        public async Task<List<CartItemDto>> GetCollection()
        {
            return await _iLocalStorageService.GetItemAsync<List<CartItemDto>>(key)
                ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _iLocalStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CartItemDto> cartItemDtos)
        {
            await _iLocalStorageService.SetItemAsync(key, cartItemDtos);
        }

        private async Task<List<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await _iShoppingCartService.GetItems(HardCoded.UserId);

            if(shoppingCartCollection != null)
            {
                await _iLocalStorageService.SetItemAsync(key, shoppingCartCollection);
            }
            return shoppingCartCollection;
        }
    }
}
