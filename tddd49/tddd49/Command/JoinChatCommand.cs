using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tddd49.Network;
using tddd49.Stores;
using tddd49.ViewModel;

namespace tddd49.Command
{
    class JoinChatCommand : NavigationCommandBase
    {   
        public JoinChatCommand(NavigationStore navigationStore) : base(navigationStore)
        {
        }

        override public void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new ChatViewModel(_navigationStore, (Client) parameter);
        }

    }
}
