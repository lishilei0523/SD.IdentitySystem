using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using SD.Infrastructure.Avalonia.Extensions;
using SD.Infrastructure.Avalonia.Visual2Ds;

namespace SD.Infrastructure.Avalonia.CustomControls
{
    /// <summary>
    /// 可缩放Canvas
    /// </summary>
    public class ScalableCanvas : Canvas
    {
        #region # 字段及构造器

        /// <summary>
        /// 缩放系数依赖属性
        /// </summary>
        public static readonly StyledProperty<float> ScaledFactorProperty;

        /// <summary>
        /// 背景图像依赖属性
        /// </summary>
        public static readonly StyledProperty<Image> BackgroundImageProperty;

        /// <summary>
        /// 显示网格线依赖属性
        /// </summary>
        public static readonly StyledProperty<bool> ShowGridLinesProperty;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ScalableCanvas()
        {
            ScaledFactorProperty = AvaloniaProperty.Register<ScalableCanvas, float>(nameof(ScaledFactor), 1.1f);
            BackgroundImageProperty = AvaloniaProperty.Register<ScalableCanvas, Image>(nameof(BackgroundImage), null);
            ShowGridLinesProperty = AvaloniaProperty.Register<ScalableCanvas, bool>(nameof(ShowGridLines), false);
            BackgroundImageProperty.Changed.AddClassHandler<ScalableCanvas>(OnBackgroundImageChanged);
            ShowGridLinesProperty.Changed.AddClassHandler<ScalableCanvas>(OnShowGridLinesChanged);
        }

        /// <summary>
        /// 矫正起始位置
        /// </summary>
        private Point? _rectifiedStartPosition;

        /// <summary>
        /// 变换矩阵
        /// </summary>
        private readonly MatrixTransform _matrixTransform;

        /// <summary>
        /// 默认构造器
        /// </summary>
        public ScalableCanvas()
        {
            this._matrixTransform = new MatrixTransform();
            base.PointerPressed += this.OnMouseDown;
            base.PointerMoved += this.OnMouseMove;
            base.PointerWheelChanged += this.OnMouseWheel;
            base.PointerReleased += this.OnMouseUp;
            base.ClipToBounds = true;
            base.HorizontalAlignment = HorizontalAlignment.Stretch;
            base.VerticalAlignment = VerticalAlignment.Stretch;
            base.Background = new SolidColorBrush(Colors.Transparent);
        }

        #endregion

        #region # 属性

        #region 依赖属性 - 缩放系数 —— float ScaledFactor
        /// <summary>
        /// 依赖属性 - 缩放系数
        /// </summary>
        public float ScaledFactor
        {
            get => this.GetValue(ScaledFactorProperty);
            set => this.SetValue(ScaledFactorProperty, value);
        }
        #endregion

        #region 依赖属性 - 背景图像 —— Image BackgroundImage
        /// <summary>
        /// 依赖属性 - 背景图像
        /// </summary>
        public Image BackgroundImage
        {
            get => this.GetValue(BackgroundImageProperty);
            set => this.SetValue(BackgroundImageProperty, value);
        }
        #endregion

        #region 依赖属性 - 显示网格线 —— bool ShowGridLines
        /// <summary>
        /// 依赖属性 - 显示网格线
        /// </summary>
        public bool ShowGridLines
        {
            get => this.GetValue(ShowGridLinesProperty);
            set => this.SetValue(ShowGridLinesProperty, value);
        }
        #endregion

        #region 只读属性 - 缩放率 —— double ScaledRatio
        /// <summary>
        /// 只读属性 - 缩放率
        /// </summary>
        public double ScaledRatio
        {
            get => this._matrixTransform.Matrix.M11;
        }
        #endregion

        #region 只读属性 - 变换矩阵 —— MatrixTransform MatrixTransform
        /// <summary>
        /// 只读属性 - 变换矩阵
        /// </summary>
        public MatrixTransform MatrixTransform
        {
            get => this._matrixTransform;
        }
        #endregion

        #endregion

        #region # 方法

        #region 背景图像改变事件 —— static void OnBackgroundImageChanged(ScalableCanvas canvas...
        /// <summary>
        /// 背景图像改变事件
        /// </summary>
        private static void OnBackgroundImageChanged(ScalableCanvas canvas, AvaloniaPropertyChangedEventArgs eventArgs)
        {
            Image oldImage = eventArgs.OldValue as Image;
            Image newImage = eventArgs.NewValue as Image;
            if (oldImage != null && canvas.Children.Contains(oldImage))
            {
                canvas.Children.Remove(oldImage);
            }
            if (newImage != null)
            {
                newImage.Stretch = Stretch.Uniform;
                //SetZIndex(newImage, int.MinValue);TODO 完善
                //DraggableCanvas.SetDraggable(newImage, false);
                //ResizableCanvas.SetResizable(newImage, false);
                canvas.Children.Add(newImage);
            }
        }
        #endregion

