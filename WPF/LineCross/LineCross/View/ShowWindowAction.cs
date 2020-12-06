using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LineCross.View;
using System.Windows.Interactivity;

namespace LineCross.View
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

		public static readonly DependencyProperty IsModalProperty
			= DependencyProperty.Register("IsModal", typeof(bool), typeof(ShowWindowAction), new PropertyMetadata(false));

		public bool IsModal
		{
			get { return (bool)this.GetValue(IsModalProperty); }
			set { this.SetValue(IsModalProperty, value); }
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

				if (IsModal)
				{
					targetWindow.ShowDialog();
				}
				else
				{
					targetWindow.Show();
				}
			}
			else
			{
				targetWindow.Activate();
			}
		}
	}
}
