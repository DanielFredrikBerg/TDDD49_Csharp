using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace tddd49
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // Code for before window opens (optional);

            var mainWindow = new MainWindow();
            mainWindow.Show();

            mainWindow.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Code for after window closes goes here.
            // save data here ?
            MessageBox.Show("Goodbye World!");
        }
    }
}
