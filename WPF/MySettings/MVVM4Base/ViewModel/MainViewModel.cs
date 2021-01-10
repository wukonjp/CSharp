using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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
			Apply();
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

		private RelayCommand<Person> _changeIsSendCommand;
		public RelayCommand<Person> ChangeIsSendCommand => _changeIsSendCommand ?? (_changeIsSendCommand = new RelayCommand<Person>((person) =>
		{
			int index = MainModel.People.IndexOf(person);
			if(index >= 0)
			{
				if (person.IsSend)
				{
					person.IsSend = false;
					MessageBox.Show(string.Format("ID{0}(Index{1})をOFFにしました。", person.ID, index));
				}
				else
				{
					person.IsSend = true;
					MessageBox.Show(string.Format("ID{0}(Index{1})をONにしました。", person.ID, index));
				}
			}
		}));

		private RelayCommand _applyToTemporaryCommand;
		public RelayCommand ApplyToTemporaryCommand => _applyToTemporaryCommand ?? (_applyToTemporaryCommand = new RelayCommand(() =>
		{
			ApplyToTemporary();
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

			var people = MainModel.People;
			PeopleTemp.Clear();
			for (int i = 0; i < people.Count; i++)
			{
				var person = people[i];
				var personTemp = (Person)person.Clone();
				PeopleTemp.Add(personTemp);
			}
		}

		private void ChangeTemporaryDataCount()
		{
			int oldCount = PeopleTemp.Count;
			int newCount = DataCountTemp;

			// 要素数を合わせる
			if (oldCount < newCount)
			{
				for (int i = oldCount; i < newCount; i++)
				{
					PeopleTemp.Add(new Person(i));
				}
			}
			else if (oldCount > newCount)
			{
				while (PeopleTemp.Count > newCount)
				{
					PeopleTemp.RemoveAt(PeopleTemp.Count - 1);
				}
			}
			else
			{
				// nop
			}
		}

		private void Apply()
		{
			var people = MainModel.People;
			int oldCount = people.Count;
			int newCount = DataCountTemp;

			// 要素数を合わせる
			if (oldCount < newCount)
			{
				for (int i = oldCount; i < newCount; i++)
				{
					people.Add(new Person(i));
				}
			}
			else if (oldCount > newCount)
			{
				while (people.Count > newCount)
				{
					people.RemoveAt(people.Count - 1);
				}
			}
			else
			{
				// nop
			}

			// 各要素のプロパティを適用
			MainModel.DataCount = newCount;
			for (int i = 0; i < newCount; i++)
			{
				var personTemp = PeopleTemp[i];

				var person = people[i];
				person.Apply(personTemp);
			}
		}

		private void ApplyToTemporary()
		{
			int oldCount = DataCountTemp;
			int newCount = MainModel.DataCount;

			// 要素数を合わせる
			if (oldCount < newCount)
			{
				for (int i = oldCount; i < newCount; i++)
				{
					PeopleTemp.Add(new Person(i));
				}
			}
			else if (oldCount > newCount)
			{
				while(PeopleTemp.Count > newCount)
				{
					PeopleTemp.RemoveAt(PeopleTemp.Count - 1);
				}
			}
			else
			{
				// nop
			}

			// 各要素のプロパティを適用
			DataCountTemp = newCount;
			var people = MainModel.People;
			for (int i = 0; i < newCount; i++)
			{
				var person = people[i];

				var personTemp = PeopleTemp[i];
				personTemp.Apply(person);
			}
		}
	}
}