using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SD.Infrastructure.WPF.Converters
{
    /// <summary>
    /// 转换树级别缩进尺寸
    /// </summary>
    public class NodeLevelIndentConverter : IValueConverter
    {
        /// <summary>
        /// 缩进单位尺寸
        /// </summary>
        private const double IndentUnitSize = 14.0;

        /// <summary>
        /// 转换
        /// </summary>
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            int level/*树节点级别*/ = System.Convert.ToInt32(value);
            double indentSize/*缩进尺寸*/ = level * IndentUnitSize;
            Thickness margin = new Thickness(indentSize, 0, 0, 0);

            return margin;
        }

        /// <summary>
        /// 转换回
        /// </summary>
        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
