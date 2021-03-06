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

namespace DXCharts.Controls.ChartElements.Primitives
{
    using Interfaces;
    using System.Collections.Generic;
    using Microsoft.Graphics.Canvas;
    using System;
    using Windows.UI.Xaml;

    public class DataSource<T> : DependencyObject, IDataSource<T>
    {
        public IEnumerable<T> Data { get; set; }

        public IChartDataPresenter<T> DataPresenter { get; set; }

        public bool IsPresented
        {
            get { return (bool)GetValue(IsPresentedProperty); }
            set { SetValue(IsPresentedProperty, value); }
        }

        public static readonly DependencyProperty IsPresentedProperty =
            DependencyProperty.Register(nameof(IsPresented), typeof(bool), typeof(DataSource<T>), new PropertyMetadata(true));
       
        public void PresentData(CanvasDrawingSession drawingSession)
        {
            if (this.DataPresenter != null && this.Data != null)
            {
                this.DataPresenter.PresentData(drawingSession, this.Data);
            }
        }
    }
}
