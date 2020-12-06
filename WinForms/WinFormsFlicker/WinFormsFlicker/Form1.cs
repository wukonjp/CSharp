using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsFlicker
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			this.timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			string dateTime = DateTime.Now.Ticks.ToString("00000000000000000000000000000000000000000000000000000000000000000");
			StringBuilder builder = new StringBuilder();
			for(int i=0; i<80; i++)
			{
				builder.AppendLine(dateTime);
			}
			string text = builder.ToString();

			this.textBox1.Text = text;
			this.label1.Text = text;
			this.richTextBox1.Text = text;
		}
	}
}
