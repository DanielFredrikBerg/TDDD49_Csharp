using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Sockets;
using tddd49.Command;
using tddd49.Stores;
using tddd49.Network;
using System.Collections.ObjectModel;
using tddd49.Model;
using System.Windows.Controls;
using System.Windows;

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
        public ICommand SendChatMessage { get => new RelayCommand(SendMessageButton);
    }
        public ChatViewModel(NavigationStore navigationStore, Client client)
        {
            ExitChatCommand = new ExitChatCommand(navigationStore);
            this.client = client;
            client.recieveChatMessage += (packet, _) => { ReceiveChatMessage((Packet) packet); };
            client.disconnected += (sender, _) => { ConnectionLost(); };

            conversation = new ChatConversation(client.HostName, client.PeerName);
            ChatHistory.AddConversation(conversation);
            ChatMessage userJoinedMessage = new ChatMessage($"User {client.HostName} has joined the chat.", "System");
            conversation.AddMessage(userJoinedMessage);
            userJoinedMessage = new ChatMessage($"User {client.PeerName} has joined the chat.", "System");
            conversation.AddMessage(userJoinedMessage);
        }


        internal void AddChatMessage(String message)
        {
            ChatMessage chatMessage = new ChatMessage("user", message);
            conversation.AddMessage(chatMessage);
        }

        internal void SendMessageButton()
        {
            // send packet through Client
            if (message.Length > 0)
            {
                client.SendPacket(Packet.PacketType.ChatMessage, message);

                ChatMessage chatMessage = new ChatMessage(message, client.HostName);
                conversation.AddMessage(chatMessage);

                Message = "";

            }
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
            //ChatHistory.Save();
        }
        private void ExitChatButton()
        {
            ChatMessage userLeaveMessage = new ChatMessage($"User {client.HostName} has left the chat.", "System");
            conversation.AddMessage(userLeaveMessage);
            //ChatHistory.Save();

            client.Close();
            ExitChatCommand.Execute(null);
        }

    }
}
