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
    using Windows.UI;
    using Windows.Foundation;

    public abstract class ArrowheadBase : IChartElement, IInvertible
    {
        protected double AngleInRadians { get; private set; }
        /// <summary>
        /// Orientation - set in degrees, get in radians
        /// </summary>
        protected double Angle
        {
            get { return AngleInRadians * Math.PI / 180; }
            set { AngleInRadians = value / 180 * Math.PI; }
        }

        /// <summary>
        /// Element's color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Position at which element will be drawn
        /// </summary>
        public ChartPoint Position;

        /// <summary>
        /// Size of the element - rectanle in which it will be drawn
        /// </summary>
        public Size Size { get; set; }


        public bool IsInverted { get; set; }

        /// <summary>
        /// Value indicating if element should be inverted
        /// </summary>
        public abstract void DrawOnCanvas(CanvasDrawingSession drawingSession);
    }
}
