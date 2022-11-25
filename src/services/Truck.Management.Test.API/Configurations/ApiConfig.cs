using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Truck.Management.Test.API.ExceptionMiddleware;
using Truck.Management.Test.Application.Handlers;
using Truck.Management.Test.Application.Interfaces;
using Truck.Management.Test.Application.Notifications;
using Truck.Management.Test.Application.Services;
using Truck.Management.Test.Domain.Interfaces;
using Truck.Management.Test.Infra.Data.Context;
using Truck.Management.Test.Infra.Data.Repository;
using Truck.Management.Test.Infra.Data.UoW;

namespace Truck.Management.Test.API.Configurations
{
    public static class ApiConfig
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            services.AddHttpContextAccessor();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application
            services.AddScoped<ITruckApplication, TruckApplication>();
            services.AddScoped<INotificationHandler<ApplicationNotification>, ApplicationNotificationHandler>();

            //Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITruckRepository, TruckRepository>();


            // Context
            services.AddScoped<DataBaseContext>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
