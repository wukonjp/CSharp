using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Interactivity;

namespace LineCross.View
{
	public class WindowClosingCancelAction : TriggerAction<DependencyObject>
	{
		protected override void Invoke(object parameter)
		{
			if (parameter is CancelEventArgs arg)
			{
				arg.Cancel = true;
			}

			var window = (Window)AssociatedObject;
			window.Visibility = Visibility.Hidden;
		}
	}
}
