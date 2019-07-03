using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        /// If true, then server would check if IPAddress is in allowed domains list, but not in IPAddresses list
        /// </summary>
        public bool DomainsCheck { get; set; }
        /// <summary>
        /// List of clients that are connected to the server
        /// </summary>
        public List<ServerClient> Clients { get; private set; }
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
        public ServerClient Client { get; private set; }
        /// <summary>
        /// Length of bytes that bytes, that you want to send, have split to (by default it's 4096)
        /// </summary>
        public int BufferSize { get; set; }

        #region constructors

        /// <summary>
        /// Constructor with default server settings
        /// </summary>
        public Server()
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<ServerClient>();
            ClientsLimit = 1000;
            Started = false;
            Client = null;
            BufferSize = 4096;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit)
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = 4096;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding)
        {
            Encoding = encoding;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = 4096;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = new List<string>();
            IPAddresses = new List<IPAddress>();
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = 4096;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port, List<string> domains)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = domains;
            IPAddresses = new List<IPAddress>();
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = 4096;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port, List<string> domains, List<IPAddress> iPAddresses)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = domains;
            IPAddresses = iPAddresses;
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = 4096;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port, List<string> domains, List<IPAddress> iPAddresses, int bufferSize)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = domains;
            IPAddresses = iPAddresses;
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = bufferSize;
            DomainsCheck = false;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Server(int clientsLimit, Encoding encoding, int port, List<string> domains, List<IPAddress> iPAddresses, int bufferSize, bool domainsCheck)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Domains = domains;
            IPAddresses = iPAddresses;
            Clients = new List<ServerClient>();
            ClientsLimit = clientsLimit;
            Started = false;
            Client = null;
            BufferSize = bufferSize;
            DomainsCheck = domainsCheck;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {

            });
        }

        #endregion

        #region events

        public class ErrorCaughtArgs
        {
            public ErrorCaughtArgs(Exception e) { Error = e; }
            public Exception Error { get; }
        }
        public delegate void ErrorCaughtEventHandler(object sender, ErrorCaughtArgs e);
        public event ErrorCaughtEventHandler ErrorCaughtEvent;

        public class ClientConnectedArgs
        {
            public ClientConnectedArgs(ServerClient c) { Client = c; }
            public ServerClient Client { get; }
        }
        public delegate void ClientConnectedEventHandler(object sender, ClientConnectedArgs e);
        public event ClientConnectedEventHandler ClientConnectedEvent;

        #endregion

        #region functions

        public StartResult Start()
        {
            StartResult sr = new StartResult(false, "", "", null, false);
            try
            {
                Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
                Socket.Listen(ClientsLimit);
                StartListener();
                Started = true;
                sr.Data = "Server started successfuly";
                sr.Error = null;
                sr.ErrorText = "";
                sr.HasError = false;
                sr.Success = true;
                return sr;
            }
            catch (Exception e)
            {
                sr.Data = e.ToString();
                sr.Success = false;
                sr.HasError = true;
                sr.Error = e;
                sr.ErrorText = e.ToString();
                return sr;
            }
        }

        public StopResult Stop()
        {
            StopResult sr = new StopResult(false, "", "", null, false);
            try
            {
                return sr;
            }
            catch (Exception e)
            {
                sr.Data = e.ToString();
                sr.Success = false;
                sr.HasError = true;
                sr.Error = e;
                sr.ErrorText = e.ToString();
                return sr;
            }
        }

        public RestartResult Restart()
        {
            RestartResult rr = new RestartResult(false, "", "", null, false);
            try
            {
                return rr;
            }
            catch (Exception e)
            {
                rr.Data = e.ToString();
                rr.Success = false;
                rr.HasError = true;
                rr.Error = e;
                rr.ErrorText = e.ToString();
                return rr;
            }
        }

        private async void StartListener()
        {
            await Task.Run(() => {
                while (Started)
                {
                    try
                    {
                        Socket newSocket = Socket.Accept();
                        if (IPAddresses.Count > 0 || Domains.Count > 0)
                        {
                            if (DomainsCheck)
                            {
                                if (!Domains.Contains(Dns.GetHostEntry(((IPEndPoint)newSocket.RemoteEndPoint).Address).HostName))
                                {
                                    try
                                    {
                                        newSocket.Disconnect(false);
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (!IPAddresses.Contains(((IPEndPoint)newSocket.RemoteEndPoint).Address))
                                {
                                    try
                                    {
                                        newSocket.Disconnect(false);
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        int id = 0;
                        if (Clients.Count > 0)
                        {
                            id = Clients.Last().ID + 1;
                        }
                        IPAddress ip = IPAddress.Parse(((IPEndPoint)newSocket.RemoteEndPoint).Address.ToString());
                        ServerClient client = new ServerClient(Port, newSocket);
                        client.IPAddress = ip;
                        client.ID = id;
                        client.SocketConnected();
                        if (Client == null)
                        {
                            Client = client;
                        }
                        ClientConnectedEvent(this, new ClientConnectedArgs(client));
                    }
                    catch (Exception e)
                    {
                        ErrorCaughtEvent(this, new ErrorCaughtArgs(e));
                    }
                }
            });
        }

        /// <summary>
        /// Function that sends data in bytes to client and returns SendResult class that contains different kinds of information
        /// </summary>
        public SendResult SendData(byte[] bytes)
        {
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            try
            {
                List<byte[]> parts = new List<byte[]>();
                if (bytes.Length <= BufferSize)
                {
                    parts.Add(bytes);
                }
                else
                {
                    parts = ByteArraySplit(bytes, BufferSize);
                }
                Client.Socket.Send(ConvertToBytes(parts.Count.ToString()));
                int size = 2;
                byte[] buffer = new byte[size];
                int len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "0")
                {
                    Client.Socket.Send(ConvertToBytes("1"));
                    size = 100;
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Client.Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    sr.BytesSent = 0;
                    sr.Data = output;
                    sr.ErrorText = output;
                    sr.HasError = true;
                    sr.Error = null;
                    sr.Success = false;
                    return sr;
                }
                for (int i = 0; i < parts.Count; i++)
                {
                    Client.Socket.Send(ConvertToBytes(parts[i].Length.ToString()));
                    buffer = new byte[2];
                    len = Client.Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Client.Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Client.Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                    Client.Socket.Send(parts[i]);
                    buffer = new byte[2];
                    len = Client.Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Client.Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Client.Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                }
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
                if (Client.SocketConnected())
                {
                    Client.Socket.Send(ConvertToBytes("0"));
                    int size = 2;
                    byte[] buffer = new byte[size];
                    int len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    string output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        Client.Socket.Send(ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString()));
                        size = 2;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        if (output == "1")
                        {
                            Client.Socket.Send(ConvertToBytes(e.ToString()));
                            size = 2;
                            buffer = new byte[size];
                            len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        }
                    }
                }
                sr.Success = false;
                sr.HasError = true;
                sr.ErrorText = e.ToString();
                sr.Error = e;
                sr.Data = e.ToString();
                sr.BytesSent = 0;
                return sr;
            }
        }
        /// <summary>
        /// Function that sends data in bytes to client and returns SendResult class that contains different kinds of information
        /// </summary>
        public SendResult SendData(string str)
        {
            byte[] bytes = ConvertToBytes(str);
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            try
            {
                List<byte[]> parts = new List<byte[]>();
                if (bytes.Length <= BufferSize)
                {
                    parts.Add(bytes);
                }
                else
                {
                    parts = ByteArraySplit(bytes, BufferSize);
                }
                Client.Socket.Send(ConvertToBytes(parts.Count.ToString()));
                int size = 2;
                byte[] buffer = new byte[size];
                int len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "0")
                {
                    Client.Socket.Send(ConvertToBytes("1"));
                    size = 100;
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Client.Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    sr.BytesSent = 0;
                    sr.Data = output;
                    sr.ErrorText = output;
                    sr.HasError = true;
                    sr.Error = null;
                    sr.Success = false;
                    return sr;
                }
                for (int i = 0; i < parts.Count; i++)
                {
                    Client.Socket.Send(ConvertToBytes(parts[i].Length.ToString()));
                    buffer = new byte[2];
                    len = Client.Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Client.Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Client.Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                    Client.Socket.Send(parts[i]);
                    buffer = new byte[2];
                    len = Client.Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Client.Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Client.Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                }
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
                if (Client.SocketConnected())
                {
                    Client.Socket.Send(ConvertToBytes("0"));
                    int size = 2;
                    byte[] buffer = new byte[size];
                    int len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    string output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        Client.Socket.Send(ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString()));
                        size = 2;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        if (output == "1")
                        {
                            Client.Socket.Send(ConvertToBytes(e.ToString()));
                            size = 2;
                            buffer = new byte[size];
                            len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        }
                    }
                }
                sr.Success = false;
                sr.HasError = true;
                sr.ErrorText = e.ToString();
                sr.Error = e;
                sr.Data = e.ToString();
                sr.BytesSent = 0;
                return sr;
            }
        }
        /// <summary>
        /// Function that receive data in bytes from client and returns ReceiveResult class that contains different kinds of information
        /// </summary>
        public ReceiveResult ReceiveData()
        {
            ReceiveResult sr = new ReceiveResult(false, "", "", null, false, 0);
            try
            {
                int size = 100;
                byte[] buffer = new byte[size];
                int len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                {
                    Client.Socket.Send(ConvertToBytes("1"));
                    size = 100;
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Client.Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    Client.Socket.Send(ConvertToBytes("1"));
                    sr.Data = Encoding.Unicode.GetString(buffer, 0, len);
                    sr.BytesReceived = 0;
                    sr.Bytes = null;
                    sr.Error = null;
                    sr.HasError = true;
                    sr.ErrorText = Encoding.Unicode.GetString(buffer, 0, len);
                    sr.Success = false;
                    return sr;
                }
                int partsCount = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                Client.Socket.Send(ConvertToBytes("1"));
                List<byte[]> parts = new List<byte[]>();
                for (int i = 0; i < partsCount; i++)
                {
                    size = 100;
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                    {
                        Client.Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Client.Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        Client.Socket.Send(ConvertToBytes("1"));
                        sr.Data = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesReceived = 0;
                        sr.Bytes = null;
                        sr.Error = null;
                        sr.HasError = true;
                        sr.ErrorText = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.Success = false;
                        return sr;
                    }
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Client.Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                    {
                        Client.Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Client.Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                        Client.Socket.Send(ConvertToBytes("1"));
                        sr.Data = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesReceived = 0;
                        sr.Bytes = null;
                        sr.Error = null;
                        sr.HasError = true;
                        sr.ErrorText = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.Success = false;
                        return sr;
                    }
                    Client.Socket.Send(ConvertToBytes("1"));
                    parts.Add(buffer);
                }
                byte[] bytes = ConcatToOneByteArray(parts.ToArray());
                sr.Data = ConvertToString(bytes);
                sr.BytesReceived = bytes.Length;
                sr.Bytes = bytes;
                sr.Error = null;
                sr.HasError = false;
                sr.ErrorText = "";
                sr.Success = true;
                return sr;
            }
            catch (Exception e)
            {
                Client.Socket.Send(ConvertToBytes("0"));
                int size = 2;
                byte[] buffer = new byte[size];
                int len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "1")
                {
                    Client.Socket.Send(ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString()));
                    size = 2;
                    buffer = new byte[size];
                    len = Client.Socket.Receive(buffer, size, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        Client.Socket.Send(ConvertToBytes(e.ToString()));
                    }
                }
                sr.Success = false;
                sr.HasError = true;
                sr.ErrorText = e.ToString();
                sr.Error = e;
                sr.Data = e.ToString();
                sr.BytesReceived = 0;
                return sr;
            }
        }

        /// <summary>
        /// Function that converts bytes to string
        /// </summary>
        public string ConvertToString(byte[] bytes)
        {
            try
            {
                return Encoding.GetString(bytes);
            }
            catch (Exception e)
            {
                ErrorCaughtEvent(this, new ErrorCaughtArgs(e));
                return null;
            }
        }

        /// <summary>
        /// Function that converts string to bytes
        /// </summary>
        public byte[] ConvertToBytes(string str)
        {
            try
            {
                return Encoding.GetBytes(str);
            }
            catch (Exception e)
            {
                ErrorCaughtEvent(this, new ErrorCaughtArgs(e));
                return null;
            }
        }

        /// <summary>
        /// Function that splits array of bytes by certain count of bytes
        /// </summary>
        public List<byte[]> ByteArraySplit(byte[] byteData, long BufferSize)
        {
            try
            {
                List<byte[]> chunks = byteData.Select((value, index) => new { PairNum = Math.Floor(index / (double)BufferSize), value }).GroupBy(pair => pair.PairNum).Select(grp => grp.Select(g => g.value).ToArray()).ToList();
                return chunks;
            }
            catch (Exception e)
            {
                ErrorCaughtEvent(this, new ErrorCaughtArgs(e));
                return null;
            }
        }

        /// <summary>
        /// Function that joins all arrays of bytes into one array of bytes
        /// </summary>
        public byte[] ConcatToOneByteArray(params byte[][] arrays)
        {
            try
            {
                byte[] rv = new byte[arrays.Sum(a => a.Length)];
                int offset = 0;
                foreach (byte[] array in arrays)
                {
                    Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                    offset += array.Length;
                }
                return rv;
            }
            catch (Exception e)
            {
                ErrorCaughtEvent(this, new ErrorCaughtArgs(e));
                return null;
            }
        }
        #endregion
    }
}
