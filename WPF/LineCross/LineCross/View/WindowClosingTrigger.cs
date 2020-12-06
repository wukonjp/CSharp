using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Interactivity;

namespace LineCross.View
{
	public class WindowClosingTrigger : TriggerBase<Window>
	{
		protected override void OnAttached()
		{
			AssociatedObject.Closing += this.Closing;
		}

		private void Closing(object sender, CancelEventArgs e)
		{
			InvokeActions(e);
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Closing -= this.Closing;
		}
	}
}
