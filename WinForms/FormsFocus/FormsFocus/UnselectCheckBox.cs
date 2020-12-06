using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsFocus
{
	public class UnselectCheckBox : CheckBox
	{
		public UnselectCheckBox()
		{
			SetStyle(ControlStyles.Selectable, false);
		}
	}
}
