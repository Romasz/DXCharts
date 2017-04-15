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
    using System;
    using Interfaces;
    using Classes;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Geometry;
    using Windows.UI;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using System.Collections.Generic;

    public abstract class AxisBase : DependencyObject, IChartAxis, IInvertible
    {
        /// <summary>
        /// Color of the axis
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// End point of the axis
        /// </summary>
        public ChartPoint EndPoint { get; set; }

        /// <summary>
        /// Value indicating if element should be inverted
        /// </summary>
        public bool IsInverted
        {
            get { return (bool)GetValue(IsInvertedProperty); }
            set { SetValue(IsInvertedProperty, value); }
        }

        public static readonly DependencyProperty IsInvertedProperty =
            DependencyProperty.Register(nameof(IsInverted), typeof(bool), typeof(AxisBase), new PropertyMetadata(false));

        /// <summary>
        /// Axis start point
        /// </summary>
        public ChartPoint StartPoint { get; set; }

        /// <summary>
        /// Axis stroke style
        /// </summary>
        public CanvasStrokeStyle StrokeStyle { get; set; }

        /// <summary>
        /// Axis thickness
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// Element used as arrowhead
        /// </summary>
        public ArrowheadBase ArrowHead { get; set; }

        /// <summary>
        /// Element used as tick
        /// </summary>
        public TickBase Tick { get; set; }

        /// <summary>
        /// Data incerement for tick placememnt
        /// </summary>
        public double TickIncrement { get; set; }

        /// <summary>
        /// Number of ticks if <see cref="IsTickAutoAdjust"/> enabled 
        /// </summary>
        public double NumberOfAutoTicks { get; set; }

        /// <summary>
        /// Tells if tick positioning should be relative to chart range
        /// If so, then <see cref="TickIncrement"/> is number of ticks visible in range
        /// </summary>
        public bool IsTickAutoAdjust
        {
            get { return (bool)GetValue(IsTickAutoAdjustProperty); }
            set { SetValue(IsTickAutoAdjustProperty, value); }
        }

        public static readonly DependencyProperty IsTickAutoAdjustProperty =
            DependencyProperty.Register(nameof(IsTickAutoAdjust), typeof(bool), typeof(AxisBase), new PropertyMetadata(false));


        /// <summary>
        /// Tick lines minimum and maximum window's point
        /// </summary>
        protected DoubleRange TickLineRange { get; set; }

        /// <summary>
        /// If additional lines should be shown
        /// </summary>
        public bool IsTickLine
        {
            get { return (bool)GetValue(IsTickLineProperty); }
            set { SetValue(IsTickLineProperty, value); }
        }

        public static readonly DependencyProperty IsTickLineProperty =
            DependencyProperty.Register(nameof(IsTickLine), typeof(bool), typeof(AxisBase), new PropertyMetadata(false));

        /// <summary>
        /// Additional lines style
        /// </summary>
        public IChartLineElement TickLine { get; set; }

        /// <summary>
        /// Pixels per data
        /// </summary>
        protected double DataRatio;

        /// <summary>
        /// Orgin of data
        /// </summary>
        protected Point DataOrigin { get; set; }

        /// <summary>
        /// If set to true, axis <see cref="DataRatio"/> will be adjusted so that all the data are visible
        /// </summary>
        public bool AdjustDataRatio { get; set; }

        /// <summary>
        /// Informs if axis should be drawn
        /// </summary>
        protected bool isVisible;

        /// <summary>
        /// Visible range of data
        /// </summary>
        protected DoubleRange dataRange;

        public AxisBase()
        {
            this.Color = Colors.Gray;
            this.IsInverted = false;
            this.StartPoint = default(ChartPoint);
            this.EndPoint = default(ChartPoint);
            this.ArrowHead = null;
            this.Thickness = 1.0f;
            this.Tick = null;
            this.TickIncrement = 0.0f;
            this.DataOrigin = new Point(0d, 0d);
            this.StrokeStyle = default(CanvasStrokeStyle);
            this.AdjustDataRatio = false;
            this.isVisible = true;
            this.dataRange = new DoubleRange(1d, 1d);
        }

        public abstract List<Tuple<float, double>> CalculateTicks();
        public abstract void DrawOnCanvas(CanvasDrawingSession drawingSession);
        public abstract float GetChartCoordinate(double coordinate);
        public abstract void PrepareAxis(Point DataOrigin, double margin);
        public abstract void Update(Size windowSize, Thickness axesMargin, PointRange visibleRange);
    }
}
