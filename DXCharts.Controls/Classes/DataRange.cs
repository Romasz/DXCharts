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

namespace DXCharts.Controls.Classes
{
    using System;
    using Windows.Foundation;

    /// <summary>
    /// Range of the data
    /// </summary>
    public class DataRange
    {
        /// <summary>
        /// Minimum X and Y
        /// </summary>
        public readonly Point Minimum;

        /// <summary>
        /// Maximum X and Y
        /// </summary>
        public readonly Point Maximum;

        public readonly double Width;
        public readonly double Height;

        public bool InVerticalRange(double data) => data >= Minimum.Y && data <= Maximum.Y;

        public bool InHorizontalRange(double data) => data >= Minimum.X && data <= Maximum.X;

        public bool InRange(Point data) => InHorizontalRange(data.X) && InVerticalRange(data.Y);

        public DataRange(double x1, double y1, double x2, double y2)
        {
            if (double.IsInfinity(x1) || double.IsNaN(x1) || double.IsInfinity(y1) || double.IsNaN(y1) ||
                double.IsInfinity(x2) || double.IsNaN(x2) || double.IsInfinity(y2) || double.IsNaN(y2))
            {
                throw new ArgumentException("Bad data range value.");
            }

            this.Minimum = new Point(Math.Min(x1, x2), Math.Min(y1, y2));
            this.Maximum = new Point(Math.Max(x1, x2), Math.Max(y1, y2));
            this.Width = Math.Abs(this.Maximum.X - this.Minimum.X);
            this.Height = Math.Abs(this.Maximum.Y - this.Minimum.Y);
        }

        public DataRange(Point firstPoint, Point secondPoint) : this(firstPoint.X, firstPoint.Y, secondPoint.X, secondPoint.Y) { }
    }
}
