using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM4Base.Model
{
	public class Person : ObservableObject
	{
		private int _id;
		public int ID
		{
			get
			{
				return _id;
			}
			set
			{
				Set(ref _id, value);
			}
		}

		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				Set(ref _name, value);
			}
		}

		private int _age;
		public int Age
		{
			get
			{
				return _age;
			}
			set
			{
				Set(ref _age, value);
			}
		}

		private int _gender;
		public int Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				Set(ref _gender, value);
			}
		}
	}
}
