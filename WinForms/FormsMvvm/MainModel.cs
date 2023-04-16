using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FormsMvvm
{
	class Person
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
	}

	class MainModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private string _driveLetter = "A:";
		public string DriveLetter
		{
			get => _driveLetter;
			set
			{
				if (_driveLetter != value)
				{
					_driveLetter = value;
					RaisePropertyChanged();
				}
			}
		}

		public SortableBindingList<Person> _people = new SortableBindingList<Person>();
		public SortableBindingList<Person> People
		{
			get => _people;
			set
			{
				if (_people != value)
				{
					_people = value;
					RaisePropertyChanged();
				}
			}
		}

		public MainModel()
		{
			_people.Add(new Person { ID = 1, Name = "AAA", Age = 11 });
			_people.Add(new Person { ID = 2, Name = "BBB", Age = 22 });
			_people.Add(new Person { ID = 3, Name = "CCC", Age = 33 });
		}

		public void RebuildPeople()
		{
			var people = new SortableBindingList<Person>();
			for (int i = 0; i < 20000; i++)
			{
				people.Add(new Person { ID = i + 1, Name = "DDD", Age = i + 10 });
			}
			this.People = people;
		}
	}
}
