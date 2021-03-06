﻿// ******************************************************************
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
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using System.Collections.Specialized;
    using System.Linq;

    public class LinePresenter<T> : DependencyObject, IChartDataPresenter<T>
    {
        public IChartLineElement LineElement
        {
            get { return (IChartLineElement)GetValue(LineElementProperty); }
            set { SetValue(LineElementProperty, value); }
        }

        public static readonly DependencyProperty LineElementProperty =
            DependencyProperty.Register(nameof(LineElement), typeof(IChartLineElement), typeof(LinePresenter<T>), new PropertyMetadata(null, OnPropertyChangedStatic));

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // later
        }

        public Predicate<T> IsPointInRange { get; set; }

        public Func<T, ChartPoint> Convert { get; set; }

        public LinePresenter() { }

        public void PresentData(CanvasDrawingSession drawingSession, IEnumerable<T> data)
        {
            if (this.LineElement != null && this.IsPointInRange != null && this.Convert != null && data != null && data.Any())
            {
                var sorted = data.Where(d => this.IsPointInRange(d)).Select(d => this.Convert(d)).ToArray();
                //var sorted = this.Data.Where(d => this.IsPointInRange(d)).OrderBy(d => d.X).Select(d => this.Convert(d)).ToArray();
                if (sorted.Length > 1)
                {
                    this.LineElement.EndPoint = sorted[0];
                    foreach (var point in sorted.Skip(1))
                    {
                        this.LineElement.StartPoint = this.LineElement.EndPoint;
                        this.LineElement.EndPoint = point;
                        this.LineElement.DrawOnCanvas(drawingSession);
                    }
                }
            }

        }
    }
}
