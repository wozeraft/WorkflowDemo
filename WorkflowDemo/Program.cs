using System;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowDemo.ThreeInOne;

namespace WorkflowDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            var host = serviceProvider.GetService<IWorkflowHost>();
            HostKeeper.Host = host;

            host.RegisterWorkflow<ThreeInOneWorkflow, ThreeInOneData>();
            host.StartWorkflow("SingleWorkflowArePossible", new ThreeInOneData());
            host.Start();

            bool active = true;
            while (active)
            {
                Console.WriteLine("Enter 1 to complete moderation, 2 to reinit timer");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        HostKeeper.Host.PublishEvent("ModerationCompletedEvent", "0", "user");
                        active = false;
                        break;
                    case "2":
                        HostKeeper.Host.PublishEvent("ModerationTimeChangedEvent", "0", "user");
                        continue;
                    default:
                        continue;
                }
            }

            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
