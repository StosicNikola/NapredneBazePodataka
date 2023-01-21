using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Neo4jClient;
using naprednebazeback.Hubs;
using naprednebazeback.RedisDataLayer;
using StackExchange.Redis;

namespace naprednebazeback
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

            //Neo4J connection

            var neo4JClient = new BoltGraphClient(new Uri("bolt://localhost:7687"), "neo4j", "root1234");
            neo4JClient.ConnectAsync();
            services.AddSingleton<IGraphClient>(neo4JClient);

            services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string,UserConnection>());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "naprednebazeback", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); 
            });

           /* var multiplexer = ConnectionMultiplexer.Connect("localhost");
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);*/

            //Logger
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddSignalR();
          
            services.AddCors( options=>{
                options.AddPolicy("CORS",builder=>
                {
                    builder.WithOrigins("https:localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    builder.WithOrigins(new string[]
                    {
                        "https://localhost:8080",
                        "https://localhost:8080",
                        "http://127.0.0.1:8080",
                        "http://127.0.0.1:8080",
                        "https://localhost:5001",
                        "http://127.0.0.1:5500",
                        "https://localhost:5001",
                        "http://127.0.0.1:5001",
                        "http://127.0.0.1:3000",
                        "http://localhost:3000",
                        "https://127.0.0.1:3000",
                        "https://localhost:3000",
                        "https:localhost:3000"
                    })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "naprednebazeback v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
