using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Port { get; set; }
        /// <summary>
        /// IPAddress that client have to connect to
        /// </summary>
        public IPAddress IPAddress { get; set; }
        /// <summary>
        /// Is client started or not
        /// </summary>
        public bool Started { get; private set; }
        /// <summary>
        /// Length of bytes that bytes, that you want to send, have split to (by default it's 4096)
        /// </summary>
        public int BufferSize { get; set; }
        public NetworkStream NetworkStream { get; set; }
        /// <summary>
        /// Can receive and send functions be timeout
        /// </summary>
        public bool CanTimeout { get; set; }
        /// <summary>
        /// Time in milliseconds, after that time, receive function would be canceled and return error (If CanTimeout is true, then it works)
        /// </summary>
        public int ReceiveTimeout { get; set; }
        /// <summary>
        /// Time in milliseconds, after that time, send function would be canceled and return error (If CanTimeout is true, then it works)
        /// </summary>
        public int SendTimeout { get; set; }

        #region constructors

        /// <summary>
        /// Constructor with default server settings
        /// </summary>
        public Client(IPAddress iPAddress)
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = 4096;
            CanTimeout = false;
            ReceiveTimeout = -1;
            SendTimeout = -1;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port)
        {
            Encoding = Encoding.Unicode;
            Port = port;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = 4096;
            CanTimeout = false;
            ReceiveTimeout = -1;
            SendTimeout = -1;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port, Encoding encoding)
        {
            Encoding = encoding;
            Port = port;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = 4096;
            CanTimeout = false;
            ReceiveTimeout = -1;
            SendTimeout = -1;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port, Encoding encoding, int bufferSize)
        {
            Encoding = encoding;
            Port = port;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = bufferSize;
            CanTimeout = false;
            ReceiveTimeout = -1;
            SendTimeout = -1;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port, Encoding encoding, int bufferSize, bool canTimeout)
        {
            Encoding = encoding;
            Port = port;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = bufferSize;
            CanTimeout = canTimeout;
            ReceiveTimeout = -1;
            SendTimeout = -1;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port, Encoding encoding, int bufferSize, bool canTimeout, int receiveTimeout)
        {
            Encoding = encoding;
            Port = port;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = bufferSize;
            CanTimeout = canTimeout;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = -1;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public Client(IPAddress iPAddress, int port, Encoding encoding, int bufferSize, bool canTimeout, int receiveTimeout, int sendTimeout)
        {
            Encoding = encoding;
            Port = port;
            Started = false;
            IPAddress = iPAddress;
            BufferSize = bufferSize;
            CanTimeout = canTimeout;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        #endregion

        #region functions

        #region fundamental functions

        /// <summary>
        /// Function that disconnects client from server
        /// </summary>
        public Result Disconnect()
        {
            Result r = new Result(false, "", "", null, false);
            try
            {
                Socket = null;
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = null;
                Started = false;
                r.Data = "Client successfuly disconnected";
                r.Error = null;
                r.ErrorText = "";
                r.HasError = false;
                r.Success = true;
                return r;
            }
            catch (Exception e)
            {
                r.Data = e.ToString();
                r.Error = e;
                r.ErrorText = e.ToString();
                r.Success = false;
                r.HasError = true;
                return r;
            }
        }

        /// <summary>
        /// Function that connects client to server
        /// </summary>
        public ConnectResult Connect()
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = new NetworkStream(Socket, true);
                Started = true;
                cr.Data = "Client successfuly connected";
                cr.Error = null;
                cr.ErrorText = "";
                cr.HasError = false;
                cr.Success = true;
                return cr;
            }
            catch (Exception e)
            {
                cr.Data = e.ToString();
                cr.Error = e;
                cr.ErrorText = e.ToString();
                cr.Success = false;
                cr.HasError = true;
                return cr;
            }
        }

        /// <summary>
        /// Function that connects client to server
        /// </summary>
        public ConnectResult Connect(IPAddress iPAddress)
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress = iPAddress;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = new NetworkStream(Socket, true);
                cr.Data = "Client successfuly connected";
                cr.Error = null;
                cr.ErrorText = "";
                cr.HasError = false;
                cr.Success = true;
                return cr;
            }
            catch (Exception e)
            {
                cr.Data = e.ToString();
                cr.Error = e;
                cr.ErrorText = e.ToString();
                cr.Success = false;
                cr.HasError = true;
                return cr;
            }
        }

        /// <summary>
        /// Function that connects client to server
        /// </summary>
        public ConnectResult Connect(IPAddress iPAddress, int port)
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress = iPAddress;
                Port = port;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = new NetworkStream(Socket, true);
                cr.Data = "Client successfuly connected";
                cr.Error = null;
                cr.ErrorText = "";
                cr.HasError = false;
                cr.Success = true;
                return cr;
            }
            catch (Exception e)
            {
                cr.Data = e.ToString();
                cr.Error = e;
                cr.ErrorText = e.ToString();
                cr.Success = false;
                cr.HasError = true;
                return cr;
            }
        }

        /// <summary>
        /// Function that reconnects client to server
        /// </summary>
        public ConnectResult Reconnect()
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = new NetworkStream(Socket, true);
                cr.Data = "Client successfuly connected";
                cr.Error = null;
                cr.ErrorText = "";
                cr.HasError = false;
                cr.Success = true;
                return cr;
            }
            catch (Exception e)
            {
                cr.Data = e.ToString();
                cr.Error = e;
                cr.ErrorText = e.ToString();
                cr.Success = false;
                cr.HasError = true;
                return cr;
            }
        }

        /// <summary>
        /// Function that reconnects client to server
        /// </summary>
        public ConnectResult Reconnect(IPAddress iPAddress)
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress = iPAddress;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = new NetworkStream(Socket, true);
                cr.Data = "Client successfuly connected";
                cr.Error = null;
                cr.ErrorText = "";
                cr.HasError = false;
                cr.Success = true;
                return cr;
            }
            catch (Exception e)
            {
                cr.Data = e.ToString();
                cr.Error = e;
                cr.ErrorText = e.ToString();
                cr.Success = false;
                cr.HasError = true;
                return cr;
            }
        }

        /// <summary>
        /// Function that reconnects client to server
        /// </summary>
        public ConnectResult Reconnect(IPAddress iPAddress, int port)
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress = iPAddress;
                Port = port;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
                NetworkStream = new NetworkStream(Socket, true);
                cr.Data = "Client successfuly connected";
                cr.Error = null;
                cr.ErrorText = "";
                cr.HasError = false;
                cr.Success = true;
                return cr;
            }
            catch (Exception e)
            {
                cr.Data = e.ToString();
                cr.Error = e;
                cr.ErrorText = e.ToString();
                cr.Success = false;
                cr.HasError = true;
                return cr;
            }
        }

        #endregion

        #region main functions

        /// <summary>
        /// Function that sends data in bytes to server and returns SendResult class that contains different kinds of information
        /// </summary>
        public SendResult SendData(byte[] bytes)
        {
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            try
            {
                if (CanTimeout)
                {
                    NetworkStream.ReadTimeout = ReceiveTimeout;
                    NetworkStream.WriteTimeout = SendTimeout;
                }
                else
                {
                    NetworkStream.ReadTimeout = -1;
                    NetworkStream.WriteTimeout = -1;
                }
                List<byte[]> parts = new List<byte[]>();
                if (bytes.Length <= BufferSize)
                {
                    parts.Add(bytes);
                }
                else
                {
                    parts = ByteArraySplit(bytes, BufferSize);
                }
                byte[] data = ConvertToBytes(parts.Count.ToString());
                NetworkStream.Write(data, 0, data.Length);
                int size = 2;
                byte[] buffer = new byte[size];
                int len = NetworkStream.Read(buffer, 0, buffer.Length);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "0")
                {
                    data = ConvertToBytes("1");
                    NetworkStream.Write(data, 0, data.Length);
                    size = 100;
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    data = ConvertToBytes("1");
                    NetworkStream.Write(data, 0, data.Length);
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
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
                    data = ConvertToBytes(parts[i].Length.ToString());
                    NetworkStream.Write(data, 0, data.Length);
                    buffer = new byte[2];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        size = 100;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                    NetworkStream.Write(parts[i], 0, parts[i].Length);
                    buffer = new byte[2];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        size = 100;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
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
            catch (TimeoutException e)
            {
                sr.Success = false;
                sr.HasError = true;
                sr.ErrorText = e.ToString();
                sr.Error = e;
                sr.Data = e.ToString();
                sr.BytesSent = 0;
                return sr;
            }
            catch (Exception e)
            {
                try
                {
                    byte[] data = ConvertToBytes("0");
                    NetworkStream.Write(data, 0, data.Length);
                    int size = 2;
                    byte[] buffer = new byte[size];
                    int len = NetworkStream.Read(buffer, 0, buffer.Length);
                    string output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        data = ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString());
                        NetworkStream.Write(data, 0, data.Length);
                        size = 2;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        if (output == "1")
                        {
                            data = ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString());
                            NetworkStream.Write(data, 0, data.Length);
                            size = 2;
                            buffer = new byte[size];
                            len = NetworkStream.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
                catch (Exception) { }
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
        /// Function that sends data in bytes to server and returns SendResult class that contains different kinds of information
        /// </summary>
        public SendResult SendData(string str)
        {
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            byte[] bytes = ConvertToBytes(str);
            try
            {
                if (CanTimeout)
                {
                    NetworkStream.ReadTimeout = ReceiveTimeout;
                    NetworkStream.WriteTimeout = SendTimeout;
                }
                else
                {
                    NetworkStream.ReadTimeout = -1;
                    NetworkStream.WriteTimeout = -1;
                }
                List<byte[]> parts = new List<byte[]>();
                if (bytes.Length <= BufferSize)
                {
                    parts.Add(bytes);
                }
                else
                {
                    parts = ByteArraySplit(bytes, BufferSize);
                }
                byte[] data = ConvertToBytes(parts.Count.ToString());
                NetworkStream.Write(data, 0, data.Length);
                int size = 2;
                byte[] buffer = new byte[size];
                int len = NetworkStream.Read(buffer, 0, buffer.Length);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "0")
                {
                    data = ConvertToBytes("1");
                    NetworkStream.Write(data, 0, data.Length);
                    size = 100;
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    data = ConvertToBytes("1");
                    NetworkStream.Write(data, 0, data.Length);
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
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
                    data = ConvertToBytes(parts[i].Length.ToString());
                    NetworkStream.Write(data, 0, data.Length);
                    buffer = new byte[2];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        size = 100;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                    NetworkStream.Write(parts[i], 0, parts[i].Length);
                    buffer = new byte[2];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        size = 100;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
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
            catch (TimeoutException e)
            {
                sr.Success = false;
                sr.HasError = true;
                sr.ErrorText = e.ToString();
                sr.Error = e;
                sr.Data = e.ToString();
                sr.BytesSent = 0;
                return sr;
            }
            catch (Exception e)
            {
                try
                {
                    byte[] data = ConvertToBytes("0");
                    NetworkStream.Write(data, 0, data.Length);
                    int size = 2;
                    byte[] buffer = new byte[size];
                    int len = NetworkStream.Read(buffer, 0, buffer.Length);
                    string output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        data = ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString());
                        NetworkStream.Write(data, 0, data.Length);
                        size = 2;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        if (output == "1")
                        {
                            data = ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString());
                            NetworkStream.Write(data, 0, data.Length);
                            size = 2;
                            buffer = new byte[size];
                            len = NetworkStream.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
                catch (Exception) { }
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
        /// Function that receive data in bytes from server and returns ReceiveResult class that contains different kinds of information
        /// </summary>
        public ReceiveResult ReceiveData()
        {
            ReceiveResult rr = new ReceiveResult(false, "", "", null, false, 0);
            try
            {
                if (CanTimeout)
                {
                    NetworkStream.ReadTimeout = ReceiveTimeout;
                    NetworkStream.WriteTimeout = SendTimeout;
                }
                else
                {
                    NetworkStream.ReadTimeout = -1;
                    NetworkStream.WriteTimeout = -1;
                }
                int size = 100;
                byte[] buffer = new byte[size];
                int len = NetworkStream.Read(buffer, 0, buffer.Length);
                if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                {
                    byte[] dataBytes = ConvertToBytes("1");
                    NetworkStream.Write(dataBytes, 0, dataBytes.Length);
                    size = 100;
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    dataBytes = ConvertToBytes("1");
                    NetworkStream.Write(dataBytes, 0, dataBytes.Length);
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    dataBytes = ConvertToBytes("1");
                    NetworkStream.Write(dataBytes, 0, dataBytes.Length);
                    rr.Data = Encoding.Unicode.GetString(buffer, 0, len);
                    rr.BytesReceived = 0;
                    rr.Bytes = null;
                    rr.Error = null;
                    rr.HasError = true;
                    rr.ErrorText = Encoding.Unicode.GetString(buffer, 0, len);
                    rr.Success = false;
                    rr.Bytes = null;
                    return rr;
                }
                int partsCount = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                byte[] data = ConvertToBytes("1");
                NetworkStream.Write(data, 0, data.Length);
                List<byte[]> parts = new List<byte[]>();
                for (int i = 0; i < partsCount; i++)
                {
                    size = 100;
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                    {
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        size = 100;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        rr.Data = Encoding.Unicode.GetString(buffer, 0, len);
                        rr.BytesReceived = 0;
                        rr.Bytes = null;
                        rr.Error = null;
                        rr.HasError = true;
                        rr.ErrorText = Encoding.Unicode.GetString(buffer, 0, len);
                        rr.Success = false;
                        rr.Bytes = null;
                        return rr;
                    }
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    data = ConvertToBytes("1");
                    NetworkStream.Write(data, 0, data.Length);
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                    {
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        size = 100;
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        buffer = new byte[size];
                        len = NetworkStream.Read(buffer, 0, buffer.Length);
                        data = ConvertToBytes("1");
                        NetworkStream.Write(data, 0, data.Length);
                        rr.Data = Encoding.Unicode.GetString(buffer, 0, len);
                        rr.BytesReceived = 0;
                        rr.Bytes = null;
                        rr.Error = null;
                        rr.HasError = true;
                        rr.ErrorText = Encoding.Unicode.GetString(buffer, 0, len);
                        rr.Success = false;
                        rr.Bytes = null;
                        return rr;
                    }
                    data = ConvertToBytes("1");
                    NetworkStream.Write(data, 0, data.Length);
                    parts.Add(buffer);
                }
                byte[] bytes = ConcatToOneByteArray(parts.ToArray());
                rr.Data = ConvertToString(bytes);
                rr.BytesReceived = bytes.Length;
                rr.Bytes = bytes;
                rr.Error = null;
                rr.HasError = false;
                rr.ErrorText = "";
                rr.Success = true;
                return rr;
            }
            catch (TimeoutException e)
            {
                rr.Success = false;
                rr.HasError = true;
                rr.ErrorText = e.ToString();
                rr.Error = e;
                rr.Data = e.ToString();
                rr.BytesReceived = 0;
                rr.Bytes = null;
                return rr;
            }
            catch (Exception e)
            {
                byte[] data = ConvertToBytes("0");
                NetworkStream.Write(data, 0, data.Length);
                int size = 2;
                byte[] buffer = new byte[size];
                int len = NetworkStream.Read(buffer, 0, buffer.Length);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "1")
                {
                    data = ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString());
                    NetworkStream.Write(data, 0, data.Length);
                    size = 2;
                    buffer = new byte[size];
                    len = NetworkStream.Read(buffer, 0, buffer.Length);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        data = ConvertToBytes(e.ToString());
                        NetworkStream.Write(data, 0, data.Length);
                    }
                }
                rr.Success = false;
                rr.HasError = true;
                rr.ErrorText = e.ToString();
                rr.Error = e;
                rr.Data = e.ToString();
                rr.BytesReceived = 0;
                rr.Bytes = null;
                return rr;
            }
        }

        #endregion

        #region stream functions

        /// <summary>
        /// Faster variant of SendData() function (this function is not so safety as SendData() function)
        /// </summary>
        public SendResult SendDataFast(byte[] bytes)
        {
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            try
            {
                NetworkStream.Write(bytes, 0, bytes.Length);
                byte[] buffer = new byte[ConvertToBytes("1").Length];
                NetworkStream.Read(buffer, 0, buffer.Length);
                if (ConvertToString(buffer) != "1")
                {
                    sr.HasError = true;
                    sr.Error = null;
                    sr.ErrorText = "Problems with sending data fast";
                    sr.Success = false;
                    sr.Data = "Problems with sending data fast";
                    sr.BytesSent = 0;
                    return sr;
                }
                sr.HasError = false;
                sr.Error = null;
                sr.Success = true;
                sr.ErrorText = "";
                sr.Data = "Data sent successfuly";
                sr.BytesSent = bytes.Length;
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

        /// <summary>
        /// Faster variant of SendData() function (this function is not so safety as SendData() function)
        /// </summary>
        public SendResult SendDataFast(string str)
        {
            byte[] bytes = ConvertToBytes(str);
            SendResult sr = new SendResult(false, "", "", null, false, 0);
            try
            {
                NetworkStream.Write(bytes, 0, bytes.Length);
                byte[] buffer = new byte[ConvertToBytes("1").Length];
                NetworkStream.Read(buffer, 0, buffer.Length);
                if (ConvertToString(buffer) != "1")
                {
                    sr.HasError = true;
                    sr.Error = null;
                    sr.ErrorText = "Problems with sending data fast";
                    sr.Success = false;
                    sr.Data = "Problems with sending data fast";
                    sr.BytesSent = 0;
                    return sr;
                }
                sr.HasError = false;
                sr.Error = null;
                sr.Success = true;
                sr.ErrorText = "";
                sr.Data = "Data sent successfuly";
                sr.BytesSent = bytes.Length;
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

        /// <summary>
        /// Faster variant of ReceiveData() function (this function is not so safety as ReceiveData() function)
        /// </summary>
        public ReceiveResult ReceiveDataFast()
        {
            ReceiveResult rr = new ReceiveResult(false, "", "", null, false, 0);
            try
            {
                int size = 1024000000;
                byte[] buffer = new byte[size];
                NetworkStream.Read(buffer, 0, buffer.Length);
                byte[] data = ConvertToBytes("1");
                NetworkStream.Write(data, 0, data.Length);
                rr.Data = ConvertToString(buffer);
                rr.Error = null;
                rr.ErrorText = "";
                rr.HasError = false;
                rr.Success = true;
                rr.BytesReceived = buffer.Length;
                rr.Bytes = buffer;
                return rr;
            }
            catch (Exception e)
            {
                byte[] data = ConvertToBytes("0");
                NetworkStream.Write(data, 0, data.Length);
                rr.Success = false;
                rr.HasError = true;
                rr.ErrorText = e.ToString();
                rr.Error = e;
                rr.Data = e.ToString();
                rr.BytesReceived = 0;
                return rr;
            }
        }

        /// <summary>
        /// Faster variant of ReceiveData() function (this function is not so safety as ReceiveData() function)
        /// </summary>
        public ReceiveResult ReceiveDataFast(int size)
        {
            ReceiveResult rr = new ReceiveResult(false, "", "", null, false, 0);
            try
            {
                byte[] buffer = new byte[size];
                NetworkStream.Read(buffer, 0, buffer.Length);
                byte[] data = ConvertToBytes("1");
                NetworkStream.Write(data, 0, data.Length);
                rr.Data = ConvertToString(buffer);
                rr.Error = null;
                rr.ErrorText = "";
                rr.HasError = false;
                rr.Success = true;
                rr.BytesReceived = buffer.Length;
                rr.Bytes = buffer;
                return rr;
            }
            catch (Exception e)
            {
                byte[] data = ConvertToBytes("0");
                NetworkStream.Write(data, 0, data.Length);
                rr.Success = false;
                rr.HasError = true;
                rr.ErrorText = e.ToString();
                rr.Error = e;
                rr.Data = e.ToString();
                rr.BytesReceived = 0;
                rr.Bytes = null;
                return rr;
            }
        }

        #endregion

        #region additional functions

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

        #endregion

        #region events

        public class ErrorCaughtArgs
        {
            public ErrorCaughtArgs(Exception e) { Error = e; }
            public Exception Error { get; }
        }
        public delegate void ErrorCaughtEventHandler(object sender, ErrorCaughtArgs e);
        public event ErrorCaughtEventHandler ErrorCaughtEvent;

        public class ConnectedArgs
        {
            public ConnectedArgs(Socket s) { Socket = s; }
            public Socket Socket { get; }
        }
        public delegate void ConnectedEventHandler(object sender, ConnectedArgs e);
        public event ConnectedEventHandler ConnectedEvent;

        #endregion
    }
}
