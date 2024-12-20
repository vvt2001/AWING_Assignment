using AWING_Assignment_API.Services;
using AWING_Assignment_Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AWING_Assignment_API
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

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1"}));

            /* services */
            services.AddTransient<IPirateNavigationServices, PirateNavigationServices>();

            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") // Allow only this origin
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("http://localhost:7284/swagger/v1/swagger.json", "My API V1"));
            }
            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        
    }
}
