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

namespace TestApp
{
    using DXCharts.Controls.ChartElements.Interfaces;
    using DXCharts.Controls.ChartElements.Primitives;
    using DXCharts.Controls.Charts;
    using DXCharts.Controls.Classes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseProperty(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public PointRange ChartRange { get; set; } = new PointRange(-5, -5, 5, 5);

        public List<Point> dataPoints = new List<Point>();

        public List<DataSource<Point>> DataSources { get; set; } = new List<DataSource<Point>>();

        private PointPresenter<Point> PointPresenter;
        private LinePresenter<Point> LinePresenter;

        public int DataPointsCount { get; set; }

        public AxisPositions HorizontalAxisPosition { get; set; } = AxisPositions.HorizontalBottom;
        public AxisPositions VerticalAxisPosition { get; set; } = AxisPositions.VerticalLeft;

        private bool horizontalInverted;
        public bool HorizontalInverted
        {
            get { return horizontalInverted; }
            set
            {
                horizontalInverted = value;
                RaiseProperty(nameof(HorizontalInverted));
                this.TheChart.RedrawChart();
            }
        }

        private bool verticalInverted;
        public bool VerticalInverted
        {
            get { return verticalInverted; }
            set
            {
                verticalInverted = value;
                RaiseProperty(nameof(VerticalInverted));
                this.TheChart.RedrawChart();
            }
        }

        private bool isHorizontalLine = true;
        public bool IsHorizontalLine
        {
            get { return isHorizontalLine; }
            set
            {
                isHorizontalLine = value;
                RaiseProperty(nameof(IsHorizontalLine));
                this.TheChart.RedrawChart();
            }
        }

        private bool isVerticalLine = true;
        public bool IsVerticalLine
        {
            get { return isVerticalLine; }
            set
            {
                isVerticalLine = value;
                RaiseProperty(nameof(IsVerticalLine));
                this.TheChart.RedrawChart();
            }
        }

        private readonly Random random = new Random();

        public MainPage()
        {
            this.dataPoints.Add(new Point(1, 1));
            this.DataPointsCount = 1;
            this.DataSources.Add(new DataSource<Point> { Data = this.dataPoints });
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void ChartLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var chart = sender as CartesianChart;
            chart.Loaded -= ChartLoaded;
            this.PointPresenter = new PointPresenter<Point> { Convert = chart.Convert, IsPointInRange = chart.IsPointInRange, PointElement = new StandardPoint { Color = Colors.Red, Size = new Size(2, 2) } };
            this.LinePresenter = new LinePresenter<Point> { Convert = chart.Convert, IsPointInRange = chart.IsPointInRange, LineElement = new StandardLine { Color = Colors.Red, Thickness = 1 } };
            foreach (var dataSource in this.DataSources)
            {
                dataSource.DataPresenter = this.PointPresenter;
            }
        }

        private void AddPoints(int number)
        {
            for (int i = 0; i < number; i++)
            {
                double randX = random.NextDouble() * ChartRange.Width;
                double randY = random.NextDouble() * ChartRange.Height;
                dataPoints.Add(new Point(ChartRange.Minimum.X + randX, ChartRange.Minimum.Y + randY));
            }
            this.DataPointsCount = dataPoints.Count;
            this.RaiseProperty(nameof(DataPointsCount));
            this.TheChart.RedrawChart();
        }

        private void SmallButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) => AddPoints(10);
        private void BigButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) => AddPoints(10000);

        private bool isClicked = false;
        private Point startPoint;

        private void CartesianChart_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            startPoint = e.GetCurrentPoint(this).Position;
            isClicked = true;
        }

        private void CartesianChart_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (isClicked)
            {
                var position = e.GetCurrentPoint(this).Position;
                var relative = new Point((startPoint.X - position.X) * 0.1, (startPoint.Y - position.Y) * 0.1);
                this.startPoint = position;
                var minPoint = new Point(this.ChartRange.Minimum.X - relative.X, this.ChartRange.Minimum.Y + relative.Y);
                var maxPoint = new Point(this.ChartRange.Maximum.X - relative.X, this.ChartRange.Maximum.Y + relative.Y);
                this.ChartRange = new PointRange(minPoint, maxPoint);
                RaiseProperty(nameof(ChartRange));
            }
        }

        private void CartesianChart_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            isClicked = false;
        }

        private void CartesianChart_PointerWheelChanged(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point minPoint, maxPoint;
            if (e.GetCurrentPoint(this).Properties.MouseWheelDelta < 0)
            {
                minPoint = new Point(this.ChartRange.Minimum.X * 1.1, this.ChartRange.Minimum.Y * 1.1);
                maxPoint = new Point(this.ChartRange.Maximum.X * 1.1, this.ChartRange.Maximum.Y * 1.1);
            }
            else
            {
                minPoint = new Point(this.ChartRange.Minimum.X / 1.1, this.ChartRange.Minimum.Y / 1.1);
                maxPoint = new Point(this.ChartRange.Maximum.X / 1.1, this.ChartRange.Maximum.Y / 1.1);
            }
            this.ChartRange = new PointRange(minPoint, maxPoint);
            RaiseProperty(nameof(ChartRange));
        }

        private void PointStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cBox = sender as ComboBox;
            if (cBox.SelectedIndex == 1)
            {
                foreach (var dataSource in this.DataSources)
                {
                    dataSource.DataPresenter = this.LinePresenter;
                }
            }
            else
            {
                foreach (var dataSource in this.DataSources)
                {
                    dataSource.DataPresenter = this.PointPresenter;
                }
            }
            this.TheChart.RedrawChart();
        }

        private void HorizontalPlacement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cBox = sender as ComboBox;
            if (cBox.SelectedIndex == 0)
            {
                this.HorizontalAxisPosition = AxisPositions.HorizontalBottom;
            }
            else if (cBox.SelectedIndex == 1)
            {
                this.HorizontalAxisPosition = AxisPositions.HorizontalTop;
            }
            else if (cBox.SelectedIndex == 2)
            {
                this.HorizontalAxisPosition = AxisPositions.HorizontalFree;
            }
            this.RaiseProperty(nameof(HorizontalAxisPosition));
            this.TheChart.RedrawChart();
        }

        private void VerticalPlacement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cBox = sender as ComboBox;
            if (cBox.SelectedIndex == 0)
            {
                this.VerticalAxisPosition = AxisPositions.VerticalLeft;
            }
            else if (cBox.SelectedIndex == 1)
            {
                this.VerticalAxisPosition = AxisPositions.VerticalRight;
            }
            else if (cBox.SelectedIndex == 2)
            {
                this.VerticalAxisPosition = AxisPositions.VerticalFree;
            }
            this.RaiseProperty(nameof(VerticalAxisPosition));
            this.TheChart.RedrawChart();
        }

    }
}
