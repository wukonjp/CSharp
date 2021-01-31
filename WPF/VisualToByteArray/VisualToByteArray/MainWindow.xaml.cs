using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VisualToByteArray
{
	public class Area
	{
		public PointCollection Points { get; set; }
		public Brush FillBrush { get; set; }
		public Color FillColor { get; set; }
		public System.Drawing.Brush FillGdiBrush { get; set; }
	}

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		ObservableCollection<Area> _areas = new ObservableCollection<Area>();
		public ObservableCollection<Area> Areas
		{
			get { return _areas; }
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// 領域を追加する(GDIの座標範囲は±2^27)
			//AddArea(new Point(-100000010, -100000010), new Size(100000200, 100000170), // NG
			//AddArea(new Point(-99999010, -99999010), new Size(99999200, 99999170), // OK
			AddArea(new Point(10, 10), new Size(180, 150),
				Brushes.Red,
				Colors.Red,
				System.Drawing.Brushes.Red);
			AddArea(new Point(10, 170), new Size(180, 150),
				Brushes.Green,
				Colors.Green,
				System.Drawing.Brushes.Green);
			//AddArea(new Point(10, 330), new Size(140000180, 140000150), // NG
			//AddArea(new Point(10, 330), new Size(130000180, 130000150), // OK
			AddArea(new Point(10, 330), new Size(180, 150),
				Brushes.Blue,
				Colors.Blue,
				System.Drawing.Brushes.Blue);

			// 各キャンバスを描画する(Canvas1はXAMLで完結)
			DrawCanvas2();
			DrawCanvas3();
			DrawCanvas4();
			DrawCanvas5();
		}

		private void AddArea(Point basePoint, Size size, Brush brush, Color color, System.Drawing.Brush gdiBrush)
		{
			var area = new Area();

			// 頂点を設定する
			var points = new PointCollection();
			points.Add(basePoint);
			points.Add(new Point(
				basePoint.X+4,
				basePoint.Y+4 + size.Height
			));
			points.Add(new Point(
				basePoint.X+8 + size.Width,
				basePoint.Y+8 + size.Height
			));
			points.Add(new Point(
				basePoint.X+4 + size.Width,
				basePoint.Y+4
			));
			area.Points = points;

			// 色を設定する
			area.FillBrush = brush;
			area.FillColor = color;
			area.FillGdiBrush = gdiBrush;

			_areas.Add(area);
		}

		private void DrawCanvas2()
		{
			// 作業用キャンバスを構築する
			var tempCanvas = new Canvas();
			foreach (var area in _areas)
			{
				var polygon = new Polygon();
				polygon.Points = area.Points;
				polygon.Fill = area.FillBrush;
				tempCanvas.Children.Add(polygon);
			}

			// レイアウトを計算させる
			var size = new Size(_canvas2.ActualWidth, _canvas2.ActualHeight);
			tempCanvas.Height = size.Height;
			tempCanvas.Width = size.Width;
			tempCanvas.Measure(size);
			tempCanvas.Arrange(new Rect(size));

			// レンダリング
			var bitmapSource = new RenderTargetBitmap(
				(int)tempCanvas.Width, (int)tempCanvas.Height, 96, 96, PixelFormats.Pbgra32);      // 一部のフォーマットしか動作しなかった
			bitmapSource.Render(tempCanvas);
			_image2.Source = bitmapSource;
		}

		private void DrawCanvas3()
		{
			int width = (int)_canvas3.ActualWidth;
			int height = (int)_canvas3.ActualHeight;
			using (var bitmap = new System.Drawing.Bitmap(width, height))
			using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
			{
				// GDI Bitmap にポリゴンを描画
				foreach(var area in _areas)
				{
					var points = area.Points;
					var polygon = new System.Drawing.Point[points.Count];
					for (int i = 0; i < points.Count; i++)
					{
						var point = points[i];
						polygon[i] = new System.Drawing.Point((int)point.X, (int)point.Y);
					}
					graphics.FillPolygon(area.FillGdiBrush, polygon);
				}

				// GDI Bitmap → WPF Bitmap
				var rect = new System.Drawing.Rectangle(0, 0, width, height);
				var bitmapData = bitmap.LockBits(
					rect,
					System.Drawing.Imaging.ImageLockMode.ReadOnly,
					System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
				var bitmapSource = BitmapSource.Create(
					width, height, 96, 96, PixelFormats.Pbgra32, null,
					bitmapData.Scan0, (bitmapData.Stride * bitmapData.Height), bitmapData.Stride);
				bitmap.UnlockBits(bitmapData);
				_image3.Source = bitmapSource;
			}
		}

		private void DrawCanvas4()
		{
			int width = (int)_canvas4.ActualWidth;
			int height = (int)_canvas4.ActualHeight;
			using (var bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
			using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
			{
				// GDI Bitmap にポリゴンを描画
				foreach (var area in _areas)
				{
					var points = area.Points;
					var polygon = new System.Drawing.Point[points.Count];
					for (int i = 0; i < points.Count; i++)
					{
						var point = points[i];
						polygon[i] = new System.Drawing.Point((int)point.X, (int)point.Y);
					}
					graphics.FillPolygon(area.FillGdiBrush, polygon);
				}

				// GDI Bitmap → ByteArray
				var rect = new System.Drawing.Rectangle(0, 0, width, height);
				var bitmapData = bitmap.LockBits(
					rect,
					System.Drawing.Imaging.ImageLockMode.ReadOnly,
					System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
				var byteArray = new byte[width * height];
				unsafe
				{
					var pSrc = (byte*)bitmapData.Scan0.ToPointer();
					fixed (byte* pDst = byteArray)
					{
						for (int i = 0; i < height; i++)
						{
							int srcLine = bitmapData.Stride * i;
							int dstLine = width * i;
							for (int j = 0; j < width; j++)
							{
								int src = srcLine + (j * 4);
								int b = pSrc[src];
								int g = pSrc[src + 1];
								int r = pSrc[src + 2];
								//int a = pSrc[src + 3];

								int color;
								if (b != 0)
								{
									color = (1 << 6) | 0x3F;
								}
								else if (g != 0)
								{
									color = (2 << 6) | 0x3F;
								}
								else if (r != 0)
								{
									color = (3 << 6) | 0x3F;
								}
								else
								{
									color = 0;
								}
								int dst = dstLine + j;
								pDst[dst] = (byte)color;
							}
						}
					}
				}
				bitmap.UnlockBits(bitmapData);

				// ByteArray → WPF Gray8
				var bitmapSource = BitmapSource.Create(width, height, 96, 96, PixelFormats.Gray8, null, byteArray, width);
				_image4.Source = bitmapSource;
			}
		}

		private void DrawCanvas5()
		{
			int width = (int)_canvas5.ActualWidth;
			int height = (int)_canvas5.ActualHeight;
			var bitmapSource = BitmapFactory.New(width, height);
			using (var context = bitmapSource.GetBitmapContext())
			{
				// WPF Bitmap にポリゴンを描画(終了点の指定が必要)
				foreach (var area in _areas)
				{
					var points = area.Points;
					var polygon = new int[(points.Count + 1) * 2];
					for (int i = 0; i < points.Count; i++)
					{
						var point = points[i];
						int indexX = i * 2;
						int indexY = (i * 2) + 1;
						polygon[indexX] = (int)point.X;
						polygon[indexY] = (int)point.Y;
					}

					// 終了点
					int lastX = points.Count * 2;
					int lastY = (points.Count * 2) + 1;
					polygon[lastX] = polygon[0];
					polygon[lastY] = polygon[1];

					bitmapSource.FillPolygon(polygon, area.FillColor);
				}
			}
			_image5.Source = bitmapSource;
		}
	}
}
