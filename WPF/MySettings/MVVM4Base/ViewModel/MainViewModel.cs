using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private DataService _dataService;

		private int _dataCountTemp;
		public int DataCountTemp
		{
			get { return _dataCountTemp; }
			set
			{
				if (_dataCountTemp != value)
				{
					_dataCountTemp = value;
					RaisePropertyChanged();
				}
			}
		}

		public ObservableCollection<Person> PeopleTemp { get; private set; } = new ObservableCollection<Person>();

		public MainModel MainModel => _dataService.MainModel;

		private RelayCommand _loadedCommand;
		public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() =>
		{
			ResetTemporary();
		}));

		private RelayCommand _applyCommand;
		public RelayCommand ApplyCommand => _applyCommand ?? (_applyCommand = new RelayCommand(() =>
		{
			ApplyTemporary();
		}));

		private RelayCommand _cancelCommand;
		public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
		{
			ResetTemporary();
		}));

		private RelayCommand _dataCountChangeCommand;
		public RelayCommand DataCountChangeCommand => _dataCountChangeCommand ?? (_dataCountChangeCommand = new RelayCommand(() =>
		{
			ChangeTemporaryDataCount();
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

			MainModel.ReadSetting();
		}

		private void ResetTemporary()
		{
			DataCountTemp = MainModel.DataCount;

			PeopleTemp.Clear();
			foreach(var person in MainModel.People)
			{
				var personTemp = new Person();
				personTemp.Name = person.Name;
				personTemp.Age = person.Age;

				PeopleTemp.Add(personTemp);
			}
		}

		private void ChangeTemporaryDataCount()
		{
			int oldCount = PeopleTemp.Count;

			// 要素数を合わせる
			if (oldCount < DataCountTemp)
			{
				int count = DataCountTemp - oldCount;
				for (int i = 0; i < count; i++)
				{
					PeopleTemp.Add(new Person());
				}
			}
			else if (oldCount > DataCountTemp)
			{
				int count = oldCount - DataCountTemp;
				for (int i = 0; i < count; i++)
				{
					PeopleTemp.RemoveAt(DataCountTemp);
				}
			}
			else
			{
				// nop
			}
		}

		private void ApplyTemporary()
		{
			// 要素数を合わせる
			if(MainModel.DataCount < DataCountTemp)
			{
				int count = DataCountTemp - MainModel.DataCount;
				for (int i=0; i<count; i++)
				{
					MainModel.People.Add(new Person());
				}
			}
			else if (MainModel.DataCount > DataCountTemp)
			{
				int count = MainModel.DataCount - DataCountTemp;
				for (int i = 0; i < count; i++)
				{
					MainModel.People.RemoveAt(DataCountTemp);
				}
			}
			else
			{
				// nop
			}

			// 各要素のプロパティを適用
			MainModel.DataCount = DataCountTemp;
			for (int i = 0; i < DataCountTemp; i++)
			{
				var person = MainModel.People[i];
				var personTemp = PeopleTemp[i];
				person.Name = personTemp.Name;
				person.Age = personTemp.Age;
			}
		}
	}
}