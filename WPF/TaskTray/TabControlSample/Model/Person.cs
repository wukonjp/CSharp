using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace TabControlSample.Model
{
	public class Person : ObservableObject
	{
		private string _name;
		public string Name
		{
			get => _name;

			set
			{
				if(_name != value)
				{
					_name = value;
					RaisePropertyChanged(nameof(Name));
				}
			}
		}

		private int _age;
		public int Age
		{
			get => _age;

			set
			{
				if (_age != value)
				{
					_age = value;
					RaisePropertyChanged(nameof(Age));
				}
			}
		}

		public void TraceCount()
		{
			var handler = this.PropertyChangedHandler;
			if (handler != null)
			{
				Trace.WriteLine("Count: " + handler.GetInvocationList().Length.ToString());
			}
			else
			{
				Trace.WriteLine("Count: 0");
			}
		}
	}
}
