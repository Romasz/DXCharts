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
    using Windows.Foundation;
    using Windows.UI.Xaml;

    /// <summary>
    /// The interface of axis element
    /// </summary>
    public interface IChartAxis : IChartLineElement
    {
        /// <summary>
        /// If set to true, axis <see cref="DataRatio"/> will be adjusted so that all the data are visible
        /// </summary>
        bool AdjustDataRatio { get; set; }

        /// <summary>
        /// Place where second axis is placed
        /// </summary>
        double DataOrigin { get; set; }

        /// <summary>
        /// Method preparing the axis
        /// </summary>
        void PrepareAxis();

        /// <summary>
        /// Method returning chart coordinate basing on data
        /// </summary>
        /// <param name="coordinate">data coordinate</param>
        /// <returns>chart coordinate</returns>
        float GetChartCoordinate(double coordinate);

        /// <summary>
        /// Update axis basing on current window size and visible range
        /// <param name="windowSize">current window size</param>
        /// <param name="axesMargin">margin of axes</param>
        /// <param name="visibleRange">visible data range</param>
        /// <param name="dataOrigin">origin of data - where axes cross and ticks start</param>
        void Update(Size windowSize, Thickness axesMargin, DataRange visibleRange, Point dataOrigin);
    }
}
