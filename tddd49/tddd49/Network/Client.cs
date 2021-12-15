using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;
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

        public EventHandler unableToConnect;
        public EventHandler portAlreadyInUse;
        public EventHandler listeningForConnection;
        public EventHandler connectionLost;
        public EventHandler chatRequest;
        public EventHandler chatAccepted;
        public EventHandler chatDeclined;

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
            }
            catch
            {
                unableToConnect?.Invoke(this, null);
                return;
            }

            SendPacket(PacketType.RequestChat);
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
                    portAlreadyInUse?.Invoke(this, null);
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

            listeningForConnection?.Invoke(this, null);

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
                    connectionLost?.Invoke(this, null);
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
                chatRequest?.Invoke(peerName, null);
            }
            else if (packet.packetType == PacketType.AcceptChat)
            {
                peerName = packet.userName;
                chatAccepted?.Invoke(this, null);
                connectToChat?.Invoke(this, null);
            }
            else if (packet.packetType == PacketType.DeclineChat)
            {
                peerName = packet.userName;
                chatDeclined?.Invoke(this, null);
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
