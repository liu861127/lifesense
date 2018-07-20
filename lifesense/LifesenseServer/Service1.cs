using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ConsoleLifesense;

namespace LifesenseServer
{
    public partial class Service1 : ServiceBase
    {
        SyncDataManager temp = new SyncDataManager();
        log4net.ILog log = null;
        public Service1()
        {
            InitializeComponent();
            log = log4net.LogManager.GetLogger("服务模式");
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {
            temp.IsSyncData = false;
            temp.IsSyncDataFail = false;
            GC.Collect();
        }
        /// <summary>
        /// 开始
        /// </summary>
        internal void DebugStart()
        {
            Start();
        }

        private void Start()
        {
            temp.IsSyncData = true;
            temp.IsSyncDataFail = true;
            temp.log = log;
            temp.start();
        }
    }
}
