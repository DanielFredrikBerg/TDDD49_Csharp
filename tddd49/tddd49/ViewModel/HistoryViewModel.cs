using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tddd49.Command;
using tddd49.Model;

namespace tddd49.ViewModel
{
    class HistoryViewModel : ViewModelBase
    {

        private String searchWord;
        private ChatConversation selectedConversation;
        private ObservableCollection<ChatConversation> conversationList;

        public String SearchWord
        { 
            get => searchWord; 
            set 
            { 
                searchWord = value;
                List<ChatConversation> cl = ChatHistory.Conversations.Where(x => x.peerName.Contains(searchWord)).ToList();
                ConversationList = new ObservableCollection<ChatConversation>(cl);
                OnPropertyChanged("SearchWord"); 
            } 
        }
        public ChatConversation SelectedConversation { get => selectedConversation; set { selectedConversation = value; OnPropertyChanged("SelectedConversation"); } }
        public ObservableCollection<ChatConversation> ConversationList { get => conversationList; set { conversationList = value; OnPropertyChanged("ConversationList"); } }

        public HistoryViewModel()
        {
            conversationList = ChatHistory.Conversations;
        }
    }
}
