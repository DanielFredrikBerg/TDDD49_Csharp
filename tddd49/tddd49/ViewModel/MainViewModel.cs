using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tddd49.ViewModel
{
    sealed class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {

        }

        /*
        private void JoinChatRadioClick(object sender, RoutedEventArgs e)
        {
            MainButton.Content = "Join Chat";

            IPField.IsEnabled = true;
            IPField.Visibility = Visibility.Visible;
            IPLabel.Visibility = Visibility.Visible;

            UsernameField.Margin = new Thickness(228, 259, 0, 0);
            UsernameLabel.Margin = new Thickness(228, 233, 0, 0);

            MainButton.Margin = new Thickness(268, 309, 0, 0);
            MainButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(HostChat));
            MainButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(JoinChat));
        }

        private void HostChatRadioClick(object sender, RoutedEventArgs e)
        {
            MainButton.Content = "Host Chat";

            IPField.IsEnabled = false;
            IPField.Visibility = Visibility.Hidden;
            IPLabel.Visibility = Visibility.Hidden;

            UsernameField.Margin = new Thickness(228, 196, 0, 0);
            UsernameLabel.Margin = new Thickness(228, 170, 0, 0);

            MainButton.Margin = new Thickness(268, 246, 0, 0);
            MainButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(HostChat));
        }

        private void JoinChat(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Join Chat Button Click");
        }

        private void HostChat(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Host Chat Button Click");
        }
        */


    }
}
