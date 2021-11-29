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

        private Socket sendSocket;
        private Socket recieveSocket;
        private IPEndPoint ipEndPoint;

        private string hostName;
        private string peerName;

        private Task listenTask;

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

            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void InitConnection()
        {
            try
            {
                sendSocket.Connect(ipEndPoint);
            }
            catch
            {
                MessageBox.Show($"Unable to connect to {ipAddress}:{port}", "Error");
                return;
            }

            SendPacket(PacketType.RequestChat);

            Task listenTask = Task.Run(() => Listen());
        }


        public async void InitListening()
        {
            if (!sendSocket.IsBound)
            {
                try
                {
                    sendSocket.Bind(ipEndPoint);
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
            sendSocket.Listen(1024);
            MessageBox.Show("listening on ...");

            // wait for connection
            recieveSocket = sendSocket.Accept();

            Task listenTask = Task.Run(() => Recieve());
        }

        public void SendPacket(PacketType type, string message = "")
        {
            if (sendSocket.Connected)
            {
                Packet packet = new Packet(type, hostName, message);
                string jsonString = JsonSerializer.Serialize(packet);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

                try
                { 
                    sendSocket.Send(bytes);
                }
                catch
                {
                    MessageBox.Show("Connection Lost");
                    // throw exception ?
                }

            }
        }

        private void Recieve()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                try
                {
                    sendSocket.Receive(bytes);
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
                    Close();
                    return;
                }
            }

        }

        private void OnPacketRecieved(Packet packet)
        {
            if (packet.packetType == PacketType.RequestChat)
            {
                if (MessageBox.Show($"{packet.userName} Wants to Chat, Accept ?", "Chat Request", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SendPacket(PacketType.AcceptChat);
     
                }
                else
                {
                    SendPacket(PacketType.DeclineChat);
                }
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
            sendSocket.Close();
            recieveSocket.Close();
        }


    }
}
