//using DadosConexao;
using DadosConexao;
using Dominio.Models;
using Infraestrutura.Interface;
using Infraestrutura.Repositorio;
using Infraestrutura.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ClimaTempo
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
           // services.AddHostedService<CidadeQueueConsumer>();
            services.AddDbContext<ConexaoContexto>(options => options.UseSqlServer(Configuration.GetConnectionString("ConexaoBancoDados")), ServiceLifetime.Singleton);
            
            services.AddAzureClients(builder => 
            {
                builder.AddServiceBusClient(Configuration.GetValue<string>("AzureServiceBus"));
            });

            services.AddSingleton<ICidadeRepositorio, CidadeRepositorio>();
            services.AddSingleton<IEstadoRepositorio, EstadoRepositorio>();

            services.AddSingleton<ICidadeServiceBusConsumer, CidadeQueueConsumer>();
            services.AddSingleton<IEstadoServiceBusConsumer, EstadoQueueConsumer>();
            services.AddSingleton<IPrevisaoClimaServiceBusConsumer, PrevisaoClimaQueueConsumer>();

            services.AddHostedService<WorkerServiceBus>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClimaTempo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClimaTempo v1"));
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
