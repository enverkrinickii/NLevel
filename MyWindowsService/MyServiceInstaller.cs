using System.ComponentModel;
using System.ServiceProcess;

namespace MyWindowsService
{
    [RunInstaller(true)]
    public partial class MyServiceInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller _serviceInstaller;
        ServiceProcessInstaller _processInstaller;
        public MyServiceInstaller()
        {
            InitializeComponent();
            _serviceInstaller = new ServiceInstaller();
            _processInstaller = new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem };

            _serviceInstaller.StartType = ServiceStartMode.Manual;
            _serviceInstaller.ServiceName = "MyWindowsService";
            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
