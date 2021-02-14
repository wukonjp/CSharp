using MVVM4Base.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM4Base.Model
{
    public interface IDataService
    {
        ObservableObjectCollection<Person> People { get; }
    }
}
