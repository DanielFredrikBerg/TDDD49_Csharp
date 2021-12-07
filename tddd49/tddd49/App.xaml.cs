using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using tddd49.Model;

namespace tddd49
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ChatHistory.Load();
            var mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Closed += Window_Closed;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Code for after window closes goes here.
            if (ChatHistory.IsUpdated)
            {
                ChatHistory.Save();
            }
            
        }
    }
}
