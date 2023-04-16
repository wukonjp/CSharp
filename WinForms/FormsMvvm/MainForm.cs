using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsMvvm
{
	public partial class MainForm : Form
	{
		private MainViewModel _viewModel = new MainViewModel();

		public MainForm()
		{
			InitializeComponent();

			_driveTextBox.DataBindings.Add(new Binding("Text", _viewModel.MainModel, "DriveLetter"));
			_pathTextBox.DataBindings.Add(new Binding("Text", _viewModel, "MainModel.DriveLetter"));
			_dataGridView.DataBindings.Add(new Binding("DataSource", _viewModel, "MainModel.People"));
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_viewModel.ButtonAction();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			_dataGridView.Columns[0].HeaderText = "ID";
			_dataGridView.Columns[1].HeaderText = "名前";
			_dataGridView.Columns[2].HeaderText = "年齢";
		}
	}
}
