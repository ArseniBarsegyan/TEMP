using System.ComponentModel;
using System.ServiceProcess;

namespace ManagerService
{
    [RunInstaller(true)]
    public partial class ManagerServiceInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public ManagerServiceInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ManagerService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
