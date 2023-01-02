using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EindOpdrachtGentseFeesten.Domain;
using EindOpdrachtGentseFeesten.PresentationWPF;
using EindOpdrachtGentseFeesten.Persistence;

namespace EindOpdrachtGentseFeesten.StartupWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            EvenementMapper mapper = new EvenementMapper();
            DomainController domain = new DomainController(mapper);
            GentseFeestenApplication app = new GentseFeestenApplication(domain);
        }
    }
}
