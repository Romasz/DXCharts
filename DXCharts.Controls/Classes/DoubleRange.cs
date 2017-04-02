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
    /// Range of the data given with double
    /// </summary>
    public struct DoubleRange
    {
        /// <summary>
        /// Minimum value
        /// </summary>
        public readonly double Minimum;

        /// <summary>
        /// Maximum value
        /// </summary>
        public readonly double Maximum;

        public readonly double Width;

        public bool InRange(double data) => data >= this.Minimum && data <= this.Maximum;

        public DoubleRange(double value1, double value2)
        {
            if (value1 < value2)
            {
                this.Minimum = value1;
                this.Maximum = value2;
            }
            else
            {
                this.Minimum = value2;
                this.Maximum = value1;
            }
            this.Width = Math.Abs(this.Maximum - this.Minimum);
        }
    }
}
