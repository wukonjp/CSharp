using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Xaml.Behaviors;
using MVVM4Base.ViewModel;

namespace MVVM4Base.Common
{
	class StringMessageSendAction : TriggerAction<DependencyObject>
	{
		public static readonly DependencyProperty MessageProperty
			= DependencyProperty.Register("Message", typeof(string), typeof(StringMessageSendAction), null);

		public string Message
		{
			get { return (string)this.GetValue(MessageProperty); }
			set { this.SetValue(MessageProperty, value); }
		}

		protected override void Invoke(object parameter)
		{
			Messenger.Default.Send<string>(Message);
		}
	}
}
