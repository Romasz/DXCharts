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
        public DataPoint Minimum { get; private set; }

        /// <summary>
        /// Maximum X and Y
        /// </summary>
        public DataPoint Maximum { get; private set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public bool InVerticalRange(double data)
        {
            return data >= Minimum.Y && data <= Maximum.Y;
        }
        
        public bool InHorizontalRange(double data)
        {
            return data >= Minimum.X && data <= Maximum.X;
        }

        public bool InRange(Point data)
        {
            return data.X >= Minimum.X && data.X <= Maximum.X && data.Y >= Minimum.Y && data.Y <= Maximum.Y;
        }

        public bool InRange(DataPoint data)
        {
            return data.X >= Minimum.X && data.X <= Maximum.X && data.Y >= Minimum.Y && data.Y <= Maximum.Y;
        }

        public DataRange(double x1, double y1, double x2, double y2)
        {
            if (double.IsInfinity(x1) || double.IsNaN(x1) || double.IsInfinity(y1) || double.IsNaN(y1) ||
                double.IsInfinity(x2) || double.IsNaN(x2) || double.IsInfinity(y2) || double.IsNaN(y2))
            {
                throw new ArgumentException("Bad data range value.");
            }

            this.Minimum = new DataPoint(Math.Min(x1, x2), Math.Min(y1, y2));
            this.Maximum = new DataPoint(Math.Max(x1, x2), Math.Max(y1, y2));
            this.Width = Math.Abs(this.Maximum.X - this.Minimum.X);
            this.Height = Math.Abs(this.Maximum.Y - this.Minimum.Y);
        }

        public DataRange(Windows.Foundation.Point firstPoint, Windows.Foundation.Point secondPoint) : this(firstPoint.X, firstPoint.Y, secondPoint.X, secondPoint.Y) { }
    }
}
