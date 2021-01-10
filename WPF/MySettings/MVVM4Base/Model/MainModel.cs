using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Configuration;

namespace MVVM4Base.Model
{
	public class MainModel : ObservableObject
	{
		private DataService _dataService;

		private int _dataCount;
		public int DataCount
		{
			get { return _dataCount; }
			set
			{
				if(_dataCount != value)
				{
					_dataCount = value;
					RaisePropertyChanged();
				}
			}
		}

		public ObservableCollection<Person> People { get; private set; } = new ObservableCollection<Person>();

		public MainModel(DataService dataService)
		{
			_dataService = dataService;
		}

		public void ReadSetting()
		{
			_dataCount = Properties.Settings.Default.DataCount;

			var peopleSetting = Properties.Settings.Default.People;
			if (peopleSetting == null)
			{
				Properties.Settings.Default.People = new List<Properties.Person>();
			}

			for (int i = 0; i < _dataCount; i++)
			{
				Person person;
				if (i >= peopleSetting.Count)
				{
					person = new Person(i);
				}
				else
				{
					person = new Person(i, peopleSetting[i]);
				}

				People.Add(person);
			}
		}

		public void ResetSetting()
		{
			for (int i=0; i< People.Count;  i++)
			{
				var person = People[i];
				person.Reset();
			}
		}

		public void WriteSetting()
		{
			Properties.Settings.Default.DataCount = _dataCount;

			var peopleSetting = Properties.Settings.Default.People;
			peopleSetting.Clear();
			foreach (var person in People)
			{
				var personSetting = person.ToSetting();
				peopleSetting.Add(personSetting);
			}
		}
	}
}
