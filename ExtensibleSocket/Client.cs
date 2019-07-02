using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ExtensibleSocket
{
    public class Client
    {
        /// <summary>
        /// Encoding that used to encode strings to bytes and decode bytes to strings (by default it's unicode)
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// Client socket that used to communicate with server socket
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// Port (by default it's 1337)
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// IPAddress that client have to connect to
        /// </summary>
        public IPAddress IPAddress { get; set; }
        /// <summary>
        /// Is client started or not
        /// </summary>
        public bool Started { get; private set; }
        /// <summary>
        /// Is client connected to the server or not
        /// </summary>
        public bool Connected { get; private set; }

        #region constructors

        /// <summary>
        /// Constructor with default server settings
        /// </summary>
        public Client(IPAddress iPAddress)
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Started = false;
            IPAddress = iPAddress;

            Connected = false;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port)
        {
            Encoding = Encoding.Unicode;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Started = false;
            IPAddress = iPAddress;
            Connected = false;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port, Encoding encoding)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Started = false;
            IPAddress = iPAddress;
            Connected = false;
        }

        #endregion

        #region functions



        #endregion

        #region events
        #endregion

    }
}
