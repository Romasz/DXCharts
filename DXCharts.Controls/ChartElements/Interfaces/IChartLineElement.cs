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

namespace DXCharts.Controls.ChartElements.Interfaces
{
    using Controls.Classes;
    using Microsoft.Graphics.Canvas.Geometry;
    using Windows.UI;

    /// <summary>
    /// The line element that can be drawn on CanvasControl
    /// </summary>
    public interface IChartLineElement : IChartElement
    {
        /// <summary>
        /// Start point of line/>
        /// </summary>
        ChartPoint StartPoint { get; set; }

        /// <summary>
        /// End point of line/>
        /// </summary>
        ChartPoint EndPoint { get; set; }

        /// <summary>
        /// Line's thickness
        /// </summary>
        double Thickness { get; set; }

        /// <summary>
        /// Line's color
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Line's style
        /// </summary>
        CanvasStrokeStyle StrokeStyle { get; set; }
    }
}