        #region 显示网格线改变事件 —— static void OnShowGridLinesChanged(ScalableCanvas canvas...
        /// <summary>
        /// 显示网格线改变事件
        /// </summary>
        private static void OnShowGridLinesChanged(ScalableCanvas canvas, AvaloniaPropertyChangedEventArgs eventArgs)
        {
            bool oldShow = (bool)eventArgs.OldValue;
            bool newShow = (bool)eventArgs.NewValue;
            if (oldShow && newShow)
            {
                return;
            }
            if (oldShow && !newShow)
            {
                foreach (Control element in canvas.Children)
                {
                    if (element is GridLinesVisual2D)
                    {
                        canvas.Children.Remove(element);
                    }
                }
            }
            if (!oldShow && newShow)
            {
                GridLinesVisual2D gridLines = new GridLinesVisual2D();
                //SetZIndex(gridLines, int.MaxValue);TODO 完善
                //DraggableCanvas.SetDraggable(gridLines, false);
                //ResizableCanvas.SetResizable(gridLines, false);
                canvas.Children.Add(gridLines);
            }
        }
        #endregion

        #region 获取矫正左边距 —— double GetRectifiedLeft(Visual element)
        /// <summary>
        /// 获取矫正左边距
        /// </summary>
        /// <param name="element">UI元素</param>
        /// <returns>矫正左边距</returns>
        public double GetRectifiedLeft(Visual element)
        {
            double left = GetLeft(element);
            left = double.IsNaN(left) ? 0 : left;
            double retifiedLeft = left / this.ScaledRatio;

            return retifiedLeft;
        }
        #endregion

        #region 获取矫正上边距 —— double GetRectifiedTop(Visual element)
        /// <summary>
        /// 获取矫正上边距
        /// </summary>
        /// <param name="element">UI元素</param>
        /// <returns>矫正上边距</returns>
        public double GetRectifiedTop(Visual element)
        {
            double top = GetTop(element);
            top = double.IsNaN(top) ? 0 : top;
            double retifiedTop = top / this.ScaledRatio;

            return retifiedTop;
        }
        #endregion

        #region 鼠标按下事件 —— void OnMouseDown(object sender, PointerPressedEventArgs eventArgs)
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        private void OnMouseDown(object sender, PointerPressedEventArgs eventArgs)
        {
            PointerPoint pointerPoint = eventArgs.GetCurrentPoint((Visual)sender);
            if (pointerPoint.Properties.IsMiddleButtonPressed)
            {
                Point mousePosition = eventArgs.GetPosition(this);
                this._rectifiedStartPosition = this._matrixTransform.Matrix.Invert()!.Transform(mousePosition);
            }
        }
        #endregion

        #region 鼠标移动事件 —— void OnMouseMove(object sender, PointerEventArgs eventArgs)
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        private void OnMouseMove(object sender, PointerEventArgs eventArgs)
        {
            PointerPoint pointerPoint = eventArgs.GetCurrentPoint((Visual)sender);
            if (pointerPoint.Properties.IsMiddleButtonPressed && this._rectifiedStartPosition.HasValue)
            {
                //设置光标
                this.Cursor = new Cursor(StandardCursorType.Hand);

                Point mousePosition = eventArgs.GetPosition(this);
                Point rectifiedMousePosition = this._matrixTransform.Value.Invert()!.Transform(mousePosition);
                Vector delta = TransformExtension.Subtract(rectifiedMousePosition, this._rectifiedStartPosition.Value);
                TranslateTransform transform = new TranslateTransform(delta.X, delta.Y);
                this._matrixTransform.Matrix = transform.Value * this._matrixTransform.Matrix;
                foreach (Control element in this.Children)
                {
                    element.RenderTransform = this._matrixTransform;
                }
            }
        }
        #endregion

        #region 鼠标滚轮事件 —— void OnMouseWheel(object sender, MouseWheelEventArgs eventArgs)
        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        private void OnMouseWheel(object sender, PointerWheelEventArgs eventArgs)
        {
            float scaledFactor = this.ScaledFactor;
            if (eventArgs.Delta.Y < 0)
            {
                scaledFactor = 1f / scaledFactor;
            }

            Point mousePostion = eventArgs.GetPosition(this);
            Matrix scaledMatrix = this._matrixTransform.Matrix;
            scaledMatrix = scaledMatrix.ScaleAt(scaledFactor, scaledFactor, mousePostion.X, mousePostion.Y);
            this._matrixTransform.Matrix = scaledMatrix;
            foreach (Control element in this.Children)
            {
                double x = GetLeft(element);
                double y = GetTop(element);
                double scaledX = x * scaledFactor;
                double scaledY = y * scaledFactor;
                SetLeft(element, scaledX);
                SetTop(element, scaledY);
                element.RenderTransform = this._matrixTransform;
            }
        }
        #endregion

        #region 鼠标松开事件 —— void OnMouseUp(object sender, PointerReleasedEventArgs eventArgs)
        /// <summary>
        /// 鼠标松开事件
        /// </summary>
        private void OnMouseUp(object sender, PointerReleasedEventArgs eventArgs)
        {
            //设置光标
            this.Cursor = new Cursor(StandardCursorType.Arrow);

            this._rectifiedStartPosition = null;
        }
        #endregion 

        #endregion
    }
}
