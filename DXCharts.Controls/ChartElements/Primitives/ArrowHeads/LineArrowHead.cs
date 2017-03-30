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
    using Classes;
    using Interfaces;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Geometry;
    using System.Numerics;
    using Windows.UI;

    public class LineArrowHead : ArrowHeadBase
    {
        /// <summary>
        /// Thickness of arrow's lines
        /// </summary>
        public double Thickness { get; set; }

        public LineArrowHead()
        {
            this.Angle = 0;
            this.Color = Colors.Black;
            this.Position = new ChartPoint(0, 0);
            this.Thickness = 1.0d;
            this.Size = new ElementSize(10, 10);
        }

        public override void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            var previousTransform = drawingSession.Transform;
            drawingSession.Transform = Matrix3x2.CreateRotation((float)this.Angle, new Vector2(this.Position.X, this.Position.Y));
            using (var pathBuilder = new CanvasPathBuilder(drawingSession))
            {
                pathBuilder.BeginFigure(this.Position.X, this.Position.Y);
                pathBuilder.AddLine(this.Position.X - this.Size.Width, this.Position.Y - this.Size.Height / 2);
                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                pathBuilder.BeginFigure(this.Position.X, this.Position.Y);
                pathBuilder.AddLine(this.Position.X - this.Size.Width, this.Position.Y + this.Size.Height / 2);
                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                drawingSession.DrawGeometry(CanvasGeometry.CreatePath(pathBuilder), this.Color, (float)this.Thickness);
            }
            drawingSession.Transform = previousTransform;
        }
    }
}
