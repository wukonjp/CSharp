using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsFocus
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public static List<T> GetAllControls<T>(Control top) where T : Control
		{
			List<T> buf = new List<T>();
			foreach (Control control in top.Controls)
			{
				if (control is T)
				{
					buf.Add((T)control);
				}
				buf.AddRange(GetAllControls<T>(control));
			}
			return buf;
		}

		public static IntPtr MakeWParam(int l, int h)
		{
			return new IntPtr((((UInt32)h & 0xFFFF) << 16) | ((UInt32)l & 0xFFFF));
		}

		private void DisableFocusIndicator()
		{
			const int WM_CHANGEUISTATE = 0x0127;

			IntPtr wParam = MakeWParam(0x01, 0x07);
			var message = Message.Create(this.Handle, WM_CHANGEUISTATE, wParam, new IntPtr(0));
			WndProc(ref message);
		}

		private void SetClickEvent()
		{
			foreach(ButtonBase buttonBase in GetAllControls<ButtonBase>(this))
			{
				buttonBase.Click += Button_Click;
			}
		}

		private void Button_Click(object sender, EventArgs e)
		{
			this.ActiveControl = null;
		}

		protected override void OnLoad(EventArgs e)
		{
			DisableFocusIndicator();
			SetClickEvent();
			timer1.Start();

			base.OnLoad(e);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.activeControlNameLabel.Text = this.ActiveControl?.Name;
		}
	}
}
