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

namespace DXCharts.Controls.ChartElements.Primitives
{
    using Interfaces;
    using Classes;
    using Microsoft.Graphics.Canvas;
    using Windows.UI;
    using System;
    using System.Collections.Generic;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using System.Collections.Specialized;

    public class PointPresenter : DependencyObject, IChartDataPresenter
    {
        public IChartPointElement PointElement
        {
            get { return (IChartPointElement)GetValue(PointElementProperty); }
            set {
                SetValue(PointElementProperty, value); }
        }

        public static readonly DependencyProperty PointElementProperty =
            DependencyProperty.Register(nameof(PointElement), typeof(IChartPointElement), typeof(PointPresenter), new PropertyMetadata(null, OnPropertyChangedStatic));

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // later
        }

        private static void OnDataPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PointPresenter)?.CollectionChanged?.Invoke((d as PointPresenter).Data, null);
        }

        public IEnumerable<Point> Data
        {
            get { return (IEnumerable<Point>)GetValue(DataProperty); }
            set {
                SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(IEnumerable<Point>), typeof(PointPresenter), new PropertyMetadata(null, OnDataPropertyChangedStatic));

        public Predicate<Point> IsPointInRange { get; set; }

        public Func<Point, ChartPoint> Convert { get; set; }

        public PointPresenter() { }

        public void PresentData(CanvasDrawingSession drawingSession)
        {
            if (PointElement != null && IsPointInRange != null && Convert != null)
            {
                foreach (var point in Data)
                {
                    if (IsPointInRange(point))
                    {
                        this.PointElement.Position = Convert(point);
                        this.PointElement.DrawOnCanvas(drawingSession);
                    }
                }
            }

        }
    }
}
