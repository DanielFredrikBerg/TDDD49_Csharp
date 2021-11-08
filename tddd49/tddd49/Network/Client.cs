using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace tddd49.Network
{
    class Client
    {
        Socket _socket;
        IPEndPoint _ipEndPoint;

        public Client(string ipString, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip;
            if (!IPAddress.TryParse(ipString, out ip))
            {
                // throw exception
            }
            _ipEndPoint = new IPEndPoint(ip, port);
        }

        public void HostChat()
        {

        }

        public void JoinChat()
        {

        }

        public void SendMessage(String message)
        {

        }


    }
}
