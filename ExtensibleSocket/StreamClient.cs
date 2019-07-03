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
    /// Class that allows to create single socket that can be connected only with one server and send a large count of bytes in fastest ways
    /// </summary>
    public class StreamClient
    {
        /// <summary>
        /// Encoding that used to encode strings to bytes and decode bytes to strings (by default it's unicode)
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// Is stream started or not
        /// </summary>
        public bool Started { get; private set; }
        /// <summary>
        /// Server socket that used to communicate with client socket
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// Port (by default it's 1337)
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// Bytes that should be sent
        /// </summary>
        public byte[] BytesSend { get; set; }
        /// <summary>
        /// Length of bytes that bytes, that you want to send, have split to (by default it's 4096)
        /// </summary>
        public int BufferSize { get; set; }
        /// <summary>
        /// IPAddress that client should connect to
        /// </summary>
        public IPAddress IPAddress { get; set; }

        #region constructors

        /// <summary>
        /// Constructor with default server settings
        /// </summary>
        public StreamClient(IPAddress iPAddress)
        {
            Encoding = Encoding.Unicode;
            Port = 1337;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Started = false;
            BufferSize = 4096;
            IPAddress = iPAddress;
            ReceivedDataEvent += new ReceivedDataEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public StreamClient(IPAddress iPAddress, int port)
        {
            Encoding = Encoding.Unicode;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Started = false;
            BufferSize = 4096;
            IPAddress = iPAddress;
            ReceivedDataEvent += new ReceivedDataEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public StreamClient (IPAddress iPAddress, int port, int bufferSize)
        {
            Encoding = Encoding.Unicode;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Started = false;
            BufferSize = bufferSize;
            IPAddress = iPAddress;
            ReceivedDataEvent += new ReceivedDataEventHandler((sender, e) => {

            });
        }

        /// <summary>
        /// Constructor with custon server settings
        /// </summary>
        public StreamClient(IPAddress iPAddress, int port, int bufferSize, Encoding encoding)
        {
            Encoding = encoding;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress = iPAddress;
            Started = false;
            BufferSize = bufferSize;
            ReceivedDataEvent += new ReceivedDataEventHandler((sender, e) => {

            });
        }

        #endregion

        #region events

        public class ReceivedDataArgs
        {
            public ReceivedDataArgs(byte[] d) { data = d; }
            public byte[] data { get; }
        }
        public delegate void ReceivedDataEventHandler(object sender, ReceivedDataArgs e);
        public event ReceivedDataEventHandler ReceivedDataEvent;

        #endregion

        #region functions

        public void Start()
        {
            try
            {
                Started = true;
                Socket.Connect(new IPEndPoint(IPAddress, Port));
            }
            catch (Exception)
            {
                Started = false;
                Start();
            }
        }

        public void Stop()
        {
            try
            {
                Started = false;
                Socket = null;
            }
            catch (Exception)
            {
                Stop();
            }
        }

        public async void StartReceiveStream()
        {
            await Task.Run(() => {
                while (Started)
                {
                    try
                    {
                        int size = 100;
                        byte[] buffer = new byte[size];
                        int len = Socket.Receive(buffer, size, SocketFlags.None);
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
                        ReceivedDataEvent(this, new ReceivedDataArgs(bytes));
                    }
                    catch (Exception)
                    {
                        Stop();
                    }
                }
            });
        }

        public async void StartSendStream()
        {
            await Task.Run(() => {
                while (Started)
                {
                    try
                    {
                        if (BytesSend == null)
                        {
                            continue;
                        }
                        List<byte[]> parts = new List<byte[]>();
                        byte[] bytes = BytesSend;
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
                        for (int i = 0; i < parts.Count; i++)
                        {
                            Socket.Send(ConvertToBytes(parts[i].Length.ToString()));
                            buffer = new byte[2];
                            len = Socket.Receive(buffer, 2, SocketFlags.None);
                            Socket.Send(parts[i]);
                            buffer = new byte[2];
                            len = Socket.Receive(buffer, 2, SocketFlags.None);
                        }
                    }
                    catch (Exception)
                    {
                        Stop();
                    }
                }
            });
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
                return ConvertToString(bytes);
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
                return ConvertToBytes(str);
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
                return ByteArraySplit(byteData, BufferSize);
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
                return ConcatToOneByteArray(arrays);
            }
        }

        #endregion
    }
}
