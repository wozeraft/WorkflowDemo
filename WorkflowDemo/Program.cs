using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowDemo.OrderWorkflow;

namespace WorkflowDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();


            var host = serviceProvider.GetService<IWorkflowHost>();
            HostKeeper.Host = host;

            host.RegisterWorkflow<OrderWorkflow.OrderWorkflow, OrderWorkflowData>();

            host.Start();

            int demandListId = (int)DateTime.Now.Ticks % int.MaxValue;
            for (int i = 0; i < 1; i++)
            {
                host.StartWorkflow("OrderWorkflow", new OrderWorkflowData()
                {
                    OrderId = i ,
                    DemandListId = demandListId
                });
            }
            
            while (true)
            {
                Console.WriteLine("Enter 1 to begin moderation, 2 to complete moderation");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        HostKeeper.Host.PublishEvent("BeginModerationEvent", demandListId.ToString(), "timeout");
                        break;
                    case "2":
                        HostKeeper.Host.PublishEvent("ModerationCompletedTimeoutEvent", demandListId.ToString(), "timeout");
                        break;
                    default:
                        continue;
                }
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow(
                options =>
                {
                    //options.UseMaxConcurrentWorkflows(1);
                    options.UsePostgreSQL(
                        "Server=localhost;Port=5432;Database=wf-demo;User ID=postgres;Password=pwd;No Reset On Close=true",
                        true,
                        true);
                });
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
