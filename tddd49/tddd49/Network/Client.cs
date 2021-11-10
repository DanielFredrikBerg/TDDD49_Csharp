using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.Threading;

namespace tddd49.Network
{
    class Client
    {
        
        IPAddress ipAddress;
        int port;

        Socket _socket;
        IPEndPoint _ipEndPoint;

        Thread ConnectThread;
        Thread ListenThread;
        Thread SendThread;
        Thread RecieveThread;

        public bool isListening;

        public Client(string portString, string ipString = null)
        {
     
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ConnectThread = new Thread(_connect);
            ListenThread = new Thread(_listen);
            RecieveThread = new Thread(_recieve);

            isListening = false;

            if (ipString == null)
            {
                IPAddress.TryParse("127.0.0.1", out ipAddress);
            }
            else
            { 
                if (!IPAddress.TryParse(ipString, out ipAddress))
                {
                    // throw exception
                }
            }
 
            if (!int.TryParse(portString, out port))
            { 
                // throw exception
            }

            _ipEndPoint = new IPEndPoint(ipAddress, port);
        }

        public void Listen()
        {
           ListenThread.Start();

            //MessageBox.Show("Listening...");
        }

        public void Connect()
        {
            if (ConnectThread.IsAlive)
            {    
                ConnectThread.Abort();
                ConnectThread = new Thread(_connect);
            }

            Close();
           
            ConnectThread.Start();
        }

        public void Send(String message)
        {

        }

        public void Recieve()
        {
            RecieveThread.Start();
        }

 
        private void _connect()
        {
            try
            {
                _socket.Connect(_ipEndPoint);
                //Thread.Sleep(1000);
            }
            catch
            {
                MessageBox.Show($"Unable to connect to {ipAddress}:{port}");
            }

            MessageBox.Show("Connected: " + _socket.Connected.ToString());
        }

        private void _listen()
        {

            if (!_socket.IsBound)
            {
                try
                {
                    _socket.Bind(_ipEndPoint);
                }
                catch
                {
                    MessageBox.Show($"port {port} is already in use.");
                    isListening = false;
                    return;
                }
                
            }

            // start listening
            _socket.Listen(1024);
            isListening = true;

            // wait for connection
            _socket = _socket.Accept();

            MessageBox.Show("Connected: " + _socket.Connected.ToString());
        }

        private void _send()
        {

        }

        private void _recieve()
        {
            byte[] bytes = new byte[1024];
            try
            {
                _socket.Receive(bytes);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Close()
        {
            //_socket.Close();
            Thread thread = new Thread(_close);
            thread.Start();
            
            /*
            if (_socket.Connected)
            {
                _socket.LingerState = new LingerOption(false, 0);
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Disconnect(false);
            }
            */
        }

        private void _close()
        {
            _socket.Close();
            ListenThread.Abort();
            RecieveThread.Abort();
        }


    }
}
