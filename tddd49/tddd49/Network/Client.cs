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


namespace tddd49.Network
{
    class Client
    {
        private IPAddress ipAddress;
        private int port;

        private Socket _socket;
        private IPEndPoint _ipEndPoint;

        private Thread ConnectThread;
        private Thread ListenThread;
        private Thread SendThread;
        private Thread RecieveThread;
        

        // updates ConnectViewModel/ChatViewModel on network events
        public EventHandler StartListenEvent;
        public EventHandler StopListenEvent;
        public EventHandler ChatRequestSentEvent;
        public EventHandler ChatRequestRecievedEvent;
        public EventHandler ChatRequestAcceptedEvent;
        public EventHandler ChatRequestDeclinedEvent;
        public EventHandler ChatMessageRecievedEvent;

        public Client(string portString, string ipString)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ConnectThread = new Thread((userName) => _Connect((string) userName));
            ConnectThread.IsBackground = true;

            ListenThread = new Thread(_Listen);
            ListenThread.IsBackground = true;

            RecieveThread = new Thread(_Recieve);
            RecieveThread.IsBackground = true;

            if (!IPAddress.TryParse(ipString, out ipAddress))
            {
                    // throw exception
            }
    
            if (!int.TryParse(portString, out port))
            { 
                // throw exception
            }

            _ipEndPoint = new IPEndPoint(ipAddress, port);
        }


        public void Connect(string userName)
        {
            ConnectThread.Start(userName);
        }

        private void _Connect(string userName)
        {
            try
            {
                _socket.Connect(_ipEndPoint);
            }
            catch
            {
                MessageBox.Show($"Unable to connect to {ipAddress}:{port}", "Error");
                return;
            }

            ChatRequestSentEvent?.Invoke(this, null);
            _SendPacket(PacketType.RequestChat, userName);
        }


        public void Listen()
        {
            if (!_socket.IsBound)
            {
                try
                {
                    _socket.Bind(_ipEndPoint);
                }
                catch
                {
                    MessageBox.Show($"Port {port} is already in use.", "Error");
                    return;
                }
            }
            ListenThread.Start();
        }

        private void _Listen()
        {
            // start listening
            _socket.Listen(1024);
            StartListenEvent?.Invoke(this, null);

            // wait for connection
            _socket = _socket.Accept();

            StopListenEvent?.Invoke(this, null);
            RecieveThread.Start();
        }


        public void SendPacket(String userName, string message = "")
        {

        }

        private void _SendPacket(PacketType type, string userName, string message = "")
        {
            if (_socket.Connected)
            {
                Packet packet = new Packet(type, userName, message);
                string jsonString = JsonSerializer.Serialize(packet);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

                try
                { 
                    _socket.Send(bytes);
                }
                catch
                {
                    MessageBox.Show("Send Error");
                    // throw exception ?
                }

            }
        }

        private void _Recieve()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                try
                {
                    _socket.Receive(bytes);
                    if (bytes.Count() > 0)
                    {
                        string jsonString = Encoding.UTF8.GetString(bytes).Trim((char) 0x00);
                        MessageBox.Show(jsonString);
                        MessageBox.Show(jsonString.Length.ToString());
                        MessageBox.Show(jsonString.Count().ToString());
                        JsonSerializerOptions jso = new JsonSerializerOptions();
                        jso.AllowTrailingCommas = true;
                        jso.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                        Packet packet = JsonSerializer.Deserialize<Packet>(jsonString, jso);
                        OnPacketRecieved(packet);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    StopListenEvent?.Invoke(this, null);
                    Close();
                    return;
                }
            }

        }

        private void OnPacketRecieved(Packet packet)
        {
            MessageBox.Show("OnPacketRecieved");
            if (packet.packetType == PacketType.RequestChat)
            {
                MessageBox.Show("Request Chat Message Recieved");
            }
            else if (packet.packetType == PacketType.AcceptChat)
            {

            }
            else if (packet.packetType == PacketType.DeclineChat)
            {

            }
            else if (packet.packetType == PacketType.ChatMessage)
            {

            }
        }

        public void Close()
        {
            Thread thread = new Thread(_close);
            thread.IsBackground = true;
            thread.Start();
        }

        private void _close()
        {
            _socket.Close();
            ListenThread.Abort();
            RecieveThread.Abort();
        }


    }
}
