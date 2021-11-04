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
using System.Net.Sockets;
using System.Net;
using tddd49.ViewModel;
using tddd49.Stores;

namespace tddd49
{

    public partial class MainWindow : Window 
    {
        private readonly NavigationStore navigationStore = new NavigationStore();

        //private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public MainWindow()
        {
            InitializeComponent();
            navigationStore.CurrentViewModel = new JoinHostViewModel(navigationStore);
            DataContext = new MainViewModel(navigationStore);
        }

        private void StartServer()
        {
           // _socket.Bind(new IPEndPoint(IPAddress.Any, 100));
        }


    }
}
