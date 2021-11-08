using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tddd49.Command;
using tddd49.Stores;

namespace tddd49.ViewModel
{
    class ConnectViewModel : ViewModelBase, IDataErrorInfo
    {
        private string userName;
        private string iPAddress;
        private string port;

        private Visibility iPLabelVisibility;
        private Visibility iPFieldVisibility;
        private Thickness usernameLabelMargin;
        private Thickness usernameFieldMargin;
        private Thickness mainButtonMargin;
        private string mainButtonContent;
        private ICommand mainButtonClick;


        public string UserName { get => userName; set { userName = value;  OnPropertyChanged("UserName"); } }
        public string IPAddress { get => iPAddress; set { iPAddress = value; OnPropertyChanged("IPAddress"); } }
        public string Port { get => port; set { port = value; OnPropertyChanged("Port"); } }

        public Visibility IPLabelVisibility { get => iPLabelVisibility; set { iPLabelVisibility = value; OnPropertyChanged("IPLabelVisibility"); } }
        public Visibility IPFieldVisibility { get => iPFieldVisibility; set { iPFieldVisibility = value; OnPropertyChanged("IPFieldVisibility"); } }
        public Thickness UsernameLabelMargin { get => usernameLabelMargin;  set { usernameLabelMargin = value; OnPropertyChanged("UsernameLabelMargin"); } }
        public Thickness UsernameFieldMargin { get => usernameFieldMargin;  set { usernameFieldMargin = value; OnPropertyChanged("UsernameFieldMargin"); } }
        public Thickness MainButtonMargin { get => mainButtonMargin; set { mainButtonMargin = value; OnPropertyChanged("MainButtonMargin"); } }
        public string MainButtonContent { get => mainButtonContent; set { mainButtonContent = value; OnPropertyChanged("MainButtonContent"); } }
        public ICommand MainButtonClick { get => mainButtonClick; set { mainButtonClick = value; OnPropertyChanged("MainButtonClick"); } }
        public ICommand JoinChatCommand { get;  }


        #region IDataErrorInfo Members

        private string error;
        public string Error { get => error; set { if (error != value) { error = value; OnPropertyChanged("Error"); } } }
        public string this[string columnName] { get => OnValidate(columnName); }

        private string OnValidate(string columnName)
        {
            string result = null;
            if (columnName == "UserName")
            {
                if (string.IsNullOrEmpty(userName))
                {
                    result = "Username is required.";
                }
                else if (!Regex.IsMatch(userName, @"^[a-zA-Z_]+$"))
                {
                    result = "Only letters and underscores allowed in username.";
                }
            } else if (columnName == "")

            error = result;

            return result;
        }
        #endregion


        public ConnectViewModel(NavigationStore navigationStore)
        {
            JoinChatCommand = new JoinChatCommand(navigationStore);
            JoinChatRadio();
        }


        public ICommand JoinChatRadioCommand { get => new RelayCommand(JoinChatRadio); }

        public ICommand HostChatRadioCommand { get => new RelayCommand(HostChatRadio); }

        public ICommand JoinChatButtonCommand { get => new RelayCommand(JoinChatButton); }

        public ICommand HostChatButtonCommand { get => new RelayCommand(HostChatButton); }

        internal void JoinChatRadio()
        {
            IPLabelVisibility = Visibility.Visible;
            IPFieldVisibility = Visibility.Visible;
            UsernameLabelMargin = new Thickness(228, 233, 0, 0);
            UsernameFieldMargin = new Thickness(228, 259, 0, 0);
            MainButtonMargin = new Thickness(268, 309, 0, 0);
            MainButtonContent = "Join Chat";
            MainButtonClick = JoinChatButtonCommand;
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
            if (error == null)
            {
                JoinChatCommand.Execute(this);
            } else
            {
                MessageBox.Show(error);
            } 
        }

        internal void HostChatButton()
        {
            Console.WriteLine("Host Chat Button");
        }

    }
}
