using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace LineCross.View
{
    public class PointDragDropBehavior : Behavior<Thumb>
	{
		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(PointDragDropBehavior), new PropertyMetadata(null));

		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached("CommandParameter", typeof(int), typeof(PointDragDropBehavior), new PropertyMetadata(null));

		public ICommand Command
		{
			get
			{
				return (ICommand)GetValue(CommandProperty);
			}
			set
			{
				SetValue(CommandProperty, value);
			}
		}

		public int CommandParameter
		{
			get
			{
				return (int)GetValue(CommandParameterProperty);
			}
			set
			{
				SetValue(CommandParameterProperty, value);
			}
		}

		public PointDragDropBehavior()
		{
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.DragStarted += AssociatedObject_DragStarted;
			AssociatedObject.DragCompleted += AssociatedObject_DragCompleted;
			AssociatedObject.DragDelta += AssociatedObject_DragDelta;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.DragStarted -= AssociatedObject_DragStarted;
			AssociatedObject.DragCompleted -= AssociatedObject_DragCompleted;
			AssociatedObject.DragDelta -= AssociatedObject_DragDelta;
			base.OnDetaching();
		}

		private void AssociatedObject_DragStarted(object sender, DragStartedEventArgs e)
		{

		}

		private void AssociatedObject_DragCompleted(object sender, DragCompletedEventArgs e)
		{

		}

		private void AssociatedObject_DragDelta(object sender, DragDeltaEventArgs e)
		{
			var thumb = AssociatedObject;
			var offsetX = thumb.ActualWidth / 2.0;
			var offsetY = thumb.ActualHeight / 2.0;
			var x = Canvas.GetLeft(thumb) + offsetX + e.HorizontalChange;
			var y = Canvas.GetTop(thumb) + offsetY + e.VerticalChange;

			if (thumb.Parent is Canvas canvas)
			{
				x = Math.Max(x, 0);
				y = Math.Max(y, 0);
				x = Math.Min(x, canvas.ActualWidth - 1);
				y = Math.Min(y, canvas.ActualHeight - 1);
			}

			var param = new Tuple<int, Point>(CommandParameter, new Point(x, y));
			Command.Execute(param);
		}
	}
}
