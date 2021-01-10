using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MVVM4Base.Model
{
	public class Person : ObservableObject
	{
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
	}
}
