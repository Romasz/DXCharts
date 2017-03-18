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
    using Microsoft.Graphics.Canvas;

    /// <summary>
    /// The basic element that can be drawn on CanvasControl
    /// </summary>
    public interface IChartElement
    {
        /// <summary>
        /// Method responsible for drawing the element
        /// </summary>
        /// <param name="drawingSession"><see cref="CanvasDrawingSession"/> on which element will be drawn</param>
        void DrawOnCanvas(CanvasDrawingSession drawingSession);
    }
}
