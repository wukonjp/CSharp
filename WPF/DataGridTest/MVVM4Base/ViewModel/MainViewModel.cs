using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM4Base.Common;
using MVVM4Base.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

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
		private readonly IDataService _dataService;

		public MainModel MainModel
		{
			get { return _dataService.MainModel; }
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

		private RelayCommand<Person> _genderChangedCommand;
		public RelayCommand<Person> GenderChangedCommand => _genderChangedCommand ?? (_genderChangedCommand = new RelayCommand<Person>((person) =>
		{
			MainModel.ChangeGender(person);
		}));

		private RelayCommand _addCommand;
		public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(() =>
		{
			MainModel.Add();
		}));

		private RelayCommand<Person> _insertCommand;
		public RelayCommand<Person> InsertCommand => _insertCommand ?? (_insertCommand = new RelayCommand<Person>((person) =>
		{
			MainModel.Insert(person);
		}));

		private RelayCommand<Person> _deleteCommand;
		public RelayCommand<Person> DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand<Person>((person) =>
		{
			MainModel.Delete(person);
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
	}
}