using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVVM4Base.Common
{
	/// <summary>
	/// DataGridのカスタムSorting ビヘイビア
	/// </summary>
	public class DataGridSortBehavior : Behavior<DataGrid>
    {
		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Sorting += AssociatedObject_Sorting;
		}

		protected override void OnDetaching()
		{
			this.AssociatedObject.Sorting -= AssociatedObject_Sorting;
			base.OnDetaching();
		}

		private void AssociatedObject_Sorting(object sender, DataGridSortingEventArgs e)
		{
			var dataGrid = (DataGrid)sender;
			if (!(CollectionViewSource.GetDefaultView(dataGrid.ItemsSource) is ListCollectionView view))
			{
				return;
			}

			if (e.Column.SortDirection == null || e.Column.SortDirection != ListSortDirection.Ascending)
			{
				e.Column.SortDirection = ListSortDirection.Ascending;
			}
			else
			{
				e.Column.SortDirection = ListSortDirection.Descending;
			}

			view.CustomSort = new StableComparer(view, e.Column);
			view.Refresh();
			e.Handled = true;
		}

		/// <summary>
		/// 安定ソート用Comparer クラス
		/// </summary>
		private class StableComparer : IComparer
		{
			private readonly IEnumerable _oldCollection;
			private readonly int _direction;
			private readonly PropertyInfo _propertyInfo;

			public StableComparer(IEnumerable oldCollection, DataGridColumn column)
			{
				_oldCollection = oldCollection;
				_direction = column.SortDirection == ListSortDirection.Descending ? -1 : 1;
				foreach(var item in oldCollection)
				{
					if (item != null)
					{
						// 最初に見つかった要素からPropertyInfoを取得する
						_propertyInfo = item.GetType().GetProperty(column.SortMemberPath);
						break;
					}
				}
			}

			private int OldIndexOf(object findItem)
			{
				int i = 0;
				foreach (var item in _oldCollection)
				{
					if (item.Equals(findItem))
					{
						return i;
					}
					i++;
				}
				return -1;
			}

			public int Compare(object o1, object o2)
			{
				if (_propertyInfo == null)
				{
					return 0;
				}

				if (!(_propertyInfo.GetValue(o1) is IComparable c1) ||
					!(_propertyInfo.GetValue(o2) is IComparable c2))
				{
					return 0;
				}

				var comparison = c1.CompareTo(c2);
				if (comparison == 0)
				{
					var i1 = OldIndexOf(o1);
					var i2 = OldIndexOf(o2);
					if (i1 >= 0 && i2 >= 0)
					{
						comparison = i1 - i2;
					}
				}

				return comparison * _direction;
			}
		}
	}
}
