using AutoMapper;
using ES.Application.AutoMapperProfile;
using ES.Application.Handler;
using ES.Domain.Persistence;
using ES.Domain.Persistence.EventStore;
using ES.Domain.Services;
using ES.Infra.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;

namespace ES.API
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

            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new AutoMapper.Profile[] { new StarshipProfile() });
            });

            services.AddSingleton(x => config.CreateMapper());

            services.AddMediatR(new Type[] { typeof(WriteStarshipHandler) });


            var store = new DocumentStore
            {
                Urls = new string[] { "http://raven:8080" },
                Database = "starships"
            };

            store.Initialize();

            services.AddSingleton<IDocumentStore>(store);

            services.AddScoped(serviceProvider =>
            {
                return serviceProvider
                    .GetService<IDocumentStore>()
                    .OpenAsyncSession();
            });

            var client = new ElasticClient(new ConnectionSettings(new Uri("http://elk:9200")));

            services.AddSingleton<IElasticClient>(client);

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IStarshipRepository, StarshipRepository>();
            services.AddScoped<IStarshipEventService, StarshipEventService>();
            services.AddScoped<IStarshipService, StarshipService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Starship API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var store = serviceScope.ServiceProvider.GetRequiredService<IDocumentStore>();
                EnsureDatabaseExists(store, "starships");
            }
        }

        private void EnsureDatabaseExists(IDocumentStore store, string database = null, bool createDatabaseIfNotExists = true)
        {
            database = database ?? store.Database;

            if (string.IsNullOrWhiteSpace(database))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(database));

            try
            {
                store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {
                if (createDatabaseIfNotExists == false)
                    throw;

                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
            }

            new ES.Infra.Index.StarshipDistanceIndex().Execute(store);
            new ES.Infra.Index.PilotIndex().Execute(store);
        }
    }
}
