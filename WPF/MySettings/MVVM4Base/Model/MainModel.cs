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
		private readonly DataService _dataService;

		public ObservableCollection<Person> People { get; private set; } = new ObservableCollection<Person>();

		public MainModel(DataService dataService)
		{
			_dataService = dataService;

			ReadSetting();
		}

		public void ReadSetting()
		{
			People.Clear();

			var peopleSetting = (List<Properties.Person>)Properties.Settings.Default["People"];		// コンストラクタから呼び出される場合はインデクサを使わないとnullになる
			//var peopleSetting = (List<Properties.Person>)Properties.Settings.Default.People;
			if (peopleSetting != null)
			{
				foreach (var personSetting in peopleSetting)
				{
					var person = new Person();
					person.Name = personSetting.Name;
					person.Age = personSetting.Age;
					People.Add(person);
				}
			}
		}

		public void ResetSetting()
		{
			foreach (var person in People)
			{
				person.Name = Properties.Person.Default.Name;
				person.Age = Properties.Person.Default.Age;
			}

			WriteSetting();
		}

		public void WriteSetting()
		{
			var peopleSetting = new List<Properties.Person>();
			foreach (var person in People)
			{
				var personSetting = new Properties.Person();
				personSetting.Name = person.Name;
				personSetting.Age = person.Age;
				peopleSetting.Add(personSetting);
			}
			Properties.Settings.Default.People = peopleSetting;
		}
	}
}
