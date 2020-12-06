using GalaSoft.MvvmLight;
using System.Windows;
using TabControlSample.Model;

namespace TabControlSample.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
		public Person[] People { get; private set; } =
		{
			new Person{ Name="Name1", Age=11 },
			new Person{ Name="Name2", Age=22 },
			new Person{ Name="Name3", Age=33 }
		};

		private Person _currentPerson;
		public Person CurrentPerson
		{
			get => _currentPerson;

			set
			{
				if(_currentPerson != value)
				{
					_currentPerson = value;
					RaisePropertyChanged(nameof(CurrentPerson));
				}
			}
		}

		private Visibility _windowVisibility = Visibility.Hidden;
		public Visibility WindowVisibility
		{
			get
			{
				return _windowVisibility;
			}

			set
			{
				if (_windowVisibility != value)
				{
					_windowVisibility = value;
					RaisePropertyChanged(nameof(WindowVisibility));
				}
			}
		}

		private bool _showInTaskbar = false;
		public bool ShowInTaskbar
		{
			get
			{
				return _showInTaskbar;
			}

			set
			{
				if (_showInTaskbar != value)
				{
					_showInTaskbar = value;
					RaisePropertyChanged(nameof(ShowInTaskbar));
				}
			}
		}

		public void Hide()
		{
			// ウインドウとタスクバーアイコンを隠す
			WindowVisibility = Visibility.Hidden;
			ShowInTaskbar = false;
		}

		public void Show()
		{
			// ウインドウとタスクバーアイコンを表示する
			WindowVisibility = Visibility.Visible;
			ShowInTaskbar = true;
		}

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}