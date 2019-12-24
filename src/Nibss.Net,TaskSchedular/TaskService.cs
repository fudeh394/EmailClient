using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Nibss.Net_TaskSchedular
{
    public partial class TaskService : ServiceBase
    {
        public TaskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            new Parkway.Scheduler.Synchronizer().Start();
        }

        protected override void OnStop()
        {
            Parkway.Scheduler.TaskMonitor.EnforceGracefulTermination();
        }
    }
}
