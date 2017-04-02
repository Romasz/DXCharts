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
    using Classes;
    using Microsoft.Graphics.Canvas;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using System.Collections.Generic;

    public class StandardAxis : AxisBase
    {
        /// <summary>
        /// Position of the axis
        /// </summary>
        public AxisPositions Position
        {
            get { return (AxisPositions)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(AxisPositions), typeof(StandardAxis), new PropertyMetadata(AxisPositions.None));

        private bool IsHorizontal => this.Position != AxisPositions.None && (int)this.Position < 4;
        private bool IsVertical => (int)this.Position > 3;
        private double offset;

        public StandardAxis() : base()
        {
            this.Position = AxisPositions.None;
            this.offset = 0d;
        }

        public override void PrepareAxis(Point dataOrigin, double margin)
        {
            this.DataOrigin = dataOrigin;
            this.offset = margin;
        }

        public override float GetChartCoordinate(double coordinate)
        {
            if (!this.IsInverted == !this.IsVertical)
            {
                return (float)(this.offset + DataRatio * (coordinate - dataRange.Minimum));
            }
            else
            {
                return (float)(this.offset + DataRatio * (dataRange.Maximum - coordinate));
            }
        }

        public override void Update(Size windowSize, Thickness axesMargin, PointRange visibleRange)
        {
            this.ArrowHead.IsInverted = this.IsInverted;
            switch (this.Position)
            {
                case AxisPositions.HorizontalBottom:
                    this.dataRange = new DoubleRange(visibleRange.Minimum.X, visibleRange.Maximum.X);
                    this.DataRatio = windowSize.Width / this.dataRange.Width;
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(windowSize.Height - axesMargin.Bottom));
                    this.EndPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(windowSize.Height - axesMargin.Bottom));
                    this.ArrowHead.Position = !this.IsInverted ? this.EndPoint : this.StartPoint;
                    this.TickLineRange = new DoubleRange(axesMargin.Top, axesMargin.Top + windowSize.Height);
                    break;
                case AxisPositions.HorizontalTop:
                    this.dataRange = new DoubleRange(visibleRange.Minimum.X, visibleRange.Maximum.X);
                    this.DataRatio = windowSize.Width / this.dataRange.Width;
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(axesMargin.Top));
                    this.EndPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(axesMargin.Top));
                    this.ArrowHead.Position = !this.IsInverted ? this.EndPoint : this.StartPoint;
                    this.TickLineRange = new DoubleRange(axesMargin.Top, axesMargin.Top + windowSize.Height);
                    break;
                case AxisPositions.HorizontalFree:
                    this.dataRange = new DoubleRange(visibleRange.Minimum.X, visibleRange.Maximum.X);
                    this.DataRatio = windowSize.Width / this.dataRange.Width;
                    this.isVisible = visibleRange.InVerticalRange(this.DataOrigin.Y);
                    if (!this.IsInverted)
                    {
                        this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(axesMargin.Top + windowSize.Height * (visibleRange.Maximum.Y - DataOrigin.Y) / visibleRange.Height));
                        this.EndPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(axesMargin.Top + windowSize.Height * (visibleRange.Maximum.Y - DataOrigin.Y) / visibleRange.Height));
                        this.ArrowHead.Position = this.EndPoint;
                    }
                    else
                    {
                        this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(axesMargin.Top + windowSize.Height * (DataOrigin.Y - visibleRange.Minimum.Y) / visibleRange.Height));
                        this.EndPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(axesMargin.Top + windowSize.Height * (DataOrigin.Y - visibleRange.Minimum.Y) / visibleRange.Height));
                        this.ArrowHead.Position = this.StartPoint;
                    }
                    this.TickLineRange = new DoubleRange(axesMargin.Top, axesMargin.Top + windowSize.Height);
                    break;
                case AxisPositions.VerticalLeft:
                    this.dataRange = new DoubleRange(visibleRange.Minimum.Y, visibleRange.Maximum.Y);
                    this.DataRatio = windowSize.Height / this.dataRange.Width;
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)axesMargin.Top);
                    this.EndPoint = new ChartPoint((float)axesMargin.Left, (float)(windowSize.Height - axesMargin.Bottom));
                    this.ArrowHead.Position = this.IsInverted ? this.EndPoint : this.StartPoint;
                    this.TickLineRange = new DoubleRange(axesMargin.Left, axesMargin.Left + windowSize.Width);
                    break;
                case AxisPositions.VerticalRight:
                    this.dataRange = new DoubleRange(visibleRange.Minimum.Y, visibleRange.Maximum.Y);
                    this.DataRatio = windowSize.Height / this.dataRange.Width;
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)axesMargin.Top);
                    this.EndPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(windowSize.Height - axesMargin.Bottom));
                    this.ArrowHead.Position = this.IsInverted ? this.EndPoint : this.StartPoint;
                    this.TickLineRange = new DoubleRange(axesMargin.Left, axesMargin.Left + windowSize.Width);
                    break;
                case AxisPositions.VerticalFree:
                    this.dataRange = new DoubleRange(visibleRange.Minimum.Y, visibleRange.Maximum.Y);
                    this.DataRatio = windowSize.Height / this.dataRange.Width;
                    this.isVisible = visibleRange.InHorizontalRange(this.DataOrigin.X);
                    if (!this.IsInverted)
                    {
                        this.StartPoint = new ChartPoint((float)(axesMargin.Left + windowSize.Width * (DataOrigin.X - visibleRange.Minimum.X) / visibleRange.Width), (float)axesMargin.Top);
                        this.EndPoint = new ChartPoint((float)(axesMargin.Left + windowSize.Width * (DataOrigin.X - visibleRange.Minimum.X) / visibleRange.Width), (float)(windowSize.Height - axesMargin.Bottom));
                        this.ArrowHead.Position = this.StartPoint;
                    }
                    else
                    {
                        this.StartPoint = new ChartPoint((float)(axesMargin.Left + windowSize.Width * (visibleRange.Maximum.X - DataOrigin.X) / visibleRange.Width), (float)axesMargin.Top);
                        this.EndPoint = new ChartPoint((float)(axesMargin.Left + windowSize.Width * (visibleRange.Maximum.X - DataOrigin.X) / visibleRange.Width), (float)(windowSize.Height - axesMargin.Bottom));
                        this.ArrowHead.Position = this.EndPoint;
                    }
                    this.TickLineRange = new DoubleRange(axesMargin.Left, axesMargin.Left + windowSize.Width);
                    break;
                case AxisPositions.None:
                default:
                    this.isVisible = false;
                    break;
            }
        }

        public override List<Tuple<float, double>> CalculateTicks()
        {
            List<Tuple<float, double>> listOfTicks = new List<Tuple<float, double>>();
            if (this.dataRange.InRange(!this.IsVertical ? this.DataOrigin.X : this.DataOrigin.Y))
            {
                double data = !this.IsVertical ? this.DataOrigin.X : this.DataOrigin.Y;

                while (data <= this.dataRange.Maximum)
                {
                    listOfTicks.Add(new Tuple<float, double>(GetChartCoordinate(data), data));
                    data += this.TickIncrement;
                }

                data = (!this.IsVertical ? this.DataOrigin.X : this.DataOrigin.Y) - this.TickIncrement;
                while (data >= this.dataRange.Minimum)
                {
                    listOfTicks.Add(new Tuple<float, double>(GetChartCoordinate(data), data));
                    data -= this.TickIncrement;
                }
            }
            else
            {
                // get nearest tick
                double data = Math.Ceiling(this.dataRange.Minimum / this.TickIncrement) * this.TickIncrement;
                do
                {
                    listOfTicks.Add(new Tuple<float, double>(GetChartCoordinate(data), data));
                    data += this.TickIncrement;
                } while (data <= this.dataRange.Maximum);
            }

            return listOfTicks;
        }

        public override void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            List<Tuple<float, double>> ticks = null;

            if (this.isVisible)
            {
                drawingSession.DrawLine(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y, this.Color, (float)this.Thickness, this.StrokeStyle);
                this.ArrowHead?.DrawOnCanvas(drawingSession);

                if (this.Tick != null && this.TickIncrement > 0.0f)
                {
                    ticks = CalculateTicks();
                    foreach (var tick in ticks)
                    {
                        this.Tick.Position = IsHorizontal ? new ChartPoint(tick.Item1, this.StartPoint.Y) : new ChartPoint(this.StartPoint.X, tick.Item1);
                        this.Tick.Label = $"{tick.Item2:0.0}";
                        this.Tick.DrawOnCanvas(drawingSession);
                    }
                }
            }

            if (this.IsTickLine && this.TickLine != null && this.TickIncrement > 0.0f)
            {
                if (ticks == null)
                {
                    ticks = CalculateTicks();
                }

                foreach (var tick in ticks)
                {
                    this.TickLine.StartPoint = IsHorizontal ? new ChartPoint(tick.Item1, (float)this.TickLineRange.Minimum) : new ChartPoint((float)this.TickLineRange.Minimum, tick.Item1);
                    this.TickLine.EndPoint = IsHorizontal ? new ChartPoint(tick.Item1, (float)this.TickLineRange.Maximum) : new ChartPoint((float)this.TickLineRange.Maximum, tick.Item1);
                    this.TickLine.DrawOnCanvas(drawingSession);
                }
            }
        }
    }
}
