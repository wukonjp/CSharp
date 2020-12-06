using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat
{
	class StateObject : IDisposable
	{
		public bool IsConnected
		{
			get
			{
				if (Handler == null)
				{
					return false;
				}
				return Handler.Connected;
			}
		}

		public Socket Handler { get; set; }
		public byte[] ReceiveBuffer { get; set; } = new byte[1024];

		public StateObject()
		{
		}

		public void Dispose()
		{
			if (Handler != null)
			{
				Handler.Dispose();
				Handler = null;
			}
		}
	}

	class ChatServer : IDisposable
	{
		object _lockObject = new object();
		Socket _serverHandler;
		List<StateObject> _clients = new List<StateObject>();

		public event EventHandler<string> ReceiveMessage;

		private void AcceptCallback(IAsyncResult ar)
		{
			lock (_lockObject)
			{
				if (_serverHandler == null)
				{
					return;
				}

				var client = new StateObject();
				try
				{
					client.Handler = _serverHandler.EndAccept(ar);
				}
				catch
				{
					return;
				}

				_clients.RemoveAll((x) => !x.IsConnected);
				_clients.Add(client);
				client.Handler.BeginReceive(client.ReceiveBuffer, 0, client.ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);

				_serverHandler.BeginAccept(new AsyncCallback(AcceptCallback), null);
			}
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			string message;

			lock (_lockObject)
			{
				var client = ar.AsyncState as StateObject;
				if (client == null)
				{
					return;
				}

				int readCount;
				try
				{
					readCount = client.Handler.EndReceive(ar);
					if (readCount <= 0)
					{
						client.Handler.Disconnect(false);
						return;
					}
				}
				catch
				{
					return;
				}

				if (ReceiveMessage == null)
				{
					return;
				}

				message = Encoding.UTF8.GetString(client.ReceiveBuffer, 0, readCount);
				ReceiveMessage(this, message);

				client.Handler.BeginReceive(client.ReceiveBuffer, 0, client.ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
			}

			Send(message);
		}

		private void SendCallback(IAsyncResult ar)
		{
			lock (_lockObject)
			{
				var client = ar.AsyncState as StateObject;
				if (client == null)
				{
					return;
				}

				client.Handler.EndSend(ar);
			}
		}

		public void Start()
		{
			Stop();

			lock (_lockObject)
			{
				_serverHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				var endPoint = new IPEndPoint(IPAddress.Loopback, 13000);
				_serverHandler.Bind(endPoint);
				_serverHandler.Listen(100);
				_serverHandler.BeginAccept(new AsyncCallback(AcceptCallback), null);
			}
		}

		public void Stop()
		{
			lock (_lockObject)
			{
				foreach(var client in _clients)
				{
					client.Dispose();
				}
				_clients.Clear();

				if (_serverHandler != null)
				{
					_serverHandler.Dispose();
					_serverHandler = null;
				}
			}
		}

		public void Send(string message)
		{
			lock (_lockObject)
			{
				byte[] body = Encoding.UTF8.GetBytes(message);
				foreach (var client in _clients)
				{
					if (!client.IsConnected)
					{
						continue;
					}
					client.Handler.BeginSend(body, 0, body.Length, 0, new AsyncCallback(SendCallback), client);
				}
			}
		}

		public void Dispose()
		{
			Stop();
		}

		public ChatServer()
		{
		}
	}
}
