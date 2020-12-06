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
	class QuitConfirmAction : TriggerAction<Window>
	{
		protected override void Invoke(object parameter)
		{
			var result = MessageBox.Show(
				"終了しますか？",
				"終了確認",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question
			);

			if (result != MessageBoxResult.Yes)
			{
				var e = (CancelEventArgs)parameter;
				e.Cancel = true;
			}
		}
	}
}
