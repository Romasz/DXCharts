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
    using Microsoft.Graphics.Canvas.Geometry;
    using System;

    public class StandardLine : IChartLineElement
    {
        public Color Color { get; set; }

        public double Angle { get; set; }

        public ChartPoint StartPoint { get; set; }

        public ChartPoint EndPoint { get; set; }

        public double Thickness { get; set; }

        public CanvasStrokeStyle StrokeStyle { get; set; }

        public StandardLine()
        {
            this.Angle = 0.0d;
            this.Color = Colors.Gray;
        }

        public void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            drawingSession.DrawLine(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y, this.Color,(float)this.Thickness, this.StrokeStyle);
        }
    }
}
