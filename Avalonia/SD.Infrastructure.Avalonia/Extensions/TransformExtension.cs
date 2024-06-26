using Avalonia;
using System.Diagnostics;

namespace SD.Infrastructure.Avalonia.Extensions
{
    /// <summary>
    /// 变换扩展
    /// </summary>
    public static class TransformExtension
    {
        /// <summary>
        /// 在指定点缩放
        /// </summary>
        public static Matrix ScaleAt(this Matrix matrix, double scaleX, double scaleY, double centerX, double centerY)
        {
            Matrix id = Matrix.Identity;
            Trace.WriteLine(id);

            Matrix kernel = new Matrix(scaleX, 0, 0, scaleY, centerX - scaleX * centerX, centerY - scaleY * centerY);
            matrix *= kernel;

            return matrix;
        }

        /// <summary>
        /// 计算两点差值
        /// </summary>
        public static Vector Subtract(Point point1, Point point2)
        {
            return new Vector(point1.X - point2.X, point1.Y - point2.Y);
        }
    }
}
