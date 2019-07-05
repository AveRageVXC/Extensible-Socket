# Extensible Socket Library
![](https://github.com/Linkovskiy/ExtensibleSocket/blob/master/Icon128.png?raw=true)

## About library

Extensible Socket Library is a socket library with different kinds of functions. Library provides possibilities to work with connected clients. It is a server-based library.

## Features

It provides features and abilities like:
- Different kinds of data exchanging.
- Extremely fast data exchanging (there is an example below where you can learn how to use our library for streaming).
- Different kinds of work with clients.
- **Also this list will be supplemented.**

## Bugs

It is simple. Everybody knows that it is not a bug, but a feature.

## Callback

Please feel free to write us or open an issue, we would be glad to answer your questions.

## Nuget

This library is available on Nuget.

There is a link: https://www.nuget.org/packages/ExtensibleSocketLibrary/

https://www.nuget.org/packages/ExtensibleSocketLibrary/

## Examples

### Start server and client

First of all, we need to start server and client.

Let&apos;s build server.

```csharp
int maxClientsLimit = 1000; // Max number of clients that can connect to server
int port = 1337; // Port
int bufferSize = 4096; // Difficult to explain, but just try to keep it in distance between 1024 and 20480 (the more bufferSize, the more speed, but actually to get very high speed use SendDataFast() function)
Encoding encoding = Encoding.Unicode; // Encoding
List<string> domainsAllowed = new List<string>(); // Domains that are allowed, keep it empty to allow all domains
List<IPAddress> iPAddressesAllowed = new List<IPAddress>(); // IPAddresses that are allowed, keep it empty to allow all IPAddresses
Server server = new Server(maxClientsLimit, port, bufferSize, encoding, domainsAllowed, iPAddressesAllowed);
```

Now, we can start server.

```csharp
StartResult startResult = server.Start(); // Function returns StartResult that contains information about errors and another kinds of data
```

After creating server, we can build client.

```csharp
IPAddress ip = IPAddress.Parse("127.0.0.1"); // IPAddress that client have to connect to
int port = 1337; // Port
Encoding encoding = Encoding.Unicode; // Encoding
int bufferSize = 4096; // Hard to explain, but just try to keep it in distance between 1024 and 20480 (the more bufferSize, the more speed, but actually to get very high speed use SendDataFast() function)
Client client = new Client(IPAddress.Parse("127.0.0.1"), 1337, Encoding.Unicode, 4096);
```

And now, connect to server.

```csharp
ConnectResult connectResult = client.Connect(); // Functions returns ConnectResult that contains information about errors and another kinds of data
```

Also, you can stop server or client.

```csharp
StopResult stopResult = server.Stop(); // Functions returns StopResult that contains information about errors and another kinds of data
Result disconnectResult = client.Disconnect(); // Functions returns Result that contains information about errors and another kinds of data
```

### Choose client to work with in server

To choose client to work with you should write this.

```csharp
int id = 0; // id of client
server.Client = server.Clients[id];
```

### Simple data exchanging

Send bytes to server or to client.

```csharp
byte[] bytes = new byte[1024];
SendResult sendResult = client.SendData(bytes);
```

The same, but with string.

```csharp
string data = "data";
SendResult sendResult = client.SendData(data);
```

To receive data you should write this.

```csharp
ReceiveResult receiveResult = client.ReceiveData();
string data = receiveResult.Data; // String that was received
byte[] bytes = receiveResult.Bytes; // Bytes that were received
```

### Fast data exchanging

The same as in previous block, but there is SendDataFast() and ReceiveDataFast().

### Start stream

There is a simple example of stream.

```csharp
int delay = 10; // Milliseconds
while (true)
{
	byte[] bytes = new byte[1024];
	SendResult sendResult = server.SendDataFast(bytes);
	int bytesThatWereSent = sendResult.BytesSent;
	Thread.Sleep(delay);
}
```

### Handle errors

Results contains errors, but there is also event to handle unexpected errors.

```csharp
ErrorCaughtEvent += new ErrorCaughtEventHandler((sender, e) => {
	// Handle error here
});
```

### Handle connections

```csharp
ClientConnectedEvent += new ClientConnectedEventHandler((sender, e) => {
	// Handle new connection in server here
});
```

```csharp
ConnectedEvent += new ConnectedEventHandler((sender, e) => {
	// Handle new connection in client here
});
```