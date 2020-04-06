namespace VB.WebStarter
{
    using System;
    using System.Diagnostics;
    using System.ServiceProcess;
    using System.Threading;
    using System.Timers;
    using Assets;
    using Features;
    using Helpers;

    public partial class Service : ServiceBase
    {
        private static bool isStop = false;
        private int timerInterval = 30 * 1000;
        private AppPoolRecycler appPoolRecycler;
        private Thread executeThread;
        private System.Timers.Timer timer;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("OnStart");
            Configure();
            Console.WriteLine("Start Thread");
            executeThread = new Thread(Execute);
            executeThread.Start();

            timer = new System.Timers.Timer(24 * 60 * 1000);
            timer.Elapsed += TimerOnElapsed;
        }

        protected override void OnStop()
        {
            isStop = true;
            Console.WriteLine("Stop Thread");
            executeThread.Abort();
            timer.Elapsed -= TimerOnElapsed;
            Console.WriteLine("OnStop");
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            EventLog.WriteEntry(WebStarterConstants.ServiceName, $"I'm alive and running.", EventLogEntryType.Information);
        }

        private void Configure()
        {
            var configuration = ConfigurationHelper.ReadConfig();
            this.appPoolRecycler = new AppPoolRecycler();

            if (configuration.RunTimeIntervalSeconds > 0)
            {
                this.timerInterval = configuration.RunTimeIntervalSeconds * 1000;
            }
        }

        private void Execute()
        {
            while (!isStop)
            {
                appPoolRecycler.Execute();

                Console.WriteLine($"Executing {DateTime.UtcNow:dd.MM HH:mm:ss}");
                Thread.Sleep(timerInterval);
            }
        }
    }
}
