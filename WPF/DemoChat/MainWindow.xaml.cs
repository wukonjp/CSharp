using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		ChatServer _chatServer = new ChatServer();
		ChatClient _chatClient1 = new ChatClient();
		ChatClient _chatClient2 = new ChatClient();

		public MainWindow()
		{
			InitializeComponent();

			_chatServer.ReceiveMessage += (sender, message) =>
			{
				serverLog.Dispatcher.Invoke(() =>
				{
					serverLog.Text += message;
				});
			};

			_chatClient1.ReceiveMessage += (sender, message) =>
			{
				client1Log.Dispatcher.Invoke(() =>
				{
					client1Log.Text += message;
				});
			};

			_chatClient2.ReceiveMessage += (sender, message) =>
			{
				client2Log.Dispatcher.Invoke(() =>
				{
					client2Log.Text += message;
				});
			};
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			_chatServer.Dispose();
			_chatClient1.Dispose();
			_chatClient2.Dispose();
		}

		private void ServerStartButton_Click(object sender, RoutedEventArgs e)
		{
			_chatServer.Start();
		}

		private void ServerStopButton_Click(object sender, RoutedEventArgs e)
		{
			_chatServer.Stop();
		}

		private void ServerSendButton_Click(object sender, RoutedEventArgs e)
		{
			_chatServer.Send("サーバー：　" + serverMessage.Text + '\n');
			serverMessage.Clear();
		}

		private void Client1StartButton_Click(object sender, RoutedEventArgs e)
		{
			_chatClient1.Start();
		}

		private void Client1StopButton_Click(object sender, RoutedEventArgs e)
		{
			_chatClient1.Stop();
		}

		private void Client1SendButton_Click(object sender, RoutedEventArgs e)
		{
			_chatClient1.Send("クライアント１：　" + client1Message.Text + '\n');
			client1Message.Clear();
		}

		private void _Client2StartButton_Click(object sender, RoutedEventArgs e)
		{
			_chatClient2.Start();
		}

		private void _Client2StopButton_Click(object sender, RoutedEventArgs e)
		{
			_chatClient2.Stop();
		}

		private void ClientSendButton_Click(object sender, RoutedEventArgs e)
		{
			_chatClient2.Send("クライアント２：　" + client2Message.Text + '\n');
			client2Message.Clear();
		}
	}
}
