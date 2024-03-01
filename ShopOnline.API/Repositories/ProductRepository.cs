﻿using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;

namespace ShopOnline.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _shopOnlineDbContext;

        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            _shopOnlineDbContext = shopOnlineDbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories =await _shopOnlineDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(x=>x.Id==id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await _shopOnlineDbContext.Products
                .Include(p => p.ProductCategory)
                .SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _shopOnlineDbContext.Products.Include(p=>p.ProductCategory).ToArrayAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int Id)
        {  
            var products = await _shopOnlineDbContext.Products
              .Include(p => p.ProductCategory)
              .Where(p => p.CategoryId == Id).ToListAsync();

            return products;
        }
    }
}
