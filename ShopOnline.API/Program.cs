
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopOnline.API.Data;
using ShopOnline.API.Repositories;
using ShopOnline.API.Repositories.Contracts;

namespace ShopOnline.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //To Add dependancy injection
            builder.Services.AddDbContextPool<ShopOnlineDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ShopOnlineConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            //End
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Add these lines to give permissions to access
            app.UseCors(policy =>
            policy.WithOrigins("http://localhost:7136", "https://localhost:7136")
            .AllowAnyMethod()
            .WithHeaders(HeaderNames.ContentType)
            );

            // app.UseCors(policy =>
            //policy.WithOrigins("http://localhost:7134/", "https://localhost:7134/")
            //.AllowAnyMethod()
            //.WithHeaders(HeaderNames.ContentType)
            //);



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
