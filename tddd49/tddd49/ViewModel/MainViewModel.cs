using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using tddd49.Command;
using tddd49.Model;
using tddd49.Stores;

namespace tddd49.ViewModel
{
    sealed class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        private HistoryViewModel historyViewModel;
        public HistoryViewModel HistoryViewModel { get => historyViewModel; private set { historyViewModel = value; } }
  
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            historyViewModel = new HistoryViewModel();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
