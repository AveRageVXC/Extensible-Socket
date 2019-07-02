using System;
using System.Net;
using System.Net.Sockets;

namespace ExtensibleSocket
{
    public class ServerClient
    {
        /// <summary>
        /// ServerClient's ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ServerClient socket that used to communicate with server socket
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// Port that ServerClient is connected to
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// ServerClient's IPAddress
        /// </summary>
        public IPAddress IPAddress { get; set; }
        /// <summary>
        /// Is ServerClient connected to the server or not
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// ServerClient constructor
        /// </summary>
        public ServerClient(int port, Socket socket)
        {
            Port = port;
            Socket = socket;
        }

        /// <summary>
        /// Function that shows if ServerClient is connected
        /// </summary>
        public bool SocketConnected()
        {
            try
            {
                bool part1 = Socket.Poll(1000, SelectMode.SelectRead);
                bool part2 = (Socket.Available == 0);
                if (part1 && part2)
                {
                    Connected = false;
                    return false;
                }
                else
                {
                    Connected = true;
                    return true;
                }
            }
            catch (Exception)
            {
                Connected = false;
                return false;
            }
        }
    }
}
