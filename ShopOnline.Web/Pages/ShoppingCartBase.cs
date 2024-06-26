﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.DTO;
using ShopOnline.Web.Services;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        IShoppingCartService iShoppingCartService { get; set; }

        [Inject]
        IManageCartItemsLocalStorageService iManageCartItemsLocalStorageService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await iManageCartItemsLocalStorageService.GetCollection();             
                CartChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await iShoppingCartService.Deleteitem(id);
            //Add
            //ShoppingCartItems = await iShoppingCartService.GetItems(HardCoded.UserId);        

            await RemoveCartItem(id);           
            CartChanged();
        }
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }

        private async Task RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            ShoppingCartItems.Remove(cartItemDto);

            await iManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };
                    var returnedUpdteItemDto = await this.iShoppingCartService.UpdateQty(updateItemDto);

                    await UpdateItemTotalPrice(returnedUpdteItemDto);                   
                    CartChanged();

                    await MakeUpdateQtyButtonVisible(id, false);
                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
        }

        private void setTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C");
        }
        private void setTotalQty()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(x => x.Qty);
        }

        private void calculateCartSummeryTotals()
        {
            setTotalPrice();
            setTotalQty();
        }

        private async Task UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item=GetCartItem(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }
            await iManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
        }

        protected async Task UpdateQty_Input(int id)
        {           
            await MakeUpdateQtyButtonVisible(id, true);
        }

        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        private void CartChanged()
        {
            calculateCartSummeryTotals();
            iShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    }
}
