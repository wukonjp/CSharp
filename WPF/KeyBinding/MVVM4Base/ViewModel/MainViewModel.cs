using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM4Base.Common;
using MVVM4Base.Model;
using System.Windows.Input;

namespace MVVM4Base.ViewModel
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
		private IDataService _dataService;

		private RelayCommand _loadedCommand;
		public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() =>
		{
		}));

		private string _inputkey;
		public string InputKey
		{
			get => _inputkey;
			set
			{
				if (_inputkey != value)
				{
					_inputkey = value;
					RaisePropertyChanged();
				}
			}
		}

		private string _inputSystemKey;
		public string InputSystemKey
		{
			get => _inputSystemKey;
			set
			{
				if (_inputSystemKey != value)
				{
					_inputSystemKey = value;
					RaisePropertyChanged();
				}
			}
		}

		private string _inputImeProcessedKey;
		public string InputImeProcessedKey
		{
			get => _inputImeProcessedKey;
			set
			{
				if (_inputImeProcessedKey != value)
				{
					_inputImeProcessedKey = value;
					RaisePropertyChanged();
				}
			}
		}

		private string _inputDeadCharProcessedKey;
		public string InputDeadCharProcessedKey
		{
			get => _inputDeadCharProcessedKey;
			set
			{
				if (_inputDeadCharProcessedKey != value)
				{
					_inputDeadCharProcessedKey = value;
					RaisePropertyChanged();
				}
			}
		}

		private string _inputKeyBinding;
		public string InputKeyBinding
		{
			get => _inputKeyBinding;
			set
			{
				if (_inputKeyBinding != value)
				{
					_inputKeyBinding = value;
					RaisePropertyChanged();
				}
			}
		}

		private RelayCommand<object> _keyEventCommand;
		public RelayCommand<object> KeyEventCommand => _keyEventCommand ?? (_keyEventCommand = new RelayCommand<object>((param) =>
		{
			var args = param as KeyEventArgs;
			if (args != null)
			{
				InputKey = args.Key.ToString();
				InputSystemKey = args.SystemKey.ToString();
				InputImeProcessedKey = args.ImeProcessedKey.ToString();
				InputDeadCharProcessedKey = args.DeadCharProcessedKey.ToString();
			}
		}));

		private RelayCommand<string> _keyBindingCommand;
		public RelayCommand<string> KeyBindingCommand => _keyBindingCommand ?? (_keyBindingCommand = new RelayCommand<string>((key) =>
		{
			InputKeyBinding = key;
		}));

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(IDataService dataService)
		{
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
				_dataService = dataService;
			}
			else
			{
				// Code runs "for real"
				_dataService = dataService;
			}
		}
	}
}