using GalaSoft.MvvmLight.Messaging;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVM4Base.Common
{
	public class StringMessageTrigger : TriggerBase<DependencyObject>
	{
		public static readonly DependencyProperty MessageProperty
			= DependencyProperty.Register("Message", typeof(string), typeof(StringMessageTrigger), null);

		public string Message
		{
			get { return (string)this.GetValue(MessageProperty); }
			set { this.SetValue(MessageProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			Messenger.Default.Register<string>(AssociatedObject, OnReceived);
		}

		private void OnReceived(string message)
		{
			if(message == Message)
			{
				InvokeActions(message);
			}
		}

		protected override void OnDetaching()
		{
			Messenger.Default.Unregister<string>(AssociatedObject);

			base.OnDetaching();
		}
	}
}
