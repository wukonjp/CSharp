using System;
using LineCross.Model;

namespace LineCross.Design
{
	public class DesignDataService : IDataService
	{
		public void GetData(Action<DataItem, Exception> callback)
		{
			// Use this to create design time data

			var item = new DataItem("Welcome to MVVM Light [design]");
			callback?.Invoke(item, null);
		}
	}
}