// ******************************************************************
// Copyright (c) Tomasz Romaszkiewicz. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace DXCharts.Controls.Charts
{
    using ChartElements.Primitives;
    using Classes;
    using System;
    using Windows.Foundation;
    using Windows.UI.Xaml;

    public sealed class CartesianChart : ChartBase
    {
        public AxisBase HorizontalAxis
        {
            get { return (AxisBase)GetValue(HorizontalAxisProperty); }
            set { SetValue(HorizontalAxisProperty, value); }
        }

        public static readonly DependencyProperty HorizontalAxisProperty =
            DependencyProperty.Register(nameof(HorizontalAxis), typeof(AxisBase), typeof(CartesianChart), new PropertyMetadata(null, OnPropertyChangedStatic));

        public AxisBase VerticalAxis
        {
            get { return (AxisBase)GetValue(VerticalAxisProperty); }
            set { SetValue(VerticalAxisProperty, value); }
        }

        public static readonly DependencyProperty VerticalAxisProperty =
            DependencyProperty.Register(nameof(VerticalAxis), typeof(AxisBase), typeof(CartesianChart), new PropertyMetadata(null, OnPropertyChangedStatic));

        public Point AxesOrigin
        {
            get { return (Point)GetValue(AxesOriginProperty); }
            set { SetValue(AxesOriginProperty, value); }
        }

        public static readonly DependencyProperty AxesOriginProperty =
            DependencyProperty.Register(nameof(AxesOrigin), typeof(Point), typeof(CartesianChart), new PropertyMetadata(new Point(0, 0), OnPropertyChangedStatic));

        public Thickness AxesMargin
        {
            get { return (Thickness)GetValue(AxesMarginProperty); }
            set { SetValue(AxesMarginProperty, value); }
        }

        public static readonly DependencyProperty AxesMarginProperty =
            DependencyProperty.Register(nameof(AxesMargin), typeof(Thickness), typeof(CartesianChart), new PropertyMetadata(new Thickness(20, 20, 20, 20), OnPropertyChangedStatic));

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CartesianChart)?.OnPropertyChanged(d, e.Property);
        }

        public CartesianChart() : base() { }


        public override void CreateAxes()
        {
            this.HorizontalAxis.PrepareAxis(this.AxesOrigin, this.AxesMargin.Left);
            this.VerticalAxis.PrepareAxis(this.AxesOrigin, this.AxesMargin.Top);
            this.AxesCollection.Add(this.HorizontalAxis);
            this.AxesCollection.Add(this.VerticalAxis);
        }

        public override void UpdateAxes(Size windowSize)
        {
            this.HorizontalAxis?.Update(new Size(windowSize.Width - this.AxesMargin.Left - this.AxesMargin.Right, windowSize.Height - this.AxesMargin.Top - this.AxesMargin.Bottom), this.AxesMargin, this.VisibleRange);
            this.VerticalAxis?.Update(new Size(windowSize.Width - this.AxesMargin.Left - this.AxesMargin.Right, windowSize.Height - this.AxesMargin.Top - this.AxesMargin.Bottom), this.AxesMargin, this.VisibleRange);
        }

        public ChartPoint Convert(Point point) => new ChartPoint(this.HorizontalAxis.GetChartCoordinate(point.X), this.VerticalAxis.GetChartCoordinate(point.Y));
        public bool IsPointInRange(Point point) => this.VisibleRange.InHorizontalRange(point.X);
    }
}
