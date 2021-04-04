using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM4Base.Model
{
	public class DataService : IDataService
	{
		public MainModel MainModel { get; private set; }

		public DataService()
		{
			MainModel = new MainModel(this);
		}
	}
}
