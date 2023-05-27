using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM4Base.Model
{
	public class DataService : IDataService
	{
		public object Data { get; private set; }

		public DataService()
		{
			Data = new object();
		}
	}
}
