using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCheckBox
{
	class ColumnHeader
	{
		public string LabelU { get; set; }
		public string LabelD { get; set; }
	}

	class Cell
	{
		public bool IsEnabled { get; set; }
		public bool IsChecked { get; set; }
	}

	class Row
	{
		public string NameA { get; set; }
		public string NameB { get; set; }
		public Cell[] Columuns { get; set; }

		public Row()
		{
			Columuns = new Cell[9]
			{
				new Cell{ IsEnabled=true,  IsChecked=false },
				new Cell{ IsEnabled=false, IsChecked=false },
				new Cell{ IsEnabled=true,  IsChecked=true },
				new Cell{ IsEnabled=false, IsChecked=false },
				new Cell{ IsEnabled=true,  IsChecked=false },
				new Cell{ IsEnabled=false, IsChecked=false },
				new Cell{ IsEnabled=true,  IsChecked=true },
				new Cell{ IsEnabled=false, IsChecked=false },
				new Cell{ IsEnabled=true,  IsChecked=false }
			};
		}
	}

	class ViewModel
	{
		public ColumnHeader[] ColumnHeaders { get; set; }
		public Row[] Rows { get; set; }

		public ViewModel()
		{
			ColumnHeaders = new ColumnHeader[9]
			{
				new ColumnHeader{LabelU="U1", LabelD="D1" },
				new ColumnHeader{LabelU="U2", LabelD="D2" },
				new ColumnHeader{LabelU="U3", LabelD="D3" },
				new ColumnHeader{LabelU="U4", LabelD="D4" },
				new ColumnHeader{LabelU="U5", LabelD="D5" },
				new ColumnHeader{LabelU="U6", LabelD="D6" },
				new ColumnHeader{LabelU="U7", LabelD="D7" },
				new ColumnHeader{LabelU="U8", LabelD="D8" },
				new ColumnHeader{LabelU="U9", LabelD="D9" }
			};

			Rows = new Row[6]
			{
				new Row{ NameA="A1", NameB="B1" },
				new Row{ NameA="A2", NameB="B2" },
				new Row{ NameA="A3", NameB="B3" },
				new Row{ NameA="A4", NameB="B4" },
				new Row{ NameA="A5", NameB="B5" },
				new Row{ NameA="A6", NameB="B6" }
			};
		}
	}
}
