using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace LineCross.View
{
	public class ButtonClickTrigger : TriggerBase<Button>
	{
		protected override void OnAttached()
		{
			AssociatedObject.Click += this.ButtonClick;
		}

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			InvokeActions(e);
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Click -= this.ButtonClick;
		}
	}
}
