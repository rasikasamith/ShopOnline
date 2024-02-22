using Microsoft.AspNetCore.Components;
using ShopOnline.WebSite.Services;

namespace ShopOnline.WebSite.Pages
{
    public class ContactBase: ComponentBase
    {
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Id = 8343;
        }
    }
}
