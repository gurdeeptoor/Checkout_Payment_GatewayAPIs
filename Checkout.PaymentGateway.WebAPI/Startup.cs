using Checkout.PaymentGateway.Data;
using Checkout.PaymentGateway.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CheckOut.Common;

namespace Checkout.PaymentGateway.WebAPI
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
            services.AddControllers();
            services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);
            services.AddLogging();
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IMerchantRepository, MerchantRepository>();

            services.AddDbContext<CheckOutDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CheckOutDBContext")));

            //Pass sendgrid settings
            IConfigurationSection bankAPISettings = Configuration.GetSection("BankAPISettings");
            services.Configure<BankAPISettings>(bankAPISettings);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Checkout PaymentGateway API V1 by Gurdeep Singh", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout PaymentGateway API V1 by Gurdeep Singh");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
