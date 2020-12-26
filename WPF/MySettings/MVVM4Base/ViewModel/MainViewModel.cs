using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM4Base.Common;
using MVVM4Base.Model;

namespace MVVM4Base.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		private readonly DataService _dataService;

		public MainModel MainModel => _dataService.MainModel;

		private RelayCommand _loadedCommand;
		public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() =>
		{
		}));

		private RelayCommand _resetCommand;
		public RelayCommand ResetCommand => _resetCommand ?? (_resetCommand = new RelayCommand(() =>
		{
			MainModel.ResetSetting();
		}));

		private RelayCommand _closedCommand;
		public RelayCommand ClosedCommand => _closedCommand ?? (_closedCommand = new RelayCommand(() =>
		{
			MainModel.WriteSetting();
			Properties.Settings.Default.Save();
		}));

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(IDataService dataService)
		{
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
				_dataService = dataService.GetDataService();
			}
			else
			{
				// Code runs "for real"
				_dataService = dataService.GetDataService();
			}
		}
	}
}