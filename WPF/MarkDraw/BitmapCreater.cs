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
		private double angle = 0.0;

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

		[System.Runtime.InteropServices.DllImport("MarkDraw.dll")]
		private static extern void DrawMark(System.IntPtr hDC);

		private void Create2()
		{
			using (var gdiBitmap = new System.Drawing.Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
			using (var g = System.Drawing.Graphics.FromImage(gdiBitmap))
			{
				IntPtr hDC = IntPtr.Zero;
				try
				{
					hDC = g.GetHdc();
					DrawMark(hDC);
				}
				finally
				{
					if (hDC != IntPtr.Zero)
					{
						g.ReleaseHdc(hDC);
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

		[System.Runtime.InteropServices.DllImport("MarkDraw.dll")]
		private static extern void DrawMark3(System.IntPtr hDC);

		private void Create3()
		{
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();
			using (var openGLBitmap = new System.Drawing.Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
			{
				using (var openGLg = System.Drawing.Graphics.FromImage(openGLBitmap))
				{
					var hDC = openGLg.GetHdc();
					DrawMark3(hDC);
					openGLg.ReleaseHdc(hDC);
				}

				using (var gdiBitmap = new System.Drawing.Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
				using (var g = System.Drawing.Graphics.FromImage(gdiBitmap))
				{
					int cx = gdiBitmap.Width / 2;    // 中心座標X
					int cy = gdiBitmap.Height / 2;   // 中心座標Y

					g.TranslateTransform(-cx, -cy);
					g.RotateTransform((float)angle, System.Drawing.Drawing2D.MatrixOrder.Append);
					g.TranslateTransform(cx, cy, System.Drawing.Drawing2D.MatrixOrder.Append);
					g.DrawImageUnscaled(openGLBitmap, 0, 0);
					angle += 15.0;

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
			System.Diagnostics.Debug.WriteLine("Elapsed = " + stopwatch.ElapsedMilliseconds.ToString());
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
			foreach (var polygon in polygons)
			{
				var clone = new PolygonDrawData();
				for (int i = 0; i < 4; i++)
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
			_waitHandle = new AutoResetEvent(false);

			while (!_isDisposing)
			{
				_waitHandle.WaitOne();

				if (_isDisposing)
				{
					break;
				}
				_isBusy = true;
				Create3();
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

			_waitHandle.Dispose();
		}

		private BitmapCreater()
		{
		}

		public BitmapCreater(Action<BitmapCreater> endCreateCallback)
		{
			_thread = new Thread(new ThreadStart(MainLoop));
			_thread.IsBackground = true;
			_endCreateCallback = endCreateCallback;

			_thread.Start();
		}

		public void Dispose()
		{
			_isDisposing = true;
		}
	}
}
