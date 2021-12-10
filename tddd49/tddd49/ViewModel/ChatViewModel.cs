using System;
using System.Windows.Input;
using tddd49.Command;
using tddd49.Stores;
using tddd49.Network;
using tddd49.Model;


namespace tddd49.ViewModel
{
    class ChatViewModel : ViewModelBase
    {
        private Client client;

        private String message;
        private ChatConversation conversation;
        
        public String Message { get => message; set { message = value; OnPropertyChanged("Message"); } }
        public ChatConversation Conversation { get => conversation; }

        public ICommand ExitChatCommand { get; }
        public ICommand ExitChatButtonCommand { get => new RelayCommand(ExitChatButton); }
        public ICommand SendChatMessage { get => new RelayCommand(SendMessageButton);}
        public ICommand SendBuzz { get => new RelayCommand(SendBuzzButton); }
        public ChatViewModel(NavigationStore navigationStore, Client client)
        {
            ExitChatCommand = new ExitChatCommand(navigationStore);
            this.client = client;
            client.recieveChatMessage += (packet, _) => { ReceiveChatMessage((Packet) packet); };
            client.disconnected += (sender, _) => { ConnectionLost(); };

            conversation = new ChatConversation(client.HostName, client.PeerName);
            App.Current.Dispatcher.Invoke(() => ChatHistory.AddConversation(Conversation));
            ChatMessage userJoinedMessage = new ChatMessage($"User {client.HostName} has joined the chat.", "System");
            conversation.AddMessage(userJoinedMessage);
            userJoinedMessage = new ChatMessage($"User {client.PeerName} has joined the chat.", "System");
            conversation.AddMessage(userJoinedMessage);
        }


        private void AddChatMessage(String message)
        {
            ChatMessage chatMessage = new ChatMessage("user", message);
            conversation.AddMessage(chatMessage);
        }

        private void SendMessageButton()
        {
            // send packet through Client
            if (message != null && message.Length > 0)
            {
                client.SendPacket(Packet.PacketType.ChatMessage, message);

                ChatMessage chatMessage = new ChatMessage(message, client.HostName);
                conversation.AddMessage(chatMessage);

                Message = "";
            }
        }

        private void SendBuzzButton()
        {
            client.SendPacket(Packet.PacketType.Buzz);
        }

        private void ReceiveChatMessage(Packet packet)
        {
            ChatMessage chatMessage = new ChatMessage(packet.message, packet.userName);
            conversation.AddMessage(chatMessage);
 
        }

        private void ConnectionLost()
        {
            ChatMessage userLeaveMessage = new ChatMessage($"User {client.PeerName} has left the chat.", "System");
            conversation.AddMessage(userLeaveMessage);
            ChatHistory.Save();
        }
        private void ExitChatButton()
        {
            ChatMessage userLeaveMessage = new ChatMessage($"User {client.HostName} has left the chat.", "System");
            conversation.AddMessage(userLeaveMessage);
            ChatHistory.Save();

            client.Close();
            ExitChatCommand.Execute(null);
        }

    }
}
