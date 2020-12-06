/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:LineCross.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using LineCross.Model;
using System.Windows;

namespace LineCross.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// <para>
	/// See http://www.mvvmlight.net
	/// </para>
	/// </summary>
	public class ViewModelLocator
	{
		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (ViewModelBase.IsInDesignModeStatic)
			{
				SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
			}
			else
			{
				SimpleIoc.Default.Register<IDataService, DataService>();
			}

			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<InfoViewModel>();
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		public MainViewModel Main
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainViewModel>();
			}
		}

		/// <summary>
		/// Gets the Info property.
		/// </summary>
		public InfoViewModel Info
		{
			get
			{
				return ServiceLocator.Current.GetInstance<InfoViewModel>();
			}
		}

		/// <summary>
		/// Cleans up all the resources.
		/// </summary>
		public static void Cleanup()
		{
			var container = SimpleIoc.Default;
			if(container != null)
			{
				if (container.ContainsCreated<InfoViewModel>())
				{
					var infoViewModel = container.GetInstance<InfoViewModel>();
					infoViewModel?.Cleanup();
				}

				if (container.ContainsCreated<MainViewModel>())
				{
					var mainViewModel = container.GetInstance<MainViewModel>();
					mainViewModel?.Cleanup();
				}
			}
		}
	}
}