using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tddd49.Stores;
using tddd49.ViewModel;

namespace tddd49.Command
{
    class JoinChatCommand : ICommand
    {

        private readonly NavigationStore _navigationStore;

        public event EventHandler CanExecuteChanged;

        public JoinChatCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new ChatViewModel();
        }
    }
}
