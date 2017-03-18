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

        // ugly way for testing
        public List<Point> DataPoints => dataPoints.ToList();

        public double HorizontalRange
        {
            get { return ChartRange.Width / 2; }
            set
            {
                ChartRange = new DataRange(-value, ChartRange.Minimum.Y, value, ChartRange.Maximum.Y);
                RaiseProperty(nameof(ChartRange));
            }
        }

        public double VerticalRange
        {
            get { return ChartRange.Height / 2; }
            set
            {
                ChartRange = new DataRange(ChartRange.Minimum.X, -value, ChartRange.Maximum.X, value);
                RaiseProperty(nameof(ChartRange));
            }
        }

        private readonly Random random = new Random();

        public MainPage()
        {
            this.dataPoints.Add(new Point(1, 1));
            this.InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                double randX = random.NextDouble() * ChartRange.Width;
                double randY = random.NextDouble() * ChartRange.Height;
                dataPoints.Add(new Point(ChartRange.Minimum.X + randX, ChartRange.Minimum.Y + randY));
            }
            RaiseProperty(nameof(DataPoints));
        }
    }
}
