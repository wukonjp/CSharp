using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Windows.Media;

namespace LineCross.Model
{
	public class DataItem
	{
		public string Title
		{
			get;
			private set;
		}

		public Point Line1Start { get; private set; }

		public Point Line1End { get; private set; }

		public Point Line2Start { get; private set; }

		public Point Line2End { get; private set; }

		public bool IsCross { get; private set; }

		public Point CrossPoint { get; private set; }
	
		public event EventHandler PointChanged;

		public DataItem(string title)
		{
			Title = title;
		}

		public void Initialize()
		{
			Line1Start = new Point(50.0, 50.0);
			Line1End   = new Point(250.0, 250.0);
			Line2Start = new Point(50.0, 250.0);
			Line2End   = new Point(250.0, 50.0);
			IsCross    = false;
			CrossPoint = new Point(0.0, 0.0);

			CalcIntersection();

			PointChanged?.Invoke(this, null);
		}

		public void ChangePoint(Tuple<int, Point> param)
		{
			if(param == null)
			{
				return;
			}

			switch(param.Item1)
			{
				case 0:
					Line1Start = param.Item2;
					break;

				case 1:
					Line1End = param.Item2;
					break;

				case 2:
					Line2Start = param.Item2;
					break;

				case 3:
					Line2End = param.Item2;
					break;

				default:
					return;
			}

			CalcIntersection();

			PointChanged?.Invoke(this, null);
		}

		/// <summary>
		/// 直線同士の交点を求める
		/// </summary>
		private void CalcIntersection()
		{
			var vector1 = Line1End - Line1Start;
			var vector2 = Line2End - Line2Start;

			var D = Vector.CrossProduct(vector1, vector2);
			if (D == 0.0)
			{
				IsCross = false;
				return;
			}

			var delta = Line1Start - Line2Start;

			var t1 = Vector.CrossProduct(vector2, delta) / D;
			if ((t1 < 0.0) || (t1 > 1.0))
			{
				IsCross = false;
				return;
			}

			var t2 = Vector.CrossProduct(vector1, delta) / D;
			if ((t2 < 0.0) || (t2 > 1.0))
			{
				IsCross = false;
				return;
			}

			CrossPoint = Line1Start + (Vector.Multiply(vector1, t1));
//			CrossPoint = Line2Start + (Vector.Multiply(vector2, t2));
			IsCross = true;
		}
	}
}