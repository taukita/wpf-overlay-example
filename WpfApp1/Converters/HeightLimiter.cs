using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Converters
{
	using System.Globalization;
	using System.Windows.Data;
	using System.Windows.Markup;

	class HeightLimiter : MarkupExtension, IValueConverter
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double height)
			{
				return 0.75f * height;
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
