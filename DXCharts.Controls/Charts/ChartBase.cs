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
    using System.Collections.ObjectModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public abstract class ChartBase : Control
    {
        protected CanvasControl rootCanvas = null;

        public DataRange VisibleRange
        {
            get { return (DataRange)GetValue(VisibleRangeProperty); }
            set { SetValue(VisibleRangeProperty, value); }
        }

        public static readonly DependencyProperty VisibleRangeProperty =
            DependencyProperty.Register(nameof(VisibleRange), typeof(DataRange), typeof(ChartBase), new PropertyMetadata(null, OnPropertyChangedStatic));

        public IChartDataPresenter DataPresenter
        {
            get { return (IChartDataPresenter)GetValue(DataPresenterProperty); }
            set { SetValue(DataPresenterProperty, value); }
        }

        public static readonly DependencyProperty DataPresenterProperty =
            DependencyProperty.Register(nameof(DataPresenter), typeof(IChartDataPresenter), typeof(ChartBase), new PropertyMetadata(null,
                new PropertyChangedCallback((d, e) =>
                {
                    (d as ChartBase)?.PrepareDataPresenter();
                    OnPropertyChangedStatic(d, e);
                })));


        public Collection<IChartAxis> AxesCollection;

        public abstract void CreateAxes();
        public abstract void PrepareDataPresenter();
        public abstract void UpdateAxes(ElementSize newSize);

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
            rootCanvas = GetTemplateChild("RootCanvas") as CanvasControl;
            rootCanvas.CreateResources += RootCanvas_CreateResources;
            rootCanvas.Draw += RootCanvas_Draw;
            CreateAxes();
        }

        protected void OnPropertyChanged(DependencyObject d, DependencyProperty prop)
        {
            rootCanvas?.Invalidate();
        }

        private void RootCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            // not used for now
        }

        private void RootCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            // first draw points
            if (DataPresenter != null)
            {
                DataPresenter.PresentData(args.DrawingSession);
            }

            // then axes
            UpdateAxes(new ElementSize((float)sender.ActualWidth, (float)sender.ActualHeight));
            foreach (var axis in AxesCollection)
            {
                axis.DrawOnCanvas(args.DrawingSession);
            }
        }
    }
}
