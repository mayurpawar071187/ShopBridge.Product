using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShopBridge.Product.Business;
using ShopBridge.Product.Business.Repository;
using ShopBridge.Product.DataBase.DBEntity;
using ShopBridge.Product.DataBase.Identity;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Product
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
            
            services.AddControllers(config=> config.Filters.Add(new ActionFilterExample()));
            services.AddDbContext<shopbridgeContext>();
            //services.AddDbContext<shopbridgeContext>(option => option.UseNpgsql(Configuration.GetConnectionString("PostgresShopBridge")));
            services.AddDbContext<ShopBridgeIdentityDBContext>(option=>option.UseNpgsql(Configuration.GetConnectionString("PostgresIdetity")));

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
            services.AddScoped<ActionFilterExample>();

            services.AddIdentity<ShopBridgeUser, IdentityRole>()
                .AddEntityFrameworkStores<ShopBridgeIdentityDBContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtoptions => {
                //jwtoptions.SaveToken = true;
                //jwtoptions.RequireHttpsMetadata = true;
                
                jwtoptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["JWTData:ValidIssuer"],
                    ValidAudience = Configuration["JWTData:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTData:SIGN_KEY"])),
                };
            });

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.ConfigureExceptionHandler();
            //app.ConfigureExceptionHandlerMiddleware();

            app.UseRouting();

            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class ActionFilterExample : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
    public class AsyncActionFilterExample : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            var result = await next();
            // execute any code after the action executes
        }
    }
   
}
