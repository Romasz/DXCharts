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
    using Classes;
    using Windows.Foundation;
    using Windows.UI.Xaml;

    public sealed class CartesianChart : ChartBase
    {
        public IChartAxis HorizontalAxis
        {
            get { return (IChartAxis)GetValue(HorizontalAxisProperty); }
            set { SetValue(HorizontalAxisProperty, value); }
        }

        public static readonly DependencyProperty HorizontalAxisProperty =
            DependencyProperty.Register(nameof(HorizontalAxis), typeof(IChartAxis), typeof(CartesianChart), new PropertyMetadata(null, OnPropertyChangedStatic));

        public IChartAxis VerticalAxis
        {
            get { return (IChartAxis)GetValue(VerticalAxisProperty); }
            set { SetValue(VerticalAxisProperty, value); }
        }

        public static readonly DependencyProperty VerticalAxisProperty =
            DependencyProperty.Register(nameof(VerticalAxis), typeof(IChartAxis), typeof(CartesianChart), new PropertyMetadata(null, OnPropertyChangedStatic));


        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CartesianChart)?.OnPropertyChanged(d, e.Property);
        }

        public Point DataOrigin
        {
            get { return (Point)GetValue(DataOriginProperty); }
            set { SetValue(DataOriginProperty, value); }
        }

        public static readonly DependencyProperty DataOriginProperty =
            DependencyProperty.Register(nameof(DataOrigin), typeof(Point), typeof(CartesianChart), new PropertyMetadata(new Point(0, 0), OnPropertyChangedStatic));


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

        public override void UpdateAxes(ElementSize newSize)
        {
            if (HorizontalAxis != null && VisibleRange.InVerticalRange(DataOrigin.Y))
            {
                HorizontalAxis.StartPoint = new ChartPoint(0.0f, GetYCoordinate(DataOrigin.Y));
                HorizontalAxis.EndPoint = new ChartPoint(newSize.Width, GetYCoordinate(DataOrigin.Y));
                HorizontalAxis.DataRatio = newSize.Width / VisibleRange.Width;
                HorizontalAxis.OriginPoint = GetXCoordinate(DataOrigin.X);
            }

            if (VerticalAxis != null && VisibleRange.InHorizontalRange(DataOrigin.X))
            {
                VerticalAxis.EndPoint = new ChartPoint(GetXCoordinate(DataOrigin.X), 0.0f);
                VerticalAxis.StartPoint = new ChartPoint(GetXCoordinate(DataOrigin.X), newSize.Height);
                VerticalAxis.DataRatio = newSize.Height / VisibleRange.Height;
                VerticalAxis.OriginPoint = GetYCoordinate(DataOrigin.Y);
            }
        }

    }
}
