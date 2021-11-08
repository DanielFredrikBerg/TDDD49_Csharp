using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
