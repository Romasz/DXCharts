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

    public class StandardAxis : IChartAxis
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
        public IChartPointElement ArrowHead { get; set; }

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
        public double DataRatio { get; set; }

        /// <summary>
        /// Orgin of data
        /// </summary>
        public double DataOrigin { get; set; }

        /// <summary>
        /// Orgin of axis
        /// </summary>
        public float OriginPoint { get; set; }

        public StandardAxis()
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
        }

        public void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            drawingSession.DrawLine(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y, this.Color, (float)this.Thickness, this.StrokeStyle);

            bool isHorizontal = Math.Abs(this.EndPoint.X - this.StartPoint.X) > Math.Abs(this.EndPoint.Y - this.StartPoint.Y);
            if (this.ArrowHead != null)
            {
                this.ArrowHead.Position = this.EndPoint;
                if (isHorizontal)
                {
                    this.ArrowHead.Angle = this.EndPoint.X > this.StartPoint.X ? 0.0f : 3.1415926535897931f;
                }
                else
                {
                    this.ArrowHead.Angle = this.EndPoint.Y > this.StartPoint.Y ? 3.1415926535897931f / 2 : 3.1415926535897931f * 1.5f;
                }
                this.ArrowHead.DrawOnCanvas(drawingSession);
            }

            if (this.Tick != null && this.TickIncrement > 0.0f)
            {
                if (isHorizontal)
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
                }
                else
                {
                    ChartPoint firstPoint = this.StartPoint.Y < this.EndPoint.Y ? this.StartPoint : this.EndPoint;
                    ChartPoint secondPoint = this.StartPoint.Y > this.EndPoint.Y ? this.StartPoint : this.EndPoint;
                    double label = DataOrigin;
                    if (this.OriginPoint <= firstPoint.Y)
                    {
                        double distance = firstPoint.Y - this.OriginPoint;
                        firstPoint.Y += (float)(this.TickIncrement * this.DataRatio - (distance % (this.TickIncrement * this.DataRatio)));
                        label = DataOrigin - Math.Floor(distance / (this.TickIncrement * this.DataRatio)) * this.TickIncrement;
                        do
                        {
                            this.Tick.Position = firstPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            firstPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                            label -= this.TickIncrement;
                        } while (firstPoint.Y <= secondPoint.Y);
                    }
                    else if (this.DataOrigin <= secondPoint.Y)
                    {
                        ChartPoint tickPoint = new ChartPoint(secondPoint.X, (float)this.OriginPoint);
                        tickPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                        label = DataOrigin - this.TickIncrement;
                        while ((tickPoint.Y <= secondPoint.Y))
                        {
                            this.Tick.Position = tickPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            tickPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                            label -= this.TickIncrement;
                        }
                        tickPoint = new ChartPoint(secondPoint.X, (float)OriginPoint);
                        tickPoint.Y -= (float)(this.TickIncrement * this.DataRatio);
                        label = DataOrigin + this.TickIncrement;
                        while ((tickPoint.Y >= firstPoint.Y))
                        {
                            this.Tick.Position = tickPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            tickPoint.Y -= (float)(this.TickIncrement * this.DataRatio);
                            label += this.TickIncrement;
                        }
                    }
                    else
                    {
                        double distance = this.OriginPoint - firstPoint.Y;
                        firstPoint.Y += (float)(this.TickIncrement * this.DataRatio - (distance % (this.TickIncrement * this.DataRatio)));
                        label = DataOrigin + Math.Floor(distance / (this.TickIncrement * this.DataRatio)) * this.TickIncrement;
                        do
                        {
                            this.Tick.Position = firstPoint;
                            this.Tick.Label = $"{label:0.0}";
                            this.Tick.DrawOnCanvas(drawingSession);
                            firstPoint.Y += (float)(this.TickIncrement * this.DataRatio);
                            label -= this.TickIncrement;
                        } while (firstPoint.Y <= secondPoint.Y);

                    }
                }
            }
        }
    }
}
