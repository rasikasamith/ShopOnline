﻿using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.DTO;

namespace ShopOnline.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext _shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            _shopOnlineDbContext = shopOnlineDbContext;
        }
        private async Task<bool> CartItemExists(int cartId,int productid)
        {
            return await _shopOnlineDbContext.CartItems.AnyAsync(x => x.CartId == cartId && x.Productid == productid);
        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {       
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in _shopOnlineDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      Productid = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await _shopOnlineDbContext.CartItems.AddAsync(item);
                    await this._shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }

            }

            return null;

        }

        public async Task<CartItem> Deleteitem(int id)
        {
            var item = await this._shopOnlineDbContext.CartItems.FindAsync(id);
            if(item != null)
            {
                this._shopOnlineDbContext.CartItems.Remove(item);
                await this._shopOnlineDbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in _shopOnlineDbContext.Carts
                          join cartItem in _shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              Productid = cartItem.Productid,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in _shopOnlineDbContext.Carts
                          join cartItem in this._shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              Productid = cartItem.Productid,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId,
                          }).ToListAsync();      

        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this._shopOnlineDbContext.CartItems.FindAsync(id);
            if(item !=null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this._shopOnlineDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}
