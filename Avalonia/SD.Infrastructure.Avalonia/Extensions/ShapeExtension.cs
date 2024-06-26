using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Infrastructure.Avalonia.Extensions
{
    /// <summary>
    /// 形状扩展
    /// </summary>
    public static class ShapeExtension
    {
        #region # 顺序化点集 —— static IList<Point> Sequentialize(this IList<Point> points)
        /// <summary>
        /// 顺序化点集
        /// </summary>
        /// <param name="points">点集</param>
        /// <returns>顺序化点集</returns>
        /// <remarks>用于排列多边形点集</remarks>
        public static IList<Point> Sequentialize(this IList<Point> points)
        {
            #region # 验证

            if (points == null)
            {
                throw new ArgumentNullException(nameof(points), "点集不可为null！");
            }
            if (!points.Any())
            {
                return new List<Point>();
            }

            #endregion

            double meanX = points.Average(point => point.X);
            double meanY = points.Average(point => point.Y);
            IOrderedEnumerable<Point> orderedPoints = points.OrderBy(point => Math.Atan2(point.Y - meanY, point.X - meanX));

            return new List<Point>(orderedPoints);
        }
        #endregion
    }
}
