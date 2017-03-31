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
    using ChartElements.Interfaces;
    using ChartElements.Primitives;
    using Classes;
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

        public Point DataOrigin
        {
            get { return (Point)GetValue(DataOriginProperty); }
            set { SetValue(DataOriginProperty, value); }
        }

        public static readonly DependencyProperty DataOriginProperty =
            DependencyProperty.Register(nameof(DataOrigin), typeof(Point), typeof(CartesianChart), new PropertyMetadata(new Point(0, 0), OnPropertyChangedStatic));

        public Thickness AxesMargin
        {
            get { return (Thickness)GetValue(AxesMarginProperty); }
            set { SetValue(AxesMarginProperty, value); }
        }

        public static readonly DependencyProperty AxesMarginProperty =
            DependencyProperty.Register(nameof(AxesMargin), typeof(Thickness), typeof(CartesianChart), new PropertyMetadata(new Thickness(5,5,5,5), OnPropertyChangedStatic));

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CartesianChart)?.OnPropertyChanged(d, e.Property);
        }

        public CartesianChart() : base() { }


        public override void CreateAxes()
        {
            AxesCollection.Add(HorizontalAxis);
            AxesCollection.Add(VerticalAxis);
            HorizontalAxis.DataOrigin = DataOrigin.X;
            VerticalAxis.DataOrigin = DataOrigin.Y;
        }

        public override void PrepareDataPresenter()
        {
            if (DataPresenter != null)
            {
                DataPresenter.Convert = Convert;
                DataPresenter.IsPointInRange = IsInRange;
                DataPresenter.CollectionChanged += DataPresenter_CollectionChanged;
            }
        }

        private void DataPresenter_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // redraw the chart
            OnPropertyChanged(this, null);
        }

        private bool IsInRange(Point point) => VisibleRange.InRange(point);

        private ChartPoint Convert(Point point)
        {
            return new ChartPoint(GetXCoordinate(point.X), GetYCoordinate(point.Y));
        }

        private float GetXCoordinate(double data)
        {
            return (float)(rootCanvas.ActualWidth * (data - VisibleRange.Minimum.X) / VisibleRange.Width);
        }

        private float GetYCoordinate(double data)
        {
            return (float)(rootCanvas.ActualHeight * (VisibleRange.Maximum.Y - data) / VisibleRange.Height);
        }
    }
}
