using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MVVM4Base.Model
{
	/// <summary>
	/// （パターンその１）
	/// 単一ソースの変更イベントを複数のViewが監視＆アクションするとアクションが複数回呼ばれてしまう
	/// これを回避するため変更通知コレクションを拡張する
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ItemsPropertyChangedEventArgs<T> : EventArgs
		where T : ObservableObject
	{
		public string PropertyName { get; private set; }
		public T Item { get; private set; }

		public ItemsPropertyChangedEventArgs(string propertyName, T item)
		{
			PropertyName = propertyName;
			Item = item;
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
			if (e.OldItems != null)
			{
				foreach (ObservableObject oldItem in e.OldItems)
				{
					oldItem.PropertyChanged -= Items_PropertyChanged;
				}
			}

			if (e.NewItems != null)
			{
				foreach (ObservableObject newItem in e.NewItems)
				{
					newItem.PropertyChanged += Items_PropertyChanged;
				}
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
