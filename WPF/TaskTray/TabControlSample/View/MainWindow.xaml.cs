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
using TabControlSample.ViewModel;

using System.Windows.Forms;

namespace TabControlSample.View
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private Component1 _taskTray = new Component1();
		private bool _closingCancel = true;

		public MainWindow()
		{
			InitializeComponent();

			_taskTray.NotifyIcon.Click += new EventHandler(TaskTrayNotifyIcon_Click);
			int index = _taskTray.ContextMenu.Items.IndexOfKey("toolStripMenuItem1");
			if(index >= 0)
			{
				var exitMenuItem = _taskTray.ContextMenu.Items[index];
				exitMenuItem.Click += TaskTrayContextMenu_Exit_Click;
			}
		}

		private void TaskTrayContextMenu_Exit_Click(object sender, EventArgs e)
		{
			_closingCancel = false;         // クローズのキャンセルを行わない
			Close();                        // クローズ
		}

		private void TaskTrayNotifyIcon_Click(object sender, EventArgs e)
		{
			var mouseEventArgs = e as System.Windows.Forms.MouseEventArgs;
			if(mouseEventArgs != null)
			{
				if(mouseEventArgs.Button == MouseButtons.Left)
				{
					var viewModel = (MainViewModel)DataContext;
					viewModel.Show();
				}
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_closingCancel)
			{
				var viewModel = (MainViewModel)DataContext;
				viewModel.Hide();
				e.Cancel = true;				// クローズをキャンセルする
			}
			else
			{
				_taskTray.Dispose();			// 破棄しないとアイコンがしばらく通知トレイに残る
			}
		}

		private void Button_Click1(object sender, RoutedEventArgs e)
		{
			_closingCancel = false;         // クローズのキャンセルを行わない
			Close();                        // クローズ
		}

		private void Button_Click2(object sender, RoutedEventArgs e)
		{
			var viewModel = (MainViewModel)DataContext;
			viewModel.Hide();
			_taskTray.NotifyIcon.ShowBalloonTip(3000);
		}

		private void Button_Click3(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var tabControl = sender as System.Windows.Controls.TabControl;
			if(tabControl != null)
			{
				var viewModel = (MainViewModel)DataContext;
				if (tabControl.SelectedIndex < 0)
				{
					viewModel.CurrentPerson = null;					
				}
				else
				{
					viewModel.CurrentPerson = viewModel.People[tabControl.SelectedIndex];
					foreach(var person in viewModel.People)
					{
						person.TraceCount();
					}
				}
			}
		}
	}
}
