using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfListView
{
	public class Item
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
	}
	public class ViewModel
	{
		public List<Item> ItemList { get; set; }

		public ViewModel()
		{
			ItemList = new List<Item>
			{
				new Item{ID = 1, Name = "ABCD", Age = 18},
				new Item{ID = 2, Name = "EFGH", Age = 12},
				new Item{ID = 3, Name = "WQPH", Age = 15},
			};
		}
	}

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
	}
}
