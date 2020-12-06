using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;

namespace ThreadMessage
{
	class DrawingCreater : IDisposable
	{
		private volatile bool _isDisposing;

		private Action<double> _createdCallBack;
		private volatile bool _isCreateRequested;
		private List<DrawItem> _drawItems;
		private readonly object _createLock = new object();

		private Thread _thread;
		private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

		private double _ratio;

		public void Create(List<DrawItem> drawItems)
		{
			lock (_createLock)
			{
				_isCreateRequested = true;
				_drawItems = drawItems;
			}
			_autoResetEvent.Set();
		}

		public void Dispose()
		{
			try
			{
				_isDisposing = true;
				_autoResetEvent.Set();
				_thread.Join();
			}
			finally
			{
				_autoResetEvent.Dispose();
			}
		}

		public DrawingCreater(double ratio, Action<double> createdCallBack)
		{
			_ratio = ratio;
			_createdCallBack = createdCallBack;
			_thread = new Thread(ThreadLoop);
			_thread.IsBackground = true;
			_thread.Start();
		}

		private void ThreadLoop()
		{
			while (true)
			{
				_autoResetEvent.WaitOne();

				if (_isDisposing)
				{
					var method = MethodBase.GetCurrentMethod();
					Debug.WriteLine(string.Format("{0}.{1}: Breaked.", method.DeclaringType, method.Name));
					return;
				}

				bool isCreate = false;
				List<DrawItem> drawItems = null;
				lock (_createLock)
				{
					isCreate = _isCreateRequested;
					drawItems = _drawItems;

					_isCreateRequested = false;
					_drawItems = null;
				}

				if (isCreate)
				{
					InnerCreate(drawItems);
				}
			}
		}

		private void InnerCreate(List<DrawItem> drawItems)
		{
			double totalLevel = 0;

			if (drawItems != null)
			{
				foreach (var drawItem in drawItems)
				{
					if (_isDisposing)
					{
						var method = MethodBase.GetCurrentMethod();
						Debug.WriteLine(string.Format("{0}.{1}: Breaked.", method.DeclaringType, method.Name));
						return;
					}

					if (_isCreateRequested)
					{
						var method = MethodBase.GetCurrentMethod();
						Debug.WriteLine(string.Format("{0}.{1}: ReCreate.", method.DeclaringType, method.Name));

						totalLevel = 0.0;

						Application.Current.Dispatcher.BeginInvoke((Action)(() =>
						{
							_createdCallBack(totalLevel);
						}));
						return;
					}

					totalLevel += drawItem.Level;
					Thread.Sleep(200);
				}
			}

			totalLevel *= _ratio;

			Application.Current.Dispatcher.BeginInvoke((Action)(() =>
			{
				_createdCallBack(totalLevel);
			}));
		}
	}
}
