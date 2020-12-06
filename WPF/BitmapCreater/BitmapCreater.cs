using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace WpfApp1.Model
{
	class BitmapCreater : IDisposable
	{
		private Thread _thread;
		private bool _isDisposing;
		private bool _isBusy;
		private AutoResetEvent _waitHandle;
		private Action<BitmapCreater> _endCreateCallback;
		private int _width;
		private int _height;
		private List<PolygonDrawData> _polygons;

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern bool DeleteObject(System.IntPtr hObject);

		private BitmapSource _bitmapSource;
		public BitmapSource BitmapSource
		{
			get
			{
				return _bitmapSource;
			}
		}

		private void Create()
		{
			using (var gdiBitmap = new System.Drawing.Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
			using (var g = System.Drawing.Graphics.FromImage(gdiBitmap))
			{
				for (int i = 0; i < _polygons.Count; i++)
				{
					var polygon = _polygons[i];
					var points = polygon.Points;
					using (var brush = new System.Drawing.SolidBrush(polygon.GdiBrushColor))
					{
						g.FillPolygon(brush, points);
					}
				}

				IntPtr hBitmap = IntPtr.Zero;
				try
				{
					hBitmap = gdiBitmap.GetHbitmap();
					_bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
						hBitmap,
						System.IntPtr.Zero,
						System.Windows.Int32Rect.Empty,
						System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
					);
					_bitmapSource.Freeze();
				}
				finally
				{
					if (hBitmap != IntPtr.Zero)
					{
						DeleteObject(hBitmap);
					}
				}
			}
		}

		public void BeginCreate(int width, int height, ObservableCollection<PolygonDrawData> polygons)
		{
			if(_isBusy)
			{
				return;
			}

			_width = width;
			_height = height;
			_polygons = new List<PolygonDrawData>();
			foreach(var polygon in polygons)
			{
				var clone = new PolygonDrawData();
				for(int i=0; i<4; i++)
				{
					clone.Points[i] = polygon.Points[i];
				}
				clone.GdiBrushColor = polygon.GdiBrushColor;

				_polygons.Add(clone);
			}

			_waitHandle.Set();
		}

		private void MainLoop()
		{
			while (!_isDisposing)
			{
				_waitHandle.WaitOne();

				if (_isDisposing)
				{
					break;
				}
				_isBusy = true;
				Create();
				Thread.Sleep(1000);
				_isBusy = false;

				if (_isDisposing)
				{
					break;
				}
				App.Current.Dispatcher.Invoke(() =>
				{
					if (!_isDisposing)
					{
						_endCreateCallback(this);
					}
				});
			}
		}

		private BitmapCreater()
		{
		}

		public BitmapCreater(Action<BitmapCreater> endCreateCallback)
		{
			_thread = new Thread(new ThreadStart(MainLoop));
			_waitHandle = new AutoResetEvent(false);
			_endCreateCallback = endCreateCallback;

			_thread.Start();
		}

		public void Dispose()
		{
			_isDisposing = true;
			_waitHandle.Set();
			_thread.Join();
			_waitHandle.Dispose();
		}
	}
}
