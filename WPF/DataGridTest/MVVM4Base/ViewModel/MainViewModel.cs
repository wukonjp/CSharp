using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM4Base.Common;
using MVVM4Base.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
		private IDataService _dataService;

		public IDataService DataService
		{
			get { return _dataService; }
		}

		private Brush _backgroundbrush = Brushes.Pink;
		public Brush BackgroundBrush
		{
			get
			{
				return _backgroundbrush;
			}
			set
			{
				Set(ref _backgroundbrush, value);
			}
		}

		private RelayCommand _loadedCommand;
		public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() =>
		{
			_dataService.People.ItemsPropertyChanged += People_ItemsPropertyChanged;
		}));

		private RelayCommand _cellDoubleClickedCommand;
		public RelayCommand CellDoubleClickedCommand => _cellDoubleClickedCommand ?? (_cellDoubleClickedCommand = new RelayCommand(() =>
		{
			if (BackgroundBrush == Brushes.Pink)
			{
				BackgroundBrush = Brushes.AliceBlue;
			}
			else
			{
				BackgroundBrush = Brushes.Pink;
			}
		}));

		bool _isAdjusting;

		private RelayCommand<Person> _genderChangedCommand;
		public RelayCommand<Person> GenderChangedCommand =>
			_genderChangedCommand ?? (_genderChangedCommand = new RelayCommand<Person>((person) =>
		{
			if (_isAdjusting)
			{
				return;
			}
			_isAdjusting = true;

			foreach (var other in _dataService.People)
			{
				if (other != person)
				{
					other.Gender = (other.Gender == 1) ? 0 : 1;
				}
			}

			_isAdjusting = false;
		}));

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(IDataService dataService)
		{
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
				_dataService = dataService;
			}
			else
			{
				// Code runs "for real"
				_dataService = dataService;
			}
		}

		private void People_ItemsPropertyChanged(object sender, ItemsPropertyChangedEventArgs<Person> e)
		{
			int index = _dataService.People.IndexOf(e.Item);

			switch (e.PropertyName)
			{
				case "ID":
					Debug.WriteLine("ID({0}) is Changed. Value={1}.", index, e.Item.ID);
					break;
				case "Name":
					Debug.WriteLine("Name({0}) is Changed. Value={1}.", index, e.Item.Name);
					break;
				case "Age":
					Debug.WriteLine("Age({0}) is Changed. Value={1}.", index, e.Item.Age);
					break;
				case "Gender":
					Debug.WriteLine("Gender({0}) is Changed. Value={1}.", index, e.Item.Gender);
					break;
			}
		}
	}
}