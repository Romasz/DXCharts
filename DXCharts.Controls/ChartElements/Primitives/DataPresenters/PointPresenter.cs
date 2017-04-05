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
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using System.Collections.Specialized;

    public class PointPresenter<T> : DependencyObject, IChartDataPresenter<T>
    {
        public IChartPointElement PointElement
        {
            get { return (IChartPointElement)GetValue(PointElementProperty); }
            set {
                SetValue(PointElementProperty, value); }
        }

        public static readonly DependencyProperty PointElementProperty =
            DependencyProperty.Register(nameof(PointElement), typeof(IChartPointElement), typeof(PointPresenter<T>), new PropertyMetadata(null, OnPropertyChangedStatic));

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // later
        }

        public Predicate<T> IsPointInRange { get; set; }

        public Func<T, ChartPoint> Convert { get; set; }

        public PointPresenter() { }

        public void PresentData(CanvasDrawingSession drawingSession, IEnumerable<T> data)
        {
            if (PointElement != null && IsPointInRange != null && Convert != null)
            {
                foreach (var point in data)
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
