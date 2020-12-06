using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat
{
	class ChatClient : IDisposable
	{
		object _lockObject = new object();
		StateObject _server;

		public event EventHandler<string> ReceiveMessage;

		private void ReceiveCallback(IAsyncResult ar)
		{
			var server = ar.AsyncState as StateObject;
			if (server == null)
			{
				return;
			}

			lock (_lockObject)
			{
				int readCount;
				try
				{
					readCount = server.Handler.EndReceive(ar);
					if (readCount <= 0)
					{
						server.Handler.Disconnect(false);
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

				var message = Encoding.UTF8.GetString(server.ReceiveBuffer, 0, readCount);
				ReceiveMessage(this, message);

				server.Handler.BeginReceive(server.ReceiveBuffer, 0, server.ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), server);
			}
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

				try
				{
					client.Handler.EndSend(ar);
				}
				catch
				{
					return;
				}
			}
		}

		public void Start()
		{
			Stop();

			lock (_lockObject)
			{
				_server = new StateObject();
				_server.Handler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				var endPoint = new IPEndPoint(IPAddress.Loopback, 13000);
				try
				{
					_server.Handler.Connect(endPoint);
				}
				catch
				{
					_server.Dispose();
					_server = null;
					return;
				}
				_server.Handler.BeginReceive(_server.ReceiveBuffer, 0, _server.ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _server);
			}
		}

		public void Stop()
		{
			lock (_lockObject)
			{
				if (_server != null)
				{
					_server.Dispose();
					_server = null;
				}
			}
		}

		public void Send(string message)
		{
			lock (_lockObject)
			{
				if (_server == null)
				{
					return;
				}

				if (!_server.IsConnected)
				{
					return;
				}

				byte[] body = Encoding.UTF8.GetBytes(message);
				_server.Handler.BeginSend(body, 0, body.Length, 0, new AsyncCallback(SendCallback), _server);
			}
		}

		public void Dispose()
		{
			Stop();
		}

		public ChatClient()
		{
		}
	}
}
