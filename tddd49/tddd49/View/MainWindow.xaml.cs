using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using tddd49.ViewModel;
using tddd49.Stores;

namespace tddd49
{

    public partial class MainWindow : Window 
    {
        private readonly NavigationStore navigationStore = new NavigationStore();

        public MainWindow()
        {
            InitializeComponent();
            navigationStore.CurrentViewModel = new ConnectViewModel(navigationStore);
            DataContext = new MainViewModel(navigationStore);
        }
    }
}
