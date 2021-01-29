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

namespace WpfVisual
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern bool DeleteObject(System.IntPtr hObject);

		Color[] _colors = new Color[256];
		Brush[] _brushes = new Brush[256];
		System.Drawing.Brush[] _gdiBrushes = new System.Drawing.Brush[256];
		List<Point> _points = new List<Point>();

		public MainWindow()
		{
			InitializeComponent();
			InitializeData();
		}

		private void InitializeData()
		{
			for(int i=0; i<256; i++)
			{
				int a = (((i >> 6) & 0x03) * 64) + 63;
				int r = (((i >> 4) & 0x03) * 64) + 63;
				int g = (((i >> 2) & 0x03) * 64) + 63;
				int b = (((i >> 0) & 0x03) * 64) + 63;
				var color = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
				_colors[i] = color;

				var brush = new SolidColorBrush(_colors[i]);
				brush.Freeze();
				_brushes[i] = brush;

				var gdiColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
				var gdiBrush = new System.Drawing.SolidBrush(gdiColor);
				_gdiBrushes[i] = gdiBrush;
			}

			var random = new Random();
			for(int i=0; i<30000; i++)
			{
				double x = random.NextDouble() * 500.0;
				double y = random.NextDouble() * 500.0;
				var point = new Point(x, y);
				_points.Add(point);
			}
		}

		/// <summary>
		/// GDI Bitmap（ブラシキャシュなし）
		/// ※即時描画
		/// </summary>
		private void Button_Click_GDI1(object sender, RoutedEventArgs e)
		{
			var stopwatch = Stopwatch.StartNew();

			using (var bitmap = new System.Drawing.Bitmap(500, 500))
			using (var g = System.Drawing.Graphics.FromImage(bitmap))
			{
				var random = new Random();
				foreach (var point in _points)
				{
					var wpfColor = _colors[random.Next(255)];
					var gdiColor = System.Drawing.Color.FromArgb(wpfColor.A, wpfColor.R, wpfColor.G, wpfColor.B);

					using (var brush = new System.Drawing.SolidBrush(gdiColor))
					{
						g.FillRectangle(brush, (float)point.X, (float)point.Y, 10.0f, 10.0f);
					}
				}

				IntPtr hBitmap = IntPtr.Zero;
				try
				{
					hBitmap = bitmap.GetHbitmap();
					var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
						hBitmap,
						System.IntPtr.Zero,
						System.Windows.Int32Rect.Empty,
						System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
					);
					bitmapSource.Freeze();

					_image.Source = bitmapSource;
				}
				finally
				{
					if (hBitmap != IntPtr.Zero)
					{
						DeleteObject(hBitmap);
					}
				}
			}

			_elapse.Content = stopwatch.Elapsed.TotalSeconds.ToString();
		}

		/// <summary>
		/// GDI Bitmap（ブラシキャシュあり）
		/// ※即時描画
		/// </summary>
		private void Button_Click_GDI2(object sender, RoutedEventArgs e)
		{
			var stopwatch = Stopwatch.StartNew();

			using (var bitmap = new System.Drawing.Bitmap(500, 500))
			using (var g = System.Drawing.Graphics.FromImage(bitmap))
			{
				var random = new Random();
				foreach (var point in _points)
				{
					var brush = _gdiBrushes[random.Next(255)];
					g.FillRectangle(brush, (float)point.X, (float)point.Y, 10.0f, 10.0f);
				}

				IntPtr hBitmap = IntPtr.Zero;
				try
				{
					hBitmap = bitmap.GetHbitmap();
					var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
						hBitmap,
						System.IntPtr.Zero,
						System.Windows.Int32Rect.Empty,
						System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
					);
					bitmapSource.Freeze();

					_image.Source = bitmapSource;
				}
				finally
				{
					if (hBitmap != IntPtr.Zero)
					{
						DeleteObject(hBitmap);
					}
				}
			}

			_elapse.Content = stopwatch.Elapsed.TotalSeconds.ToString();
		}

		/// <summary>
		/// WPF DrawingImage（Freezeなし）
		/// ※遅延描画（計測外でも描画）
		/// </summary>
		private void Button_Click_WPF1(object sender, RoutedEventArgs e)
		{
			var stopwatch = Stopwatch.StartNew();

			var dg = new DrawingGroup();
			using (var dc = dg.Open())
			{
				var random = new Random();

				var pen = new Pen();
				pen.Thickness = 1;

				foreach (var point in _points)
				{
					var color = _colors[random.Next(255)];

					var brush = new SolidColorBrush(color);

					var rect = new Rect(point, new Size(10.0, 10.0));

					dc.DrawRectangle(brush, pen, rect);
				}
			}

			var imageSouce = new DrawingImage(dg);

			_image.Source = imageSouce;

			_elapse.Content = stopwatch.Elapsed.TotalSeconds.ToString();
		}

		/// <summary>
		/// WPF DrawingImage（Freezeあり、ブラシキャッシュなし）
		/// ※遅延描画（計測外でも描画）
		/// </summary>
		private void Button_Click_WPF2(object sender, RoutedEventArgs e)
		{
			var stopwatch = Stopwatch.StartNew();

			var dg = new DrawingGroup();
			using (var dc = dg.Open())
			{
				var random = new Random();

				var pen = new Pen();
				pen.Thickness = 1;
				pen.Freeze();                                           // すごく早くなる

				foreach (var point in _points)
				{
					var color = _colors[random.Next(255)];

					var brush = new SolidColorBrush(color);
					brush.Freeze();

					var rect = new Rect(point, new Size(10.0, 10.0));

					dc.DrawRectangle(brush, pen, rect);
				}
			}

			var imageSouce = new DrawingImage(dg);
			imageSouce.Freeze();

			_image.Source = imageSouce;

			_elapse.Content = stopwatch.Elapsed.TotalSeconds.ToString();
		}

		/// <summary>
		/// WPF DrawingImage（Freezeあり、ブラシキャッシュあり）
		/// ※遅延描画（計測外でも描画）
		/// </summary>
		private void Button_Click_WPF3(object sender, RoutedEventArgs e)
		{
			var stopwatch = Stopwatch.StartNew();

			var dg = new DrawingGroup();
			using (var dc = dg.Open())
			{
				var random = new Random();

				var pen = new Pen();
				pen.Thickness = 1;
				pen.Freeze();

				foreach (var point in _points)
				{
					var brush = _brushes[random.Next(255)];				// 気持ち早くなる

					var rect = new Rect(point, new Size(10.0, 10.0));

					dc.DrawRectangle(brush, pen, rect);
				}
			}

			var imageSouce = new DrawingImage(dg);
			imageSouce.Freeze();

			_image.Source = imageSouce;

			_elapse.Content = stopwatch.Elapsed.TotalSeconds.ToString();
		}

		/// <summary>
		/// WPF RenderTargetBitmap（Freezeあり、ブラシキャッシュあり）
		/// ※即時描画
		/// </summary>
		private void Button_Click_WPF4(object sender, RoutedEventArgs e)
		{
			var stopwatch = Stopwatch.StartNew();

			var visual = new DrawingVisual();
			using (var dc = visual.RenderOpen())
			{
				var random = new Random();

				var pen = new Pen();
				pen.Thickness = 1;
				pen.Freeze();

				foreach (var point in _points)
				{
					var brush = _brushes[random.Next(255)];

					var rect = new Rect(point, new Size(10.0, 10.0));

					dc.DrawRectangle(brush, pen, rect);
				}
			}

			var bitmapSource = new RenderTargetBitmap(500, 500, 96.0, 96.0, PixelFormats.Default);
			bitmapSource.Render(visual);
			bitmapSource.Freeze();

			_image.Source = bitmapSource;

			_elapse.Content = stopwatch.Elapsed.TotalSeconds.ToString();
		}
	}
}
