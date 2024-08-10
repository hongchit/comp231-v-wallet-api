
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using v_wallet_api.Data;
using v_wallet_api.Repositories;
using v_wallet_api.Services;

namespace v_wallet_api
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "V-Wallet API",
                    Version = "v1",
                    Description = "RESTFul Application for COMP231 Group 6",
                });
                c.EnableAnnotations();

            });

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IFinancialAccountRepository, FinancialAccountRepository>();
            builder.Services.AddScoped<IFinancialAccountService, FinancialAccountService>();
            builder.Services.AddScoped<IFinancialAccountTypeRepository, FinancialAccountTypeRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("v-wallet-sql"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", x =>
                {
                    x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowAnyOrigin");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
