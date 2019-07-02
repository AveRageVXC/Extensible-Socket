using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtensibleSocket
{
    /// <summary>
	/// Server class with socket and a lot of functions that allow you to get connection from clients and build communication with client's socket
	/// </summary>
    public class Server
    {
        /// <summary>
        /// Encoding that used to encode strings to bytes and decode bytes to strings (by default it's unicode)
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// Server socket that used to communicate with client socket
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// Port (by default it's 1337)
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// List of IPAddresses that are allowed to be connected to the server (leave it empty to allow any IPAddress to be connected)
        /// </summary>
        public List<IPAddress> IPAddresses { get; set; }
        /// <summary>
        /// List of domains that are allowed to be connected to the server (leave it empty to allow any domain to be connected)
        /// </summary>
        public List<string> Domains { get; set; }
        /// <summary>
        /// List of clients that are connected to the server
        /// </summary>
        public List<Client> Clients { get; private set; }
        /// <summary>
        /// Limit of clients that can connect to the server
        /// </summary>
        public int ClientsLimit { get; set; }
        /// <summary>
        /// Is server started or not
        /// </summary>
        public bool Started { get; private set; }
        /// <summary>
        /// Current client that server should work with
        /// </summary>
        public Client Client { get; private set; }
        private bool busySend = false;

        #region constructors

        /// <summary>
        /// Constructor with default server settings
        /// </summary>
        public Server()
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<Client>();
            ClientsLimit = 1000;
            Started = false;
            Client = null;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit)
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<Client>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding)
        {
            Encoding = encoding;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<Client>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<Client>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port, List<string> domains)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Domains = domains;
            IPAddresses = new List<IPAddress>();
            Clients = new List<Client>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port, List<string> domains, List<IPAddress> iPAddresses)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Domains = domains;
            IPAddresses = iPAddresses;
            Clients = new List<Client>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
        }

        #endregion

        #region functions

        /// <summary>
        /// Function that sends data in bytes to client and returns SendResult class that contains different kinds of information
        /// </summary>
        public SendResult SendData(byte[] bytes)
        {
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            try
            {
                sr.Data = "Data successfully sent";
                sr.BytesSent = bytes.Length;
                sr.Error = null;
                sr.HasError = false;
                sr.ErrorText = "";
                sr.Success = true;
                return sr;
            }
            catch (Exception e)
            {
                sr.Success = false;
                sr.HasError = true;
                sr.ErrorText = e.ToString();
                sr.Error = e;
                sr.Data = e.ToString();
                sr.BytesSent = 0;
                return sr;
            }
        }
    }

    #endregion

}
