using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ThreadMessage
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private Storage _storage;
		private DrawingCreater _drawingCreater1;
		private DrawingCreater _drawingCreater2;
		private List<DrawItem> _drawItems;

		public MainWindow()
		{
			InitializeComponent();

			_storage = new Storage(QueueChangedCallBack, ExtractedCallBack, ProgressChangedCallBack);
			_drawingCreater1 = new DrawingCreater(1.0, CreatedCallBack1);
			_drawingCreater2 = new DrawingCreater(1.5, CreatedCallBack2);
		}

		private void QueueChangedCallBack()
		{
			_queueCountLabel.Content = _storage.QueueCount.ToString();
		}

		private void ExtractedCallBack(List<DrawItem> drawItems)
		{
			_drawItems = drawItems;
			_drawingCreater1.Create(_drawItems);
			_drawingCreater2.Create(_drawItems);

			Debug.WriteLine("Extracted.");
		}

		private void CreatedCallBack1(double totalLevel)
		{
			_view1DrawLevelLabel.Content = totalLevel.ToString();
		}

		private void CreatedCallBack2(double totalLevel)
		{
			_view2DrawLevelLabel.Content = totalLevel.ToString();
		}

		private void ProgressChangedCallBack()
		{
			Storage.ExtractState state;
			int progress;
			_storage.GetProgress(out state, out progress);

			_statusLabel.Content = state.ToString();
			_progressBar.Value = progress;

			Debug.WriteLine("Progress Changed.");
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_storage?.Dispose();
			_drawingCreater1?.Dispose();
			_drawingCreater2?.Dispose();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var dataItems = new List<DataItem>();
			var dataItem = new DataItem();
			dataItem.Level = 1.0;
			dataItems.Add(dataItem);
			dataItems.Add(dataItem);
			_storage.AddItems(dataItems);
			_storage.AddItems(dataItems);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			_storage.StartExtract();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			_storage.AbortExtract();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			if (_drawItems != null)
			{
				foreach (var drawItem in _drawItems)
				{
					drawItem.Level += 10.0;
				}
				_drawingCreater1.Create(_drawItems);
				_drawingCreater2.Create(_drawItems);
			}
		}
	}
}
