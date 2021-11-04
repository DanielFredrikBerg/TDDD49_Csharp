using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tddd49.Command;

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

        public JoinHostViewModel()
        {
            Initialise();   
        }
        

        public Visibility IPLabelVisibility { get { return iPLabelVisibility; } set { iPLabelVisibility = value; OnPropertyChanged("IPLabelVisibility"); } }
        public Visibility IPFieldVisibility { get { return iPFieldVisibility; } set { iPFieldVisibility = value; OnPropertyChanged("IPFieldVisibility"); } }
        public Thickness UsernameLabelMargin { get { return usernameLabelMargin; } set { usernameLabelMargin = value; OnPropertyChanged("UsernameLabelMargin"); } }
        public Thickness UsernameFieldMargin { get { return usernameFieldMargin; } set { usernameFieldMargin = value; OnPropertyChanged("UsernameFieldMargin"); } }
        public Thickness MainButtonMargin { get { return mainButtonMargin; } set { mainButtonMargin = value; OnPropertyChanged("MainButtonMargin"); } }
        public string MainButtonContent { get { return mainButtonContent; } set { mainButtonContent = value; OnPropertyChanged("MainButtonContent"); } }


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

        internal void JoinChatRadio()
        {
            Initialise();
            Console.WriteLine("Join Chat Button Click");
        }

        internal void HostChatRadio()
        {
            IPLabelVisibility = Visibility.Hidden;
            IPFieldVisibility = Visibility.Hidden;
            UsernameLabelMargin = new Thickness(228, 170, 0, 0);
            UsernameFieldMargin = new Thickness(228, 196, 0, 0);
            MainButtonMargin = new Thickness(268, 246, 0, 0);
            MainButtonContent = "Host Chat";
            Console.WriteLine("Host Chat Button Click");
        }

        internal void Initialise()
        {
            IPLabelVisibility = Visibility.Visible;
            IPFieldVisibility = Visibility.Visible;
            UsernameLabelMargin = new Thickness(228, 233, 0, 0);
            UsernameFieldMargin = new Thickness(228, 259, 0, 0);
            MainButtonMargin = new Thickness(268, 309, 0, 0);
            MainButtonContent = "Join Chat";
        }
    }
}
