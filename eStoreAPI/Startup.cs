using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using BusinessObject.RequestModels;
using BusinessObject.ResponseModels;
using DataAccess.Repositories;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace eStoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddOData(option => option.Select().Filter()
                .OrderBy().Expand().SetMaxTop(100));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eStoreAPI", Version = "v1" });
            });
            services.AddDbContext<ProductStoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StoreDB")));

            var mapper = new MapperConfiguration(mc =>
            {
                mc.CreateMap<ProductRequestModel, Product>();
                mc.CreateMap<Product, ProductResponseModel>()
                    .ForMember(des => des.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
                mc.CreateMap<Category, CategoryResponseModel>();
                mc.CreateMap<OrderDetailRequestModel, OrderDetail>();
                mc.CreateMap<OrderDetail, OrderDetailResponseModel>()
                    .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
                mc.CreateMap<OrderRequestModel, Order>();
                mc.CreateMap<Order, OrderResponseModel>();
                mc.CreateMap<User, UserResponseModel>().ReverseMap();
            }).CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton(typeof(IUserRepository), typeof(UserRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eStoreAPI v1"));
            }

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
