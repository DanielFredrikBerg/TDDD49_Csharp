using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tddd49.Stores;
using tddd49.ViewModel;

namespace tddd49.Command
{
    class ExitChatCommand : NavigationCommandBase
    {
        public ExitChatCommand(NavigationStore navigationStore) : base(navigationStore)
        {
        }

        override public void Execute(object _)
        {
            _navigationStore.CurrentViewModel = new ConnectViewModel(_navigationStore);
        }
    }
}
