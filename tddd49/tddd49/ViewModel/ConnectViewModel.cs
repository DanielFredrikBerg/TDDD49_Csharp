using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using tddd49.Command;
using tddd49.Network;
using tddd49.Stores;

namespace tddd49.ViewModel
{
    class ConnectViewModel : ViewModelBase
    {
        // network functions
        private Client client;

        // store TextField input
        private string userName;
        private string iPAddress;
        private string port;

        public string UserName { get => userName; set { userName = value; OnPropertyChanged("UserName"); } }
        public string IPAddress { get => iPAddress; set { iPAddress = value; OnPropertyChanged("IPAddress"); } }
        public string Port { get => port; set { port = value; OnPropertyChanged("Port"); } }


        // used to update UI with Join/Host radiobuttons
        private Visibility iPLabelVisibility;
        private Visibility iPFieldVisibility;
        private Thickness usernameLabelMargin;
        private Thickness usernameFieldMargin;
        private Thickness mainButtonMargin;
        private string mainButtonContent;
        private ICommand mainButtonClick;
       
        public Visibility IPLabelVisibility { get => iPLabelVisibility; set { iPLabelVisibility = value; OnPropertyChanged("IPLabelVisibility"); } }
        public Visibility IPFieldVisibility { get => iPFieldVisibility; set { iPFieldVisibility = value; OnPropertyChanged("IPFieldVisibility"); } }
        public Thickness UsernameLabelMargin { get => usernameLabelMargin;  set { usernameLabelMargin = value; OnPropertyChanged("UsernameLabelMargin"); } }
        public Thickness UsernameFieldMargin { get => usernameFieldMargin;  set { usernameFieldMargin = value; OnPropertyChanged("UsernameFieldMargin"); } }
        public Thickness MainButtonMargin { get => mainButtonMargin; set { mainButtonMargin = value; OnPropertyChanged("MainButtonMargin"); } }
        public string MainButtonContent { get => mainButtonContent; set { mainButtonContent = value; OnPropertyChanged("MainButtonContent"); } }
        public ICommand MainButtonClick { get => mainButtonClick; set { mainButtonClick = value; OnPropertyChanged("MainButtonClick"); } }

       
        // navigation to ChatView command
        public ICommand JoinChatCommand { get;  }


        // button commands
        public ICommand JoinChatRadioCommand { get => new RelayCommand(JoinChatRadio); }
        public ICommand HostChatRadioCommand { get => new RelayCommand(HostChatRadio); }
        public ICommand JoinChatButtonCommand { get => new RelayCommand(JoinChatButton); }
        public ICommand HostChatButtonCommand { get => new RelayCommand(HostChatButton); }


        // constructor
        public ConnectViewModel(NavigationStore navigationStore)
        {
            JoinChatCommand = new JoinChatCommand(navigationStore);
            JoinChatRadio();  // set UI to JoinChat
        }


        // button functions
        private void JoinChatRadio()
        {
            IPLabelVisibility = Visibility.Visible;
            IPFieldVisibility = Visibility.Visible;
            UsernameLabelMargin = new Thickness(-98, 233, 0, 0);
            UsernameFieldMargin = new Thickness(0, 259, 0, 0);
            MainButtonMargin = new Thickness(0, 309, 0, 0);
            MainButtonContent = "Join Chat";
            MainButtonClick = JoinChatButtonCommand;
        }

        private void HostChatRadio()
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
                ResetClient();
                client.InitConnection();
            } 
        }

        internal void HostChatButton()
        {
            if (ValidatePort() && ValidateUserName())
            {
                iPAddress = "127.0.0.1";
                ResetClient();
                client.InitListening();
            }
        }

        private void ResetClient()
        {
            if (client != null)
            {
                client.Close();
            }

            client = new Client(port, iPAddress, userName);
            client.connectToChat += (source, _) => { JoinChatCommand.Execute(client); };
        }


        // validate TextField input
        private bool ValidatePort()
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

        private bool ValidateIP()
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

        private bool ValidateUserName()
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
