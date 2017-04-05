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


namespace DXCharts.Controls.Charts
{
    using ChartElements.Interfaces;
    using Classes;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public abstract class ChartBase : Control
    {
        protected CanvasControl rootCanvas = null;

        public PointRange VisibleRange
        {
            get { return (PointRange)GetValue(VisibleRangeProperty); }
            set { SetValue(VisibleRangeProperty, value); }
        }

        public static readonly DependencyProperty VisibleRangeProperty =
            DependencyProperty.Register(nameof(VisibleRange), typeof(PointRange), typeof(ChartBase), new PropertyMetadata(null, OnPropertyChangedStatic));

        public IEnumerable<IDataPresentible> DataSources
        {
            get { return (IEnumerable<IDataPresentible>)GetValue(DataSourcesProperty); }
            set { SetValue(DataSourcesProperty, value); }
        }

        public static readonly DependencyProperty DataSourcesProperty =
            DependencyProperty.Register(nameof(DataSources), typeof(IEnumerable<IDataPresentible>), typeof(ChartBase), new PropertyMetadata(null,
                new PropertyChangedCallback((d, e) =>
                {
                    OnPropertyChangedStatic(d, e);
                })));


        public Collection<IChartAxis> AxesCollection;

        public abstract void CreateAxes();
        public abstract void UpdateAxes(Size windowSize);

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartBase)?.OnPropertyChanged(d, e.Property);
        }

        public ChartBase()
        {
            this.AxesCollection = new Collection<IChartAxis>();
            this.DefaultStyleKey = typeof(ChartBase);
            this.Unloaded += ChartBase_Unloaded;
        }

        private void ChartBase_Unloaded(object sender, RoutedEventArgs e)
        {
            rootCanvas.Draw -= RootCanvas_Draw;
            rootCanvas.CreateResources -= RootCanvas_CreateResources;
            rootCanvas.RemoveFromVisualTree();
            rootCanvas = null;
        }

        protected override void OnApplyTemplate()
        {
            CreateAxes();
            rootCanvas = GetTemplateChild("RootCanvas") as CanvasControl;
            rootCanvas.CreateResources += RootCanvas_CreateResources;
            rootCanvas.Draw += RootCanvas_Draw;
        }

        protected void OnPropertyChanged(DependencyObject d, DependencyProperty prop)
        {
            rootCanvas?.Invalidate();
        }

        public void RedrawChart()
        {
            rootCanvas?.Invalidate();
        }

        private void RootCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            // not used for now
        }

        private void RootCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            Debug.WriteLine($"Redrawing the canvas");
            // first update axes - important to have updated DataRatio and methods responsible for geting ChartPoints
            this.UpdateAxes(new Size(sender.ActualWidth, sender.ActualHeight));

            foreach (var axis in this.AxesCollection)
            {
                axis.DrawOnCanvas(args.DrawingSession);
            }

            foreach (var dataSource in this.DataSources)
            {
                dataSource.PresentData(args.DrawingSession);
            }
        }
    }
}
