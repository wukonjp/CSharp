using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using MVVM4Base.ViewModel;

namespace MVVM4Base.Common
{
	class WindowCloseAction : TriggerAction<Window>
	{
		protected override void Invoke(object parameter)
		{
			AssociatedObject.Close();
		}
	}
}
