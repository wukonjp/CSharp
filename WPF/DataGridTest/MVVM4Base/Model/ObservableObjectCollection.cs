using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;


namespace MVVM4Base.Model
{
	public class ItemsPropertyChangedEventArgs<T> : EventArgs
		where T : ObservableObject
	{
		private string _propertyName;
		public string PropertyName
		{
			get { return _propertyName; }
		}

		private int _index;
		public int Index
		{
			get { return _index; }
		}

		private T _item;
		public T Item
		{
			get { return _item; }
		}

		public ItemsPropertyChangedEventArgs(string propertyName, int index, T item)
		{
			_propertyName = propertyName;
			_index = index;
			_item = item;
		}
	}

	public delegate void ItemsPropertyChangedEventHandler<T>(object sender, ItemsPropertyChangedEventArgs<T> e)
		where T : ObservableObject;

	public class ObservableObjectCollection<T> : ObservableCollection<T>
		where T : ObservableObject
	{
		public event ItemsPropertyChangedEventHandler<T> ItemsPropertyChanged;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (ItemsPropertyChanged != null)
			{
				switch (e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						break;

					case NotifyCollectionChangedAction.Move:
						break;

					case NotifyCollectionChangedAction.Remove:
						break;

					case NotifyCollectionChangedAction.Replace:
						break;

					case NotifyCollectionChangedAction.Reset:
						break;
				}
			}

			base.OnCollectionChanged(e);
		}
	}
}
