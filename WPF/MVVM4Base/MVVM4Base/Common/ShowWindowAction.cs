using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MVVM4Base.Common
{
    public class ShowWindowAction : TriggerAction<DependencyObject>
	{
		public static readonly DependencyProperty WindowTypeProperty
			= DependencyProperty.Register("WindowType", typeof(Type), typeof(ShowWindowAction), null);

		public Type WindowType
		{
			get { return (Type)this.GetValue(WindowTypeProperty); }
			set { this.SetValue(WindowTypeProperty, value); }
		}

		protected override void Invoke(object parameter)
		{
			if(WindowType == null)
			{
				return;
			}
			
			var windows = Application.Current.Windows;
			Window targetWindow = null;
			foreach(Window window in windows)
			{
				if(window.GetType() == WindowType)
				{
					targetWindow = window;
					break;
				}
			}

			if (targetWindow == null)
			{
				targetWindow = (Window)Activator.CreateInstance(WindowType);
				targetWindow.Owner = Application.Current.MainWindow;
			}

			targetWindow.Visibility = Visibility.Visible;
		}
	}
}
