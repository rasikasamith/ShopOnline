using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTO;
using ShopOnline.WebSite.Services.Contracts;

namespace ShopOnline.WebSite.Pages
{
    public class AllProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
        //public List<ProductDto> Products { get; set; }

        //protected override async Task OnInitializedAsync()
        //{

        //    //Products.Add(new ProductDto()
        //    //{
        //    //    Id = 1,
        //    //    Name = "Glossier - Beauty Kit",
        //    //    Description = "A kit provided by Glossier, containing skin care, hair care and makeup products",
        //    //    ImageURL = "/Images/Beauty/Beauty1.png",
        //    //    Price = 100,
        //    //    Qty = 100,
        //    //    CategoryId = 1,
        //    //    CategoryName = "Beauty"
        //    //});

        //    Products = await ProductService.GetItems();
        //}

        protected override  void OnInitialized()
        {

            Products = ProductService.GetItems();
        }




    }
}
