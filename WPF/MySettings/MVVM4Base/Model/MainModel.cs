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

			// 要素数を合わせる
			People.Clear();
			//var peopleSetting = (List<Properties.Person>)Properties.Settings.Default["People"];		// MainViewModelが構築される前はインデクサを使わないとnullになる
			var peopleSetting = Properties.Settings.Default.People;
			for (int i = 0; i < _dataCount; i++)
			{
				var person = new Person();
				if ((peopleSetting == null) || (i >= peopleSetting.Count))
				{
					person.Name = Properties.Person.Default.Name;
					person.Age = Properties.Person.Default.Age;
					person.IsSend = Properties.Person.Default.IsSend;
				}
				else
				{
					person.Name = peopleSetting[i].Name;
					person.Age = peopleSetting[i].Age;
					person.IsSend = peopleSetting[i].IsSend;
				}

				People.Add(person);
			}
		}

		public void ResetSetting()
		{
			foreach (var person in People)
			{
				person.Name = Properties.Person.Default.Name;
				person.Age = Properties.Person.Default.Age;
				person.IsSend = Properties.Person.Default.IsSend;
			}
		}

		public void WriteSetting()
		{
			Properties.Settings.Default.DataCount = _dataCount;

			var peopleSetting = new List<Properties.Person>();
			foreach (var person in People)
			{
				var personSetting = new Properties.Person();
				personSetting.Name = person.Name;
				personSetting.Age = person.Age;
				personSetting.IsSend = person.IsSend;

				peopleSetting.Add(personSetting);
			}
			Properties.Settings.Default.People = peopleSetting;
		}
	}
}
