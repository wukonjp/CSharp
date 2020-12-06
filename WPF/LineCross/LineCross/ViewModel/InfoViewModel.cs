using LineCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Diagnostics;

namespace LineCross.ViewModel
{
	public class InfoViewModel : ViewModelBase
	{
		private readonly IDataService _dataService;

		public DataItem Model { get; private set; }

		private RelayCommand _windowLoadedCommand;
		public RelayCommand WindowLoadedCommand => _windowLoadedCommand ?? (_windowLoadedCommand = new RelayCommand(() =>
		{
			Trace.WriteLine(string.Format("{0}> InfoViewModel Loaded", DateTime.Now.ToString()));
		}));

		private RelayCommand _windowClosedCommand;
		public RelayCommand WindowClosedCommand => _windowClosedCommand ?? (_windowClosedCommand = new RelayCommand(() =>
		{
			Trace.WriteLine(string.Format("{0}> InfoViewModel Closed", DateTime.Now.ToString()));
		}));

		/// <summary>
		/// Initializes a new instance of the InfoViewModel class.
		/// </summary>
		public InfoViewModel(IDataService dataService)
		{
			if (dataService == null)
			{
				return;
			}

			_dataService = dataService;
			_dataService.GetData((item, error) =>
			{
				if (error != null)
				{
					// Report error here
					return;
				}
				Model = item;
			});

			Trace.WriteLine(string.Format("{0}> InfoViewModel 初期化", DateTime.Now.ToString()));
		}

		public override void Cleanup()
		{
			Trace.WriteLine(string.Format("{0}> InfoViewModel Cleanup", DateTime.Now.ToString()));
			base.Cleanup();
		}
	}
}
