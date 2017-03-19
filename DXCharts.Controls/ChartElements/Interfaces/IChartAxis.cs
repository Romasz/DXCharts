﻿// ******************************************************************
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
    /// <summary>
    /// The interface of axis element
    /// </summary>
    public interface IChartAxis : IChartLineElement
    {
        /// <summary>
        /// Pixels per data
        /// </summary>
        double DataRatio { get; set; }

        /// <summary>
        /// Orgin of data
        /// </summary>
        double DataOrigin { get; set; }

        /// <summary>
        /// Orgin of axis
        /// </summary>
        float OriginPoint { get; set; }

    }
}