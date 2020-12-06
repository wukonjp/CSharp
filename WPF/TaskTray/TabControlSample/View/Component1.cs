using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabControlSample.View
{
	public partial class Component1 : Component
	{
		public ContextMenuStrip ContextMenu => contextMenuStrip1;
		public NotifyIcon NotifyIcon => notifyIcon1;

		public Component1()
		{
			InitializeComponent();
		}

		public Component1(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}
	}
}
