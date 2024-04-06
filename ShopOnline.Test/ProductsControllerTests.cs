using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.DTO;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using MyProject.Controllers;
//using MyProject.Models;
//using MyProject.Repositories;
using Xunit;
using Moq;
using ShopOnline.API.Controllers;
using ShopOnline.API.Repositories;
using ShopOnline.API.Data;
using System.Collections.Generic;
using ShopOnline.API.Entities;

namespace ShopOnline.Test
{
    public class ProductsControllerTests
    {
        ShopOnlineDbContext _shopOnlineDbContext;
        IProductRepository _iProductRepository;
        ProductController _productController;        
             


        public ProductsControllerTests()
        {
            //_shopOnlineDbContext = null;
            _iProductRepository = new ProductRepository(_shopOnlineDbContext);
            _productController=new ProductController(_iProductRepository);          
        }
      


        [Fact]
        public void ProductController_GetProducts()
        {
            //Act
            var result = _productController.GetProducts();

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //var productDtos = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.NotNull(result);
            //Assert.Equal(23, productDtos.Count());
        }

        //[Theory]
        //[InlineData(1)] // Valid ID
        ////[InlineData(999)] // Invalid ID
        //public async Task GetProduct_ReturnsCorrectResult(int id)
        //{
        //    // Act
        //    var result = await _productController.GetProduct(id);

        //    // Assert
        //    if (result.Result is OkObjectResult okObjectResult)
        //    {
        //        Assert.IsType<ProductDto>(okObjectResult.Value);

        //        // Additional assertions for valid ID
        //        if (id == 1)
        //        {
        //            var productDto = (ProductDto)okObjectResult.Value;
        //            Assert.Equal(1, productDto.Id);
        //            Assert.Equal("Expected Product Name", productDto.Name);
        //        }
        //    }
        //    else if (result.Result is BadRequestResult)
        //    {
        //        // Additional assertions for invalid ID
        //        if (id == 999)
        //        {
        //            Assert.True(true); // You can add specific assertions here if needed
        //        }
        //    }
        //    else
        //    {
        //        // Unexpected result
        //        Assert.True(false, "Unexpected result type");
        //    }
        //}



        //[Theory]
        //[InlineData(3, 89)]
        //public void ProductController_GetProduct(int validId, int invalidId)
        //{
        //    //arrage
        //    //act
        //    var okResult = _productController.GetProduct(validId);
        //    var notFoundResult = _productController.GetProduct(invalidId);

        //    //assert         
        //    Assert.IsType<ActionResult>(okResult.Result);
        //    Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);


        //    Assert.IsType<ProductDto>(okResult.Result.Value);
        //    Assert.Equal(3, okResult.Result.Value.Id);
        //    Assert.Equal("Cocooil - Organic Coconut Oil", okResult.Result.Value.Name);

        //}

        //[Fact]
        //public void ProductController_GetProductCount()
        //{
        //    int rowCount = 23;
        //    int rowCountOfResultList = 0;
        //    var resultList = _productController.GetProducts() as IEnumerable<ProductDto>;
        //    if (resultList != null)
        //    {
        //        rowCountOfResultList = resultList.Count();
        //    }

        //    Assert.Equal(rowCount, rowCountOfResultList);
        //}


    }
}





