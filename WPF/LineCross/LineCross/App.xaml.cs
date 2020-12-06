using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using LineCross.ViewModel;

namespace LineCross
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		static App()
		{
			DispatcherHelper.Initialize();
		}
	}
}
