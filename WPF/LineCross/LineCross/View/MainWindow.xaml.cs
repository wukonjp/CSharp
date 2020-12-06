using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GalaSoft.MvvmLight;
using LineCross.ViewModel;

namespace LineCross.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Closing += Window_Closing;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var result = MessageBox.Show(
				"アプリケーションを終了しますか？",
				"終了確認",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question,
				MessageBoxResult.No
			);
			if (result != MessageBoxResult.Yes)
			{																					// 「はい」以外
				e.Cancel = true;                                                                // キャンセルする
				return;
			}

			ViewModelLocator.Cleanup();
		}
	}
}