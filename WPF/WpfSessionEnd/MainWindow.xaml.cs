using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace WpfSessionEnd
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			App.Log("Window_Constractor");
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			// セッション終了時は呼ばれない

			App.Log("Window_Closed");
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// セッション終了時は呼ばれない

			App.Log("Window_Closing1");
			Thread.Sleep(1000);
			App.Log("Window_Closing2");
			//MessageBox.Show("終了確認");
		}

		private void Window_Initialized(object sender, EventArgs e)
		{
			// ベースクラスのイベント FrameworkElement.Initialized

			App.Log("Window_Initialized");
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			App.Log("Window_Loaded");
		}

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			// 親がいないので呼ばれない

			App.Log("Window_Unloaded");
		}
	}
}
