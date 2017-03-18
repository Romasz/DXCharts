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
    using Interfaces;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Text;
    using Windows.UI;

    public abstract class TickBase : IChartPointElement
    {
        /// <summary>
        /// Orientation of arrowhead in radians
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Element's color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Position at which element will be drawn
        /// </summary>
        public ChartPoint Position { get; set; }

        /// <summary>
        /// Size of the element
        /// </summary>
        public ElementSize Size { get; set; }

        /// <summary>
        /// Thickness of arrow's lines
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// Label to be shown
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Format of the text
        /// </summary>
        public CanvasTextFormat TextFormat { get; set; }

        public abstract void DrawOnCanvas(CanvasDrawingSession drawingSession);
    }
}
