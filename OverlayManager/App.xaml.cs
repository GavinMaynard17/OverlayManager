using OverlayManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OverlayManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Match _match;
        protected override void OnStartup(StartupEventArgs e)
        {
            _match = new Match();

            base.OnStartup(e);
        }

    }
}
