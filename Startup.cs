using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plutus.WebService.IRepos;
using System;

namespace Plutus.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IFileManagerRepository, FileManagerRepository>();
            services.AddScoped<ISchedulerRepository, SchedulerRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IGoalsRepository, GoalsRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICartBackendRepository, CartBackendRepository>();
            services.AddScoped<IShoppingBackendRepository, ShoppingBackendRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddSingleton<IVerificationService, VerificationService>();
            services.AddSingleton<ILoggerRepository, LoggerRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
