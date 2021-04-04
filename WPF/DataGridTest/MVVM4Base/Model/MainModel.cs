using GalaSoft.MvvmLight;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;

namespace MVVM4Base.Model
{
	public class MainModel : ObservableObject
	{
		bool _isAdjusting;

		public ObservableObjectCollection<Person> People { get; private set; }
		public CollectionViewSource PeopleViewSource1 { get; private set; }
		public CollectionViewSource PeopleViewSource2 { get; private set; }

		public MainModel(DataService dataService)
		{
			People = new ObservableObjectCollection<Person>()
			{
				new Person() { ID = 1, Name = "田中", Age = 18, Gender = 1 },
				new Person() { ID = 2, Name = "山田", Age = 28, Gender = 0 },
				new Person() { ID = 3, Name = "鈴木", Age = 38, Gender = 1 },
			};
			People.ItemsPropertyChanged += People_ItemsPropertyChanged;

			PeopleViewSource1 = new CollectionViewSource();
			PeopleViewSource1.Source = People;

			PeopleViewSource2 = new CollectionViewSource();
			PeopleViewSource2.Source = People;
		}


		public void Add()
		{
			int id = CreateID();
			People.Add(new Person { ID = id, Name = "追加" });
			RefreshAllView();
		}

		public void Insert(Person person)
		{
			int index = People.IndexOf(person);
			int id = CreateID();
			People.Insert(index, new Person { ID = id, Name = "挿入" });
			RefreshAllView();
		}

		public void Delete(Person person)
		{
			People.Remove(person);
			for (int i = 0; i < People.Count; i++)
			{
				People[i].ID = i + 1;
			}
			RefreshAllView();
		}

		private int CreateID()
		{
			int id;
			if (People.Count < 1)
			{
				id = 1;
			}
			else
			{
				id = People.Max(item => item.ID) + 1;
			}
			return id;
		}

		public void RefreshAllView()
		{
			try
			{
				PeopleViewSource1.View.Refresh();
				PeopleViewSource2.View.Refresh();
			}
			catch
			{
				// nop
			}
		}

		public void ChangeGender(Person person)
		{
			if (_isAdjusting)
			{
				return;
			}
			_isAdjusting = true;

			foreach (var other in People)
			{
				if (other != person)
				{
					other.Gender = (other.Gender == 1) ? 0 : 1;
				}
			}

			_isAdjusting = false;
		}

		private void People_ItemsPropertyChanged(object sender, ItemsPropertyChangedEventArgs<Person> e)
		{
			int index = People.IndexOf(e.Item);

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
