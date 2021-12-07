using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.Threading;
using System.ComponentModel;
using static tddd49.Network.Packet;
using System.Media;

namespace tddd49.Network
{
    class Client
    {
        private IPAddress ipAddress;
        private int port;

        private Socket socket;
        private IPEndPoint ipEndPoint;

        private string hostName;
        private string peerName;

        public EventHandler connectToChat;
        public EventHandler recieveChatMessage;
        public EventHandler disconnected;
        public String HostName { get => hostName; }
        public String PeerName { get => peerName; }

        public Client(string portStr, string ipStr, string hostName)
        {
            this.hostName = hostName;

            if (!IPAddress.TryParse(ipStr, out ipAddress))
            {
                    // throw exception
            }
    
            if (!int.TryParse(portStr, out port))
            { 
                // throw exception
            }

            ipEndPoint = new IPEndPoint(ipAddress, port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void InitConnection()
        {
            try
            {
                socket.Connect(ipEndPoint);
                //MessageBox.Show("Init connection post connect");
            }
            catch
            {
                MessageBox.Show($"Unable to connect to {ipAddress}:{port}", "Error");
                return;
            }

            SendPacket(PacketType.RequestChat);

            //Task listenTask = Task.Run(() => Listen());
            Task.Run(Receive);
        }


        public async void InitListening()
        {
            if (!socket.IsBound)
            {
                try
                {
                    socket.Bind(ipEndPoint);
                }
                catch
                {
                    MessageBox.Show($"Port {port} is already in use.", "Error");
                    return;
                }
            }
            await Task.Run(Listen);
            //await listenTask.
        }

        private void Listen()
        {
            // start listening
            socket.Listen(1024);
            MessageBox.Show($"Listening for connections on {ipAddress}:{port}");

            // wait for connection
            socket = socket.Accept();

            //Task listenTask = Task.Run(() => Recieve());
            Task.Run(Receive);
        }

        public void SendPacket(PacketType type, string message = "")
        {
            if (socket.Connected)
            {
                Packet packet = new Packet(type, hostName, message);
                string jsonString = JsonSerializer.Serialize(packet);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

                try
                { 
                    socket.Send(bytes);
                }
                catch
                {
                    MessageBox.Show("Connection Lost");
                    // throw exception ?
                }

            }
        }

        private void Receive()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                try
                {
                    socket.Receive(bytes);
                    if (bytes.Count() > 0)
                    {
                        string jsonString = Encoding.UTF8.GetString(bytes).Trim((char) 0x00);
                        JsonSerializerOptions jso = new JsonSerializerOptions();
                        jso.AllowTrailingCommas = true;
                        jso.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                        Packet packet = JsonSerializer.Deserialize<Packet>(jsonString, jso);
                        OnPacketRecieved(packet);
                    }

                }
                catch (Exception e)
                {
                    App.Current.Dispatcher.Invoke(() => disconnected?.Invoke(this, null));
                    Console.Out.WriteLine(e.Message);
                    Close();
                    return;
                }
            }
        }

        private void OnPacketRecieved(Packet packet)
        {
            if (packet.packetType == PacketType.RequestChat)
            {
                peerName = packet.userName;
                if (MessageBox.Show($"{packet.userName} Wants to Chat, Accept ?", "Chat Request", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SendPacket(PacketType.AcceptChat);
                    connectToChat?.Invoke(this, null);
                }
                else
                {
                    SendPacket(PacketType.DeclineChat);
                }
            }
            else if (packet.packetType == PacketType.AcceptChat)
            {
                MessageBox.Show("Chat request was accepted.");
                peerName = packet.userName;
                connectToChat?.Invoke(this, null);
            }
            else if (packet.packetType == PacketType.DeclineChat)
            {
                MessageBox.Show("Chat request was declined.");
                peerName = packet.userName;
            }
            else if (packet.packetType == PacketType.ChatMessage)
            {
                App.Current.Dispatcher.Invoke(() => recieveChatMessage?.Invoke(packet, null));
            }
            else if (packet.packetType == PacketType.Buzz)
            {
                SoundPlayer player = new SoundPlayer(@"wario_sound.wav");
                player.Play();
            }
        }

        public void Close()
        {
            Task.Run(() => socket.Shutdown(SocketShutdown.Both));
        }
    }
}
