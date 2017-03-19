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
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Text;
    using System.Numerics;

    public class StandardTick : TickBase
    {
        public StandardTick()
        {
            this.Size = new ElementSize(10, 10); // for test
            this.TextFormat = new CanvasTextFormat() { FontSize = 12 }; // for test
        }

        public override void DrawOnCanvas(CanvasDrawingSession drawingSession)
        {
            var previousTransform = drawingSession.Transform;
            drawingSession.Transform = Matrix3x2.CreateRotation((float)this.Angle, new Vector2(this.Position.X, this.Position.Y));
            drawingSession.DrawLine(this.Position.X, this.Position.Y - this.Size.Height / 2, this.Position.X, this.Position.Y + this.Size.Height / 2, this.Color, (float)this.Thickness);
            drawingSession.Transform = previousTransform;
            drawingSession.DrawText(this.Label, this.Position.X + 5, this.Position.Y + 3, this.Color, this.TextFormat);
        }
    }
}
