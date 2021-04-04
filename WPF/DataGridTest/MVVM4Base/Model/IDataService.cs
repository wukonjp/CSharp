using MVVM4Base.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM4Base.Model
{
	public interface IDataService
	{
		MainModel MainModel { get; }
	}
}
