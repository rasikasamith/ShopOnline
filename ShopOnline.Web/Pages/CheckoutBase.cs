﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.DTO;
using ShopOnline.Web.Services;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class CheckoutBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        protected int TotalQty { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService iManageCartItemsLocalStorageService { get; set; }
              

        protected string DisplayButtons { get; set; } = "block";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                ShoppingCartItems = await iManageCartItemsLocalStorageService.GetCollection();


                if (ShoppingCartItems != null /*&& ShoppingCartItems.Count() > 0*/)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = ShoppingCartItems.Sum(p => p.TotalPrice);
                    TotalQty = ShoppingCartItems.Sum(p => p.Qty);
                    PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}"; 

                }
                //else
                //{
                //    DisplayButtons = "none";
                //}

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
