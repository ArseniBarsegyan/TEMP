using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagerService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }

    class Logger
    {
        private FileSystemWatcher _watcher;
        object _obj = new object();
        private bool _enabled = true;

        public Logger()
        {
            _watcher = new FileSystemWatcher();
            _watcher.Created += WatcherOnCreated;
        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {

        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (_enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _enabled = false;
        }
    }
}
