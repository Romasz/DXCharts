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
    /// <summary>
    /// The struct defining size of the element
    /// </summary>
    public struct ElementSize
    {
        /// <summary>
        /// Element's width
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Element's height
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Create new size width x height
        /// </summary>
        /// <param name="width">value set as width</param>
        /// <param name="height">value set as height</param>
        public ElementSize(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
