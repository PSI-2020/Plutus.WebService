using Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private readonly ILoggerService _logger = new LoggerService();
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PlutusDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<ISchedulerService, SchedulerService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IGoalsService, GoalsService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICartBackendService, CartBackendService>();
            services.AddScoped<IShoppingBackendService, ShoppingBackendService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddSingleton<ILoggerService, LoggerService>();
            PaymentController.PaymentAdded += OutputDataPaymentAdded;

        }
        private void OutputDataPaymentAdded(Payment payment) => _logger.Log(payment.Name + " was added");

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
