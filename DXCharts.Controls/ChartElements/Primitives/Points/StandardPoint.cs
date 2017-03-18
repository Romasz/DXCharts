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

    public class StandardPoint : IChartPointElement
    {
        public Color Color { get; set; }

        public double Angle { get; set; }

        public ChartPoint Position { get; set; }

        public ElementSize Size { get; set; }

        public StandardPoint()
        {
            this.Size = new ElementSize(1, 1);
            this.Angle = 0.0d;
            this.Color = Colors.Gray;
        }

        public void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            drawingSession.DrawCircle(this.Position.X, this.Position.Y, this.Size.Width, this.Color);
        }
    }
}
