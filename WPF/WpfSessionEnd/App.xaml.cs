﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace WpfSessionEnd
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		[STAThread]
		public static void Main()
		{
			var listner =  (DefaultTraceListener)Trace.Listeners["Default"];
			listner.LogFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

			App.Log("Main_Start");

			var mutex = new Mutex(false, "TestApp");
			if(mutex.WaitOne(0))
			{
				App app = new App();
				app.InitializeComponent();
				app.Run();
			}
			mutex.Close();

			App.Log("Main_End");
		}

		public static void Log(string callerName)
		{
			Trace.WriteLine(string.Format("[{0}][{1}]{2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, callerName));
		}

		private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
		{
			// セッション終了時にのみ呼ばれる

			App.Log("Application_SessionEnding1");
			Thread.Sleep(3000);
			App.Log("Application_SessionEnding2");
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			App.Log("Application_Startup");
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			App.Log("Application_Exit");
		}
	}
}
