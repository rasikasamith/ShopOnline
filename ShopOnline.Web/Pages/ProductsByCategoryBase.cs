﻿using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTO;
using ShopOnline.Web.Services.Contracts;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopOnline.Web.Pages
{
    public class ProductsByCategoryBase:ComponentBase
    {
        [Parameter]
        public int CategoryId { get; set; }
        [Inject]
        public IProductService _iProductService { get; set; }
        [Inject]
        public IManageProductsLocalStorageService iManageProductsLocalStorageService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
               // Products = await _iProductService.GetItemsByCategory(CategoryId);
                Products = await GetProductCollectionByCategoryId(CategoryId);

                if (Products !=null && Products.Count()>0)
                {
                    var productDto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);
                    if (productDto != null)
                    {
                        CategoryName = productDto.CategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage=ex.Message;
            }
        }

        private async Task<IEnumerable<ProductDto>> GetProductCollectionByCategoryId(int categoryId)
        {
            var productCollection = await iManageProductsLocalStorageService.GetCollection();
            if(productCollection !=null)
            {
                return productCollection.Where(p=>p.CategoryId== categoryId);

            }
            else
            {
                return await _iProductService.GetItemsByCategory(categoryId);
            }
        }


    }
}
