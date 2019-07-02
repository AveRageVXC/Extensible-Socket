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
        /// Length of bytes that bytes, that you want to send, have split to (by default it's 4096)
        /// </summary>
        public int BufferSize { get; private set; }

        #region constructors

        /// <summary>
        /// Constructor with default server settings
        /// </summary>
        public Client(IPAddress iPAddress)
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Started = false;
            IPAddress = iPAddress;
            BufferSize = 4096;
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
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Started = false;
            IPAddress = iPAddress;
            BufferSize = 4096;
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
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Started = false;
            IPAddress = iPAddress;
            BufferSize = 4096;
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
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            Started = false;
            IPAddress = iPAddress;
            BufferSize = bufferSize;
            ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {

            });
            ConnectedEvent += new ConnectedEventHandler((sender, e) => {

            });
        }

        #endregion

        #region functions

        /// <summary>
        /// Function that connects client to server
        /// </summary>
        public ConnectResult Connect()
        {
            ConnectResult cr = new ConnectResult(false, "", "", null, false);
            try
            {
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
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
                IPAddress = iPAddress;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
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
                IPAddress = iPAddress;
                Port = port;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
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
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
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
                IPAddress = iPAddress;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
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
                IPAddress = iPAddress;
                Port = port;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
                ConnectedEvent(this, new ConnectedArgs(Socket));
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
        /// Function that sends data in bytes to server and returns SendResult class that contains different kinds of information
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
                Socket.Send(ConvertToBytes(parts.Count.ToString()));
                int size = 2;
                byte[] buffer = new byte[size];
                int len = Socket.Receive(buffer, size, SocketFlags.None);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "0")
                {
                    Socket.Send(ConvertToBytes("1"));
                    size = 100;
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
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
                    Socket.Send(ConvertToBytes(parts[i].Length.ToString()));
                    buffer = new byte[2];
                    len = Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                    Socket.Send(parts[i]);
                    buffer = new byte[2];
                    len = Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
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
                try
                {
                    Socket.Send(ConvertToBytes("0"));
                    int size = 2;
                    byte[] buffer = new byte[size];
                    int len = Socket.Receive(buffer, size, SocketFlags.None);
                    string output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        Socket.Send(ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString()));
                        size = 2;
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        if (output == "1")
                        {
                            Socket.Send(ConvertToBytes(e.ToString()));
                            size = 2;
                            buffer = new byte[size];
                            len = Socket.Receive(buffer, size, SocketFlags.None);
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
                List<byte[]> parts = new List<byte[]>();
                if (bytes.Length <= BufferSize)
                {
                    parts.Add(bytes);
                }
                else
                {
                    parts = ByteArraySplit(bytes, BufferSize);
                }
                Socket.Send(ConvertToBytes(parts.Count.ToString()));
                int size = 2;
                byte[] buffer = new byte[size];
                int len = Socket.Receive(buffer, size, SocketFlags.None);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "0")
                {
                    Socket.Send(ConvertToBytes("1"));
                    size = 100;
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
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
                    Socket.Send(ConvertToBytes(parts[i].Length.ToString()));
                    buffer = new byte[2];
                    len = Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        sr.BytesSent = 0;
                        sr.Data = output;
                        sr.ErrorText = output;
                        sr.HasError = true;
                        sr.Error = null;
                        sr.Success = false;
                        return sr;
                    }
                    Socket.Send(parts[i]);
                    buffer = new byte[2];
                    len = Socket.Receive(buffer, 2, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "0")
                    {
                        Socket.Send(ConvertToBytes("1"));
                        size = 100;
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                        Socket.Send(ConvertToBytes("1"));
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
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
                try
                {
                    Socket.Send(ConvertToBytes("0"));
                    int size = 2;
                    byte[] buffer = new byte[size];
                    int len = Socket.Receive(buffer, size, SocketFlags.None);
                    string output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        Socket.Send(ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString()));
                        size = 2;
                        buffer = new byte[size];
                        len = Socket.Receive(buffer, size, SocketFlags.None);
                        output = Encoding.Unicode.GetString(buffer, 0, len);
                        if (output == "1")
                        {
                            Socket.Send(ConvertToBytes(e.ToString()));
                            size = 2;
                            buffer = new byte[size];
                            len = Socket.Receive(buffer, size, SocketFlags.None);
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
            ReceiveResult sr = new ReceiveResult(false, "", "", null, false, 0);
            try
            {
                int size = 100;
                byte[] buffer = new byte[size];
                int len = Socket.Receive(buffer, size, SocketFlags.None);
                if (Encoding.Unicode.GetString(buffer, 0, len) == "0")
                {

                }
                int partsCount = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                Socket.Send(ConvertToBytes("1"));
                List<byte[]> parts = new List<byte[]>();
                for (int i = 0; i < partsCount; i++)
                {
                    size = 100;
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
                    size = Convert.ToInt32(Encoding.Unicode.GetString(buffer, 0, len));
                    Socket.Send(ConvertToBytes("1"));
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
                    Socket.Send(ConvertToBytes("1"));
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
                Socket.Send(ConvertToBytes("0"));
                int size = 2;
                byte[] buffer = new byte[size];
                int len = Socket.Receive(buffer, size, SocketFlags.None);
                string output = Encoding.Unicode.GetString(buffer, 0, len);
                if (output == "1")
                {
                    Socket.Send(ConvertToBytes(ConvertToBytes(e.ToString()).Length.ToString()));
                    size = 2;
                    buffer = new byte[size];
                    len = Socket.Receive(buffer, size, SocketFlags.None);
                    output = Encoding.Unicode.GetString(buffer, 0, len);
                    if (output == "1")
                    {
                        Socket.Send(ConvertToBytes(e.ToString()));
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

        private string ConvertToString(byte[] bytes)
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

        private byte[] ConvertToBytes(string str)
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

        private List<byte[]> ByteArraySplit(byte[] byteData, long BufferSize)
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

        private byte[] ConcatToOneByteArray(params byte[][] arrays)
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
