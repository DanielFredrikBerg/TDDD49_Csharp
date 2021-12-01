using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.Model
{
    class ChatConversation
    {

       private ObservableCollection<ChatMessage> messages;
       // time var

       public ObservableCollection<ChatMessage> Messages { get => messages; }

        public ChatConversation()
        {
            messages = new ObservableCollection<ChatMessage>();
        }

        
        public void addMessage(ChatMessage message)
        {
            messages.Add(message);
        }
        


    }
}
