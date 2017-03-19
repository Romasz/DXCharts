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
    using DXCharts.Controls.Classes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Windows.Foundation;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseProperty(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public DataRange ChartRange { get; set; } = new DataRange(-10, -5, 10, 5);

        public List<Point> dataPoints = new List<Point>();

        public IChartDataPresenter DataPresenter { get; set; }

        // ugly way for testing
        public List<Point> DataPoints => dataPoints.ToList();

        public double HorizontalRange
        {
            get { return ChartRange.Width / 2; }
            set
            {
                this.ChartRange = new DataRange(-value, this.ChartRange.Minimum.Y, value, this.ChartRange.Maximum.Y);
                this.RaiseProperty(nameof(this.ChartRange));
            }
        }

        public double VerticalRange
        {
            get { return this.ChartRange.Height / 2; }
            set
            {
                this.ChartRange = new DataRange(this.ChartRange.Minimum.X, -value, this.ChartRange.Maximum.X, value);
                this.RaiseProperty(nameof(this.ChartRange));
            }
        }

        private readonly Random random = new Random();

        public MainPage()
        {
            this.dataPoints.Add(new Point(1, 1));
            this.InitializeComponent();
            this.DataContext = this;
            this.DataPresenter = this.Resources["PointPresenter"] as IChartDataPresenter;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                double randX = random.NextDouble() * ChartRange.Width;
                double randY = random.NextDouble() * ChartRange.Height;
                dataPoints.Add(new Point(ChartRange.Minimum.X + randX, ChartRange.Minimum.Y + randY));
            }
            this.RaiseProperty(nameof(DataPoints));
        }

        private void PointButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.DataPresenter = this.Resources["PointPresenter"] as IChartDataPresenter;
            this.RaiseProperty(nameof(DataPresenter));
        }

        private void LineButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.DataPresenter = this.Resources["LinePresenter"] as IChartDataPresenter;
            this.RaiseProperty(nameof(DataPresenter));
        }
    }
}
