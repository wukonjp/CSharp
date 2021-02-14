using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM4Base.Common;
using MVVM4Base.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
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

		public ObservableObjectCollection<Person> People
		{
			get { return _dataService.People; }
		}

		private Brush _brush = Brushes.Pink;
		public Brush Brush
		{
			get
			{
				return _brush;
			}
			set
			{
				Set(ref _brush, value);
			}
		}

		private RelayCommand _loadedCommand;
		public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() =>
		{
			People.ItemsPropertyChanged += People_ItemsPropertyChanged;
		}));

		private RelayCommand _cellDoubleClickedCommand;
		public RelayCommand CellDoubleClickedCommand => _cellDoubleClickedCommand ?? (_cellDoubleClickedCommand = new RelayCommand(() =>
		{
			if (Brush == Brushes.Pink)
			{
				Brush = Brushes.AliceBlue;
			}
			else
			{
				Brush = Brushes.Pink;
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

			foreach(var other in People)
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
			switch(e.PropertyName)
			{
				case "Name":
					Debug.WriteLine("{0} is Changed. Valu={1}.", e.PropertyName, e.Item.Name);
					break;
			}
		}
	}
}