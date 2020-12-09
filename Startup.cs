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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IShoppingRepository, ShoppingRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddSingleton<IVerificationRepository, VerificationRepository>();
            services.AddSingleton<ILoggerRepository, LoggerRepository>();
        }
        private void OutputDataDeletion(object o, string name)
        {
            Console.WriteLine(name + " was deleted");
        }
        private void OutputDataPaymentAdded(Payment payment)
        {
            Console.WriteLine(payment.Name + " was added");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            GoalsController.GoalDeletedEvent += OutputDataDeletion;
            PaymentController.PaymentAdded += OutputDataPaymentAdded;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
