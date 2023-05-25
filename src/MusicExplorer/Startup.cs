using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicExplorer.Client;
using MusicExplorer.Infrastructure.Infrastructure.EntityFrameworkCore;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Mappings;
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
            services.AddTransient<IArtistSearchService, ArtistSearchService>();
            services.AddTransient<IArtistReleaseService, ArtistReleaseService>();
            services.AddScoped<IArtistMapper, ArtistMapper>();

            services.AddHttpClient<IMusicBrainzClient, MusicBrainzClient>(httpClient => ConfigureArtistHttpClient(httpClient));

            // Configure DbContext and connection string
            services.AddDbContext<ArtistDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SQL_ConnectionString"),
                    sqlOptions => sqlOptions.CommandTimeout(180)));

            // Register repositories
            services.AddScoped<IArtistRepository, ArtistRepository>();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private void ConfigureArtistHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Clients:MusicBrainz:BaseUri").Value, UriKind.Absolute);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "MusicExplorer/1.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
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
