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

    public abstract class AxisBase : IChartAxis
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
        /// If axis is working in inverse mode
        /// </summary>
        public bool IsInverse { get; set; }

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
        public ArrowHeadBase ArrowHead { get; set; }

        /// <summary>
        /// Element used as tick
        /// </summary>
        public TickBase Tick { get; set; }

        /// <summary>
        /// Data incerement for tick placememnt
        /// </summary>
        public double TickIncrement { get; set; }

        /// <summary>
        /// Pixels per data
        /// </summary>
        protected double DataRatio;

        /// <summary>
        /// Orgin of data
        /// </summary>
        public double DataOrigin { get; set; }

        /// <summary>
        /// If set to true, axis <see cref="DataRatio"/> will be adjusted so that all the data are visible
        /// </summary>
        public bool AdjustDataRatio { get; set; }

        /// <summary>
        /// Informs if axis should be drawn
        /// </summary>
        protected bool isVisible;

        /// <summary>
        /// Visible range on data from width to height
        /// </summary>
        protected Size visibleRange;

        public AxisBase()
        {
            this.Color = Colors.Gray;
            this.IsInverse = false;
            this.StartPoint = default(ChartPoint);
            this.EndPoint = default(ChartPoint);
            this.ArrowHead = null;
            this.Thickness = 1.0f;
            this.Tick = null;
            this.TickIncrement = 0.0f;
            this.DataOrigin = 0.0d;
            this.StrokeStyle = default(CanvasStrokeStyle);
            this.AdjustDataRatio = false;
            this.isVisible = true;
            this.visibleRange = new Size(1, 1);
        }

        public abstract void DrawOnCanvas(CanvasDrawingSession drawingSession);
        public abstract float GetChartCoordinate(double coordinate);
        public abstract void PrepareAxis();
        public abstract void Update(Size windowSize, Thickness axesMargin, DataRange visibleRange, Point dataOrigin);
    }
}
