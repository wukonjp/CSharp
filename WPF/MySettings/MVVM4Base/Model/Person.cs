using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MVVM4Base.Model
{
	public class Person : ObservableObject, ICloneable
	{
		public int Index { get; private set; }

		public int ID
		{
			get { return Index + 1; }
		}

		private string _name = "田中";
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					RaisePropertyChanged();
				}
			}
		}

		private int _age = 38;
		public int Age
		{
			get { return _age; }
			set
			{
				if (_age != value)
				{
					_age = value;
					RaisePropertyChanged();
				}
			}
		}

		private bool _isSend = true;
		public bool IsSend
		{
			get { return _isSend; }
			set
			{
				if (_isSend != value)
				{
					_isSend = value;
					RaisePropertyChanged();
				}
			}
		}

		public Person(int index)
		{
			Index = index;
			Reset();
		}

		public Person(int index, Properties.Person settingPerson)
		{
			Index = index;
			Reset(settingPerson);
		}

		public void Reset()
		{
			Reset(Properties.Person.Default);
		}

		public void Reset(Properties.Person settingPerson)
		{
			_name = settingPerson.Name;
			_age = settingPerson.Age;
			_isSend = settingPerson.IsSend;
		}

		public void Apply(Person person)
		{
			this.Name = person.Name;
			this.Age = person.Age;
			this.IsSend = person.IsSend;
		}

		public Properties.Person ToSetting()
		{
			var settingPerson = new Properties.Person();
			settingPerson.Name = _name;
			settingPerson.Age = _age;
			settingPerson.IsSend = _isSend;
			return settingPerson;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
