using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tddd49.Command;
using tddd49.Network;
using tddd49.Stores;

namespace tddd49.ViewModel
{
    class ConnectViewModel : ViewModelBase
    {
        private Client client;

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
        private string listenningStatus;

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
        public string ListeningStatus { get => listenningStatus; set { listenningStatus = value; OnPropertyChanged("ListeningMsg"); } }
        public ICommand JoinChatCommand { get;  }


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
            UsernameLabelMargin = new Thickness(-98, 233, 0, 0);
            UsernameFieldMargin = new Thickness(0, 259, 0, 0);
            MainButtonMargin = new Thickness(0, 309, 0, 0);
            MainButtonContent = "Join Chat";
            MainButtonClick = JoinChatButtonCommand;
        }

        internal void HostChatRadio()
        {
            IPLabelVisibility = Visibility.Hidden;
            IPFieldVisibility = Visibility.Hidden;
            UsernameLabelMargin = new Thickness(-98, 170, 0, 0);
            UsernameFieldMargin = new Thickness(0, 196, 0, 0);
            MainButtonMargin = new Thickness(0, 246, 0, 0);
            MainButtonContent = "Host Chat";
            MainButtonClick = HostChatButtonCommand;
        }

        internal void JoinChatButton()
        {
            if (ValidatePort() && ValidateIP() && ValidateUserName())
            {
                if (client != null)
                {
                    client.Close();
                }
                else
                {
                    client = new Client(port, iPAddress);
                }
                client.Connect();
                //JoinChatCommand.Execute(this);
            } 
        }

        internal void HostChatButton()
        {
            if (ValidatePort() && ValidateUserName())
            {
                if (client != null)
                {
                    client.Close();
                }
                client = new Client(port);

                client.Listen();

                Thread.Sleep(100);
                if (client.isListening)
                {
                    ListeningStatus = $"Listening on 127.0.0.1 : {port}";
                }
                else
                {
                    ListeningStatus = "";
                }  
            }
        }

        internal bool ValidatePort()
        {
            int portNumber;
            if (string.IsNullOrEmpty(port))
            {
                MessageBox.Show("Port is required.");
                return false;
            }
            else if (!int.TryParse(port, out portNumber) || portNumber < 0 || portNumber > 65535)
            {
                MessageBox.Show("Port should be a number 0-65535.");
                return false;
            }
            return true;
        }

        internal bool ValidateIP()
        {
            if (string.IsNullOrEmpty(iPAddress))
            {
                MessageBox.Show("IP-address is required.");
                return false;
            }
            else if (!Regex.IsMatch(iPAddress, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
            {
                MessageBox.Show("Invalid IP-address.");
                return false;
            }
            return true;
        }

        internal bool ValidateUserName()
        {
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Username is required.");
                return false;
            }
            else if (!Regex.IsMatch(userName, @"^[a-zA-Z_]+$"))
            {
                MessageBox.Show("Only letters and underscores allowed in username.");
                return false;
            }
            return true;
        }
    }
}
