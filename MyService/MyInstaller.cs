using System.ComponentModel;
using System.ServiceProcess;

namespace MyService
{
    [RunInstaller(true)]
    public partial class MyInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller _serviceInstaller;
        ServiceProcessInstaller _processInstaller;
        public MyInstaller()
        {
            InitializeComponent();
            _serviceInstaller = new ServiceInstaller();
            _processInstaller = new ServiceProcessInstaller {Account = ServiceAccount.LocalSystem};

            _serviceInstaller.StartType = ServiceStartMode.Manual;
            _serviceInstaller.ServiceName = "MyService";
            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
