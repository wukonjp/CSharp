using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Microsoft.Xaml.Behaviors;

namespace MVVM4Base.Common
{
	public class WindowClosingCancelAction : TriggerAction<DependencyObject>
	{
		protected override void Invoke(object parameter)
		{
			CancelEventArgs arg = (CancelEventArgs)parameter;
			var window = (Window)AssociatedObject;
			window.Visibility = Visibility.Hidden;
			arg.Cancel = true;
		}
	}
}
