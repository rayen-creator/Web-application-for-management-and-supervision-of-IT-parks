using SignalRbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetailService
{
    public partial class Service1 : ServiceBase
    {
        NLog.Logger logger = NLog.LogManager.GetLogger("*");
        Timer serviceTimer;

        public Service1()
        {
            InitializeComponent();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
        protected override void OnStart(string[] args)
        {
            
            logger.Debug("Starting service ");
            serviceTimer = new Timer(new TimerCallback(serviceTimer_callback), null, 0, 60000);
        }

        private void serviceTimer_callback(object state)
        {
            try
            {
                logger.Debug("Callback started");
                InfoManager manager = new InfoManager();
                logger.Debug("Starting sending Data");
                manager.SendData();
                logger.Debug("Ended sending data");

            }catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

        protected override void OnStop()
        {

            logger.Debug("Service stopped");
        }
    }
}
