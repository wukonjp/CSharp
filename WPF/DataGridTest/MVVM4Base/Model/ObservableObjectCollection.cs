using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

// （パターンその１）
// 単一ソースの変更イベントを複数のViewが監視＆アクションするとアクションが複数回呼ばれてしまう
// これを防ぐため変更通知コレクションを拡張する

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

		private T _item;
		public T Item
		{
			get { return _item; }
		}

		public ItemsPropertyChangedEventArgs(string propertyName, T item)
		{
			_propertyName = propertyName;
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
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (ObservableObject newItem in e.NewItems)
					{
						newItem.PropertyChanged += Items_PropertyChanged;
					}
					break;

				case NotifyCollectionChangedAction.Remove:
					foreach (ObservableObject oldItem in e.OldItems)
					{
						oldItem.PropertyChanged -= Items_PropertyChanged;
					}
					break;

				case NotifyCollectionChangedAction.Replace:
					foreach (ObservableObject oldItem in e.OldItems)
					{
						oldItem.PropertyChanged -= Items_PropertyChanged;
					}
					foreach (ObservableObject newItem in e.NewItems)
					{
						newItem.PropertyChanged += Items_PropertyChanged;
					}
					break;
			}

			base.OnCollectionChanged(e);
		}

		private void Items_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (ItemsPropertyChanged != null)
			{
				var eventArgs = new ItemsPropertyChangedEventArgs<T>(e.PropertyName, (T)sender);
				ItemsPropertyChanged(this, eventArgs);
			}
		}
	}
}
