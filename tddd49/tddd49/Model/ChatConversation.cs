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
        public String hostName;
        public String peerName;
        public DateTime dateTime;

        public ObservableCollection<ChatMessage> Messages { get => messages; }

        public ChatConversation(String hostName, String peerName)
        {
            this.hostName = hostName;
            this.peerName = peerName;
            dateTime = DateTime.Now;
            messages = new ObservableCollection<ChatMessage>();
        }

       
        public void AddMessage(ChatMessage message)
        {
            messages.Add(message);
        }

        public override string ToString()
        {
            return dateTime + ":\n" + "Username: " + hostName + "\n" + "Peername: " + peerName;
        }


    }
}
