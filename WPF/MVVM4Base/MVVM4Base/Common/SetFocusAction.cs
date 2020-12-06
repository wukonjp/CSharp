using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace MVVM4Base.Common
{
	public class SetFocusAction : TriggerAction<UIElement>
	{
		public static readonly DependencyProperty ElementNameProperty
			= DependencyProperty.Register("ElementName", typeof(UIElement), typeof(SetFocusAction), null);

		public UIElement ElementName
		{
			get { return (UIElement)this.GetValue(ElementNameProperty); }
			set { this.SetValue(ElementNameProperty, value); }
		}

		protected override void Invoke(object parameter)
		{
			var element = ElementName;
			if(element != null)
			{
				element.Focus();
			}
		}
	}
}
