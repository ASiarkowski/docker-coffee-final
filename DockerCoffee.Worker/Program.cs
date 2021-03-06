using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCoffee.Shared;
using DockerCoffee.Worker.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DockerCoffee.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDockerCoffeeShared();
                    services.AddMassTransitHostedService();
                    services.AddAndConfigureMassTransit(hostContext.Configuration, (cfg) =>
                    {
                        cfg.AddConsumer<RecurringJobConsumer>();
                        cfg.AddConsumer<OrderPlacedEventConsumer>();
                    });
                });
    }
}
