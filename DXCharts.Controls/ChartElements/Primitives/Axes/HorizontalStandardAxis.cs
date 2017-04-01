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

    public class HorizontalStandardAxis : AxisBase
    {
        /// <summary>
        /// Position of the axis
        /// </summary>
        public HorizontalAxisPositions Position { get; set; }

        public HorizontalStandardAxis() : base()
        {
            this.Position = HorizontalAxisPositions.Bottom;
        }

        public override void PrepareAxis(Point dataOrigin)
        {
            this.ArrowHead.IsInverted = this.IsInverted;
            this.DataOrigin = dataOrigin;
        }

        public override float GetChartCoordinate(double coordinate)
        {
            if (!this.IsInverted)
            {
                return (float)(DataRatio * (coordinate - visibleRange.Width));
            }
            else
            {
                return (float)(DataRatio * (visibleRange.Height - coordinate));
            }
        }

        public override void Update(Size windowSize, Thickness axesMargin, DataRange visibleRange)
        {
            this.visibleRange = new Size(visibleRange.Minimum.X, visibleRange.Maximum.X);
            this.DataRatio = windowSize.Width / visibleRange.Width;

            switch (Position)
            {
                case HorizontalAxisPositions.Bottom:
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(windowSize.Height - axesMargin.Bottom));
                    this.StartPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(windowSize.Height - axesMargin.Bottom));
                    break;
                case HorizontalAxisPositions.Middle:
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(windowSize.Height / 2));
                    this.StartPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(windowSize.Height / 2));
                    break;
                case HorizontalAxisPositions.Top:
                    this.isVisible = true;
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(axesMargin.Top));
                    this.StartPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(axesMargin.Top));
                    break;
                case HorizontalAxisPositions.Free:
                default:
                    this.isVisible = visibleRange.InVerticalRange(DataOrigin.Y);
                    this.StartPoint = new ChartPoint((float)axesMargin.Left, (float)(windowSize.Height * (DataOrigin.Y - visibleRange.Minimum.Y) / visibleRange.Height));
                    this.StartPoint = new ChartPoint((float)(windowSize.Width - axesMargin.Right), (float)(windowSize.Height * (DataOrigin.Y - visibleRange.Minimum.Y) / visibleRange.Height));
                    break;
            }
        }

        public override void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            if (this.isVisible)
            {
                drawingSession.DrawLine(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y, this.Color, (float)this.Thickness, this.StrokeStyle);
                this.ArrowHead?.DrawOnCanvas(drawingSession);

                if (this.Tick != null && this.TickIncrement > 0.0f)
                {

                    ChartPoint firstPoint = this.StartPoint.X < this.EndPoint.X ? this.StartPoint : this.EndPoint;
                    ChartPoint secondPoint = this.StartPoint.X > this.EndPoint.X ? this.StartPoint : this.EndPoint;
                    double label = DataOrigin;
                    if (this.OriginPoint <= firstPoint.X)
                    {
                        double distance = firstPoint.X - this.OriginPoint;
                        firstPoint.X += (float)(this.TickIncrement * this.DataRatio - (distance % (this.TickIncrement * this.DataRatio)));
                        label = DataOrigin + Math.Floor(distance / (this.TickIncrement * this.DataRatio)) * this.TickIncrement;
                        do
                        {
                            this.Tick.Position = firstPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            firstPoint.X += (float)(this.TickIncrement * this.DataRatio);
                            label += this.TickIncrement;
                        } while (firstPoint.X <= secondPoint.X);
                    }
                    else if (this.DataOrigin <= secondPoint.X)
                    {
                        ChartPoint tickPoint = new ChartPoint((float)this.OriginPoint, secondPoint.Y);
                        tickPoint.X += (float)(this.TickIncrement * this.DataRatio);
                        label = DataOrigin + this.TickIncrement;
                        while ((tickPoint.X <= secondPoint.X))
                        {
                            this.Tick.Position = tickPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            tickPoint.X += (float)(this.TickIncrement * this.DataRatio);
                            label += this.TickIncrement;
                        }
                        tickPoint = new ChartPoint((float)OriginPoint, secondPoint.Y);
                        tickPoint.X -= (float)(this.TickIncrement * this.DataRatio);
                        label = DataOrigin - this.TickIncrement;
                        while ((tickPoint.X >= firstPoint.X))
                        {
                            this.Tick.Position = tickPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            tickPoint.X -= (float)(this.TickIncrement * this.DataRatio);
                            label -= this.TickIncrement;
                        }
                    }
                    else
                    {
                        double distance = this.OriginPoint - firstPoint.X;
                        firstPoint.X += (float)(this.TickIncrement * this.DataRatio - (distance % (this.TickIncrement * this.DataRatio)));
                        label = DataOrigin - Math.Floor(distance / (this.TickIncrement * this.DataRatio)) * this.TickIncrement;
                        do
                        {
                            this.Tick.Position = firstPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            firstPoint.X += (float)(this.TickIncrement * this.DataRatio);
                            label += this.TickIncrement;
                        } while (firstPoint.X <= secondPoint.X);

                    }
                    //else
                    //{
                    //    ChartPoint firstPoint = this.StartPoint.Y < this.EndPoint.Y ? this.StartPoint : this.EndPoint;
                    //    ChartPoint secondPoint = this.StartPoint.Y > this.EndPoint.Y ? this.StartPoint : this.EndPoint;
                    //    double label = DataOrigin;
                    //    if (this.OriginPoint <= firstPoint.Y)
                    //    {
                    //        double distance = firstPoint.Y - this.OriginPoint;
                    //        firstPoint.Y += (float)(this.TickIncrement * this.DataRatio - (distance % (this.TickIncrement * this.DataRatio)));
                    //        label = DataOrigin - Math.Floor(distance / (this.TickIncrement * this.DataRatio)) * this.TickIncrement;
                    //        do
                    //        {
                    //            this.Tick.Position = firstPoint;
                    //            this.Tick.Label = $"{label:0.0}";
                    //            this.Tick.DrawOnCanvas(drawingSession);
                    //            firstPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                    //            label -= this.TickIncrement;
                    //        } while (firstPoint.Y <= secondPoint.Y);
                    //    }
                    //    else if (this.DataOrigin <= secondPoint.Y)
                    //    {
                    //        ChartPoint tickPoint = new ChartPoint(secondPoint.X, (float)this.OriginPoint);
                    //        tickPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                    //        label = DataOrigin - this.TickIncrement;
                    //        while ((tickPoint.Y <= secondPoint.Y))
                    //        {
                    //            this.Tick.Position = tickPoint;
                    //            this.Tick.Label = $"{label:0.0}";
                    //            this.Tick.DrawOnCanvas(drawingSession);
                    //            tickPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                    //            label -= this.TickIncrement;
                    //        }
                    //        tickPoint = new ChartPoint(secondPoint.X, (float)OriginPoint);
                    //        tickPoint.Y -= (float)(this.TickIncrement * this.DataRatio);
                    //        label = DataOrigin + this.TickIncrement;
                    //        while ((tickPoint.Y >= firstPoint.Y))
                    //        {
                    //            this.Tick.Position = tickPoint;
                    //            this.Tick.Label = $"{label:0.0}";
                    //            this.Tick.DrawOnCanvas(drawingSession);
                    //            tickPoint.Y -= (float)(this.TickIncrement * this.DataRatio);
                    //            label += this.TickIncrement;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        double distance = this.OriginPoint - firstPoint.Y;
                    //        firstPoint.Y += (float)(this.TickIncrement * this.DataRatio - (distance % (this.TickIncrement * this.DataRatio)));
                    //        label = DataOrigin + Math.Floor(distance / (this.TickIncrement * this.DataRatio)) * this.TickIncrement;
                    //        do
                    //        {
                    //            this.Tick.Position = firstPoint;
                    //            this.Tick.Label = $"{label:0.0}";
                    //            this.Tick.DrawOnCanvas(drawingSession);
                    //            firstPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                    //            label -= this.TickIncrement;
                    //        } while (firstPoint.Y <= secondPoint.Y);

                    //    }
                    //}
                }
            }
        }
    }
}
