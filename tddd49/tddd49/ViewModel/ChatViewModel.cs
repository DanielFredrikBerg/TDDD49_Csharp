using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Sockets;
using tddd49.Command;
using tddd49.Stores;
using tddd49.Network;

namespace tddd49.ViewModel
{
    class ChatViewModel : ViewModelBase
    {
        private Client client;
        public ICommand ExitChatCommand { get; }
        public ChatViewModel(NavigationStore navigationStore, Client client)
        {
            ExitChatCommand = new ExitChatCommand(navigationStore);
            this.client = client;
        }

        internal void ExitChatButton()
        {

        }
    }
}
