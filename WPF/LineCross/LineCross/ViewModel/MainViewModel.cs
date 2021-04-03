using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LineCross.Model;
using System;
using System.Windows;
using System.Diagnostics;

namespace LineCross.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// See http://www.mvvmlight.net
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		private readonly IDataService _dataService;
		private TextWriterTraceListener _traceListener;

		/// <summary>
		/// The <see cref="WelcomeTitle" /> property's name.
		/// </summary>
		//		public const string WelcomeTitlePropertyName = "WelcomeTitle";

		private string _welcomeTitle = string.Empty;

		/// <summary>
		/// Gets the WelcomeTitle property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public string WelcomeTitle
		{
			get
			{
				return _welcomeTitle;
			}
			set
			{
				Set(ref _welcomeTitle, value);
			}
		}

		public DataItem Model { get; private set; }

		private double _line1StartX;
		public double Line1StartX
		{
			get
			{
				return _line1StartX;
			}
			set
			{
				Set(ref _line1StartX, value);
			}
		}

		private double _line1StartY;
		public double Line1StartY
		{
			get
			{
				return _line1StartY;
			}
			set
			{
				Set(ref _line1StartY, value);
			}
		}

		private double _line1EndX;
		public double Line1EndX
		{
			get
			{
				return _line1EndX;
			}
			set
			{
				Set(ref _line1EndX, value);
			}
		}

		private double _line1EndY;
		public double Line1EndY
		{
			get
			{
				return _line1EndY;
			}
			set
			{
				Set(ref _line1EndY, value);
			}
		}

		private double _line2StartX;
		public double Line2StartX
		{
			get
			{
				return _line2StartX;
			}
			set
			{
				Set(ref _line2StartX, value);
			}
		}

		private double _line2StartY;
		public double Line2StartY
		{
			get
			{
				return _line2StartY;
			}
			set
			{
				Set(ref _line2StartY, value);
			}
		}

		private double _line2EndX;
		public double Line2EndX
		{
			get
			{
				return _line2EndX;
			}
			set
			{
				Set(ref _line2EndX, value);
			}
		}

		private double _line2EndY;
		public double Line2EndY
		{
			get
			{
				return _line2EndY;
			}
			set
			{
				Set(ref _line2EndY, value);
			}
		}

		private Visibility _crossPointVisibility;
		public Visibility CrossPointVisibility
		{
			get
			{
				return _crossPointVisibility;
			}
			set
			{
				Set(ref _crossPointVisibility, value);
			}
		}

		private double _crossPointX;
		public double CrossPointX
		{
			get
			{
				return _crossPointX;
			}
			set
			{
				Set(ref _crossPointX, value);
			}
		}

		private double _crossPointY;
		public double CrossPointY
		{
			get
			{
				return _crossPointY;
			}
			set
			{
				Set(ref _crossPointY, value);
			}
		}

		private Point _crossPoint;
		public Point CrossPoint
		{
			get
			{
				return _crossPoint;
			}
			set
			{
				Set(ref _crossPoint, value);
			}
		}

		private RelayCommand _windowLoadedCommand;
		public RelayCommand WindowLoadedCommand => _windowLoadedCommand ?? (_windowLoadedCommand = new RelayCommand(() =>
		{
			// ログの開始
			// コンストラクタでリスナー登録するとアナライザのログを拾ってしまうので、ここで登録することにする。
			Trace.AutoFlush = true;
			_traceListener = new TextWriterTraceListener("trace.txt");
			Trace.Listeners.Add(_traceListener);

			Trace.WriteLine(string.Format("{0}> MainViewModel Loaded", DateTime.Now.ToString()));
		}));

		private RelayCommand _windowClosedCommand;
		public RelayCommand WindowClosedCommand => _windowClosedCommand ?? (_windowClosedCommand = new RelayCommand(() =>
		{
			Trace.WriteLine(string.Format("{0}> MainViewModel Closed", DateTime.Now.ToString()));
		}));

		private RelayCommand<Tuple<int, Point>> _changePointCommand;
		public RelayCommand<Tuple<int, Point>> ChangePointCommand =>
			_changePointCommand ?? (_changePointCommand = new RelayCommand<Tuple<int, Point>>((param) =>
		{
			Model.ChangePoint(param);
		}));

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(IDataService dataService)
		{
			if(dataService == null)
			{
				return;
			}

			Trace.WriteLine(string.Format("{0}> MainViewModel 初期化", DateTime.Now.ToString()));

			_dataService = dataService;
			_dataService.GetData((item, error) =>
			{
				if (error != null)
				{
					// Report error here
					return;
				}

				WelcomeTitle = item.Title;

				Model = item;
			});

			Model.PointChanged += Model_PointChanged;

			Model.Initialize();
		}

		public override void Cleanup()
		{
			Trace.WriteLine(string.Format("{0}> MainViewModel Cleanup", DateTime.Now.ToString()));

			base.Cleanup();
		}

		private void Model_PointChanged(object sender, System.EventArgs e)
		{
			Line1StartX = Model.Line1Start.X;
			Line1StartY = Model.Line1Start.Y;

			Line1EndX = Model.Line1End.X;
			Line1EndY = Model.Line1End.Y;

			Line2StartX = Model.Line2Start.X;
			Line2StartY = Model.Line2Start.Y;

			Line2EndX = Model.Line2End.X;
			Line2EndY = Model.Line2End.Y;

			CrossPointVisibility = Model.IsCross ? Visibility.Visible : Visibility.Collapsed;
			CrossPointX = Model.CrossPoint.X;
			CrossPointY = Model.CrossPoint.Y;

			CrossPoint = Model.CrossPoint;
		}
	}
}