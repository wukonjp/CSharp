using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM4Base.Common
{
	public class StringJoinMultiConverter : IMultiValueConverter
	{
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder builder = new StringBuilder();

            foreach(var value in values)
			{
                builder.Append(value.ToString());
			}

            var text = builder.ToString();
            return text;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
