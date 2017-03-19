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
    using Classes;
    using Microsoft.Graphics.Canvas;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Windows.Foundation;

    /// <summary>
    /// The interface of for data presenter
    /// </summary>
    public interface IChartDataPresenter : INotifyCollectionChanged
    {
        /// <summary>
        /// Data to be presented
        /// </summary>
        IEnumerable<Point> Data { get; set; }

        /// <summary>
        /// Method used to check if point is in range;
        /// </summary>
        /// <returns></returns>
        Predicate<Point> IsPointInRange { get; set; }

        /// <summary>
        /// Method used to convert data to chart points
        /// </summary>
        Func<Point, ChartPoint> Convert { get; set; }

        /// <summary>
        /// Method responsible for drawing data
        /// </summary>
        /// <param name="drawingSession"><see cref="CanvasDrawingSession"/> on which element will be drawn</param>
        void PresentData(CanvasDrawingSession drawingSession);
    }
}
