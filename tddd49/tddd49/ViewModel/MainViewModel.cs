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
