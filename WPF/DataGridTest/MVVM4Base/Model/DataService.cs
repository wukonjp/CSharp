using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM4Base.Model
{
	public class DataService : IDataService
	{
		public ObservableObjectCollection<Person> People { get; private set; }
		public CollectionViewSource PeopleViewSource1 { get; private set; }
		public CollectionViewSource PeopleViewSource2 { get; private set; }

		public DataService()
		{
			People = new ObservableObjectCollection<Person>()
			{
				new Person() { ID = 1, Name = "田中", Age = 18, Gender = 1 },
				new Person() { ID = 2, Name = "山田", Age = 28, Gender = 0 },
				new Person() { ID = 3, Name = "鈴木", Age = 38, Gender = 1 },
			};
			PeopleViewSource1 = new CollectionViewSource();
			PeopleViewSource1.Source = People;

			PeopleViewSource2 = new CollectionViewSource();
			PeopleViewSource2.Source = People;
		}
	}
}
