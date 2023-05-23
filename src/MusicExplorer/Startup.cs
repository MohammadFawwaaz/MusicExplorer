using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicExplorer.Infrastructure.Infrastructure.EntityFrameworkCore;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Services;
using System.Reflection;

namespace MusicExplorer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            // Add MediatR
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            // Register custom services
            services.AddTransient<IArtistService, ArtistService>();

            // Configure DbContext and connection string
            services.AddDbContext<ArtistDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SQL_ConnectionString"),
                    sqlOptions => sqlOptions.CommandTimeout(180)));

            // Register reposotories
            services.AddScoped<IArtistRepository, ArtistRepository>();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
