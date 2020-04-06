namespace VB.WebStarter
{
    using System;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Threading;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };

            if (Environment.UserInteractive)
            {
                RunInteractive(ServicesToRun);
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }
        }

        static void RunInteractive(ServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            var onStartMethod = OnStartMethod();
            foreach (var service in servicesToRun)
            {
                Console.WriteLine("Starting {0}", service.ServiceName);
                onStartMethod?.Invoke(service, new object[] { new string[] { } });
                Console.WriteLine("Started");
                Console.WriteLine(".......");
            }

            Console.WriteLine("Press any key to stop the services and end the process.");
            Console.Read();
            Console.WriteLine();

            var onStopMethod = OnStopMethod();
            foreach (var service in servicesToRun)
            {
                Console.WriteLine("Stopping {0}", service.ServiceName);
                onStopMethod.Invoke(service, null);
                Console.WriteLine("Stopped");
                Console.WriteLine(".......");
            }

            Console.WriteLine("All services stopped.");
            // Keep the console alive for a ten seconds to allow the user to see the message.
            Thread.Sleep(1000 * 10);
        }

        private static MethodInfo OnStopMethod()
        {
            return typeof(ServiceBase).GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static MethodInfo OnStartMethod()
        {
            return typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
