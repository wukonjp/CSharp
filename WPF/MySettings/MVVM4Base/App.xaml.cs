using MVVM4Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace MVVM4Base
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			Console.WriteLine(e.Exception);
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			ViewModelLocator.Cleanup();
		}
	}
}
