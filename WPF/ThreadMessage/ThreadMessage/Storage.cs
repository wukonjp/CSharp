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
	using DataItemList = List<DataItem>;

	class Storage : IDisposable
	{
		public enum ExtractState
		{
			Waiting,
			Extracting,
			Completed
		}

		private volatile bool _isDisposing;

		private List<DataItemList> _works = new List<DataItemList>();
		private readonly object _worksLock = new object();

		private DataItemList _accumulatedItems = new DataItemList();

		private volatile bool _isExtractRequested;
		private bool _isEmptyExtract;
		private readonly object _extractLock = new object();

		private ExtractState _extractState;
		private int _extractProgress;
		private readonly object _progressLock = new object();

		private Action _queueChangedCallBack;
		private Action<List<DrawItem>> _extractedCallBack;
		private Action _progressChangedCallBack;

		private Thread _thread;
		private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

		public int QueueCount
		{
			get
			{
				lock (_worksLock)
				{
					return _works.Count;
				}
			}
		}

		public void AddItems(DataItemList dataItems)
		{
			int count;
			lock (_worksLock)
			{
				_works.Add(dataItems);
				count = _works.Count;
			}
			_autoResetEvent.Set();

			RaiseQueueChanged();
		}

		public void StartExtract()
		{
			lock (_extractLock)
			{
				_isExtractRequested = true;
				_isEmptyExtract = false;
			}
			_autoResetEvent.Set();
		}

		public void AbortExtract()
		{
			lock (_extractLock)
			{
				_isExtractRequested = true;
				_isEmptyExtract = true;
			}
			_autoResetEvent.Set();
		}

		public void GetProgress(out ExtractState state, out int progress)
		{
			lock(_progressLock)
			{
				state = _extractState;
				progress = _extractProgress;
			}
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

		public Storage(Action queueChangedCallBack, Action<List<DrawItem>> extractedCallBack, Action progressChangedCallBack)
		{
			_queueChangedCallBack = queueChangedCallBack;
			_extractedCallBack = extractedCallBack;
			_progressChangedCallBack = progressChangedCallBack;
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

				var isExtractRequested = false;
				var isEmptyExtract = false;
				lock (_extractLock)
				{
					isExtractRequested = _isExtractRequested;
					isEmptyExtract = _isEmptyExtract;

					_isExtractRequested = false;
					_isEmptyExtract = false;
				}

				if (isExtractRequested)
				{
					Extract(isEmptyExtract);
				}
				else
				{
					List<DataItemList> works = null;
					lock (_worksLock)
					{
						if (_works.Count > 0)
						{
							works = _works;
							_works = new List<DataItemList>();
						}
					}
					RaiseQueueChanged();

					if (works != null)
					{
						Accumulate(works);
					}
				}
			}
		}

		private void Accumulate(List<DataItemList> works)
		{
			// TODO: BeginTransaction

			foreach (var work in works)
			{
				foreach (var dataItem in work)
				{
					if (_isDisposing)
					{
						var method = MethodBase.GetCurrentMethod();
						Debug.WriteLine(string.Format("{0}.{1}: Breaked.", method.DeclaringType, method.Name));
						return;
					}

					_accumulatedItems.Add(dataItem);
					Thread.Sleep(200);
				}
			}

			// TODO: Commit
		}

		private void Extract(bool isEmptyExtract)
		{
			RaiseProgressChanged(ExtractState.Extracting, 0);

			List<DrawItem> drawItems = null;
			if ( !isEmptyExtract )
			{
				drawItems = new List<DrawItem>();

				int lastProgress = 0;
				for (int i =0; i < _accumulatedItems.Count; i++)
				{
					if (_isDisposing)
					{
						var method = MethodBase.GetCurrentMethod();
						Debug.WriteLine(string.Format("{0}.{1}: Breaked.", method.DeclaringType, method.Name));
						return;
					}

					if (_isExtractRequested)
					{
						var method = MethodBase.GetCurrentMethod();
						Debug.WriteLine(string.Format("{0}.{1}: ReExtract.", method.DeclaringType, method.Name));

						drawItems = null;
						break;
					}

					var dataItem = _accumulatedItems[i];
					var drawItem = new DrawItem();
					drawItem.Level = dataItem.Level;
					drawItems.Add(drawItem);
					Thread.Sleep(200);

					int progress = (((i * 100) / _accumulatedItems.Count) / 10) * 10;
					if (lastProgress != progress)
					{
						RaiseProgressChanged(ExtractState.Extracting, progress);
						lastProgress = progress;
					}
				}
			}

			RaiseProgressChanged(ExtractState.Completed, 100);
			RaiseExtracted(drawItems);
		}

		private void RaiseQueueChanged()
		{
			Application.Current.Dispatcher.BeginInvoke((Action)(() =>
			{
				_queueChangedCallBack();
			}));
		}

		private void RaiseExtracted(List<DrawItem> drawItems)
		{
			Application.Current.Dispatcher.BeginInvoke((Action)(() =>
			{
				_extractedCallBack(drawItems);
			}));
		}

		private void RaiseProgressChanged(ExtractState state, int progress)
		{
			lock (_progressLock)
			{
				_extractState = state;
				_extractProgress = progress;
			}

			Application.Current.Dispatcher.BeginInvoke((Action)(() =>
			{
				_progressChangedCallBack();
			}));
		}
	}
}