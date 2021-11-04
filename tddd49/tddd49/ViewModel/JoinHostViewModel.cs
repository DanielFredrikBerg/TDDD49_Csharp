using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tddd49.Command;
using tddd49.Stores;

namespace tddd49.ViewModel
{
    class JoinHostViewModel : ViewModelBase
    {

        private Visibility iPLabelVisibility;
        private Visibility iPFieldVisibility;
        private Thickness usernameLabelMargin;
        private Thickness usernameFieldMargin;
        private Thickness mainButtonMargin;
        private string mainButtonContent;
        private ICommand mainButtonClick;  


        public Visibility IPLabelVisibility { get { return iPLabelVisibility; } set { iPLabelVisibility = value; OnPropertyChanged("IPLabelVisibility"); } }
        public Visibility IPFieldVisibility { get { return iPFieldVisibility; } set { iPFieldVisibility = value; OnPropertyChanged("IPFieldVisibility"); } }
        public Thickness UsernameLabelMargin { get { return usernameLabelMargin; } set { usernameLabelMargin = value; OnPropertyChanged("UsernameLabelMargin"); } }
        public Thickness UsernameFieldMargin { get { return usernameFieldMargin; } set { usernameFieldMargin = value; OnPropertyChanged("UsernameFieldMargin"); } }
        public Thickness MainButtonMargin { get { return mainButtonMargin; } set { mainButtonMargin = value; OnPropertyChanged("MainButtonMargin"); } }
        public string MainButtonContent { get { return mainButtonContent; } set { mainButtonContent = value; OnPropertyChanged("MainButtonContent"); } }
        public ICommand MainButtonClick { get { return mainButtonClick; } set { mainButtonClick = value; OnPropertyChanged("MainButtonClick"); } }
        public ICommand JoinChatCommand { get;  }


        public JoinHostViewModel(NavigationStore navigationStore)
        {
            JoinChatCommand = new JoinChatCommand(navigationStore);
            JoinChatRadio();
        }


        public ICommand JoinChatRadioCommand
        {
            get
            {
                return new RelayCommand(JoinChatRadio);
            }
        }

        public ICommand HostChatRadioCommand
        {
            get
            {
                return new RelayCommand(HostChatRadio);
            }
        }

        public ICommand JoinChatButtonCommand
        {
            get 
            {
                return new RelayCommand(JoinChatButton);
            }
        }

        public ICommand HostChatButtonCommand
        {
            get
            {
                return new RelayCommand(HostChatButton);
            }
        }

        internal void JoinChatRadio()
        {
            IPLabelVisibility = Visibility.Visible;
            IPFieldVisibility = Visibility.Visible;
            UsernameLabelMargin = new Thickness(228, 233, 0, 0);
            UsernameFieldMargin = new Thickness(228, 259, 0, 0);
            MainButtonMargin = new Thickness(268, 309, 0, 0);
            MainButtonContent = "Join Chat";
            MainButtonClick = JoinChatCommand;
        }

        internal void HostChatRadio()
        {
            IPLabelVisibility = Visibility.Hidden;
            IPFieldVisibility = Visibility.Hidden;
            UsernameLabelMargin = new Thickness(228, 170, 0, 0);
            UsernameFieldMargin = new Thickness(228, 196, 0, 0);
            MainButtonMargin = new Thickness(268, 246, 0, 0);
            MainButtonContent = "Host Chat";
            MainButtonClick = HostChatButtonCommand;
        }

        internal void JoinChatButton()
        {
            Console.WriteLine("Join Chat Button");
            
        }

        internal void HostChatButton()
        {
            Console.WriteLine("Host Chat Button");
        }

    }
}
