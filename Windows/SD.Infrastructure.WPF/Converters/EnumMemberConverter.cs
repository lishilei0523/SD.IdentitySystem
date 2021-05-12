using SD.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SD.Infrastructure.WPF.Converters
{
    /// <summary>
    /// 枚举描述转换器
    /// </summary>
    public class EnumMemberConverter : IValueConverter
    {
        /// <summary>
        /// 转换枚举描述
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            #region # 验证

            if (value == null)
            {
                return null;
            }

            #endregion

            Enum @enum = (Enum)value;
            string enumMember = @enum.GetEnumMember();

            return enumMember;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
