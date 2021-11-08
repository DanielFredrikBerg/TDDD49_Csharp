using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Sockets;
using tddd49.Command;
using tddd49.Stores;

namespace tddd49.ViewModel
{
    class ChatViewModel : ViewModelBase
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public ICommand ExitChatCommand { get; }
        public ChatViewModel(NavigationStore navigationStore)
        {
            ExitChatCommand = new ExitChatCommand(navigationStore);
        }

        internal void ExitChatButton()
        {

        }
    }
}
