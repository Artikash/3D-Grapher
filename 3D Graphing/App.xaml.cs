using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3D_Graphing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        OutputWindow output;

        public void AppStartup(Object sender, StartupEventArgs e)
        {
            InputWindow input = new InputWindow();
            input.Show();
            input.Left = 0;
            input.DrawButtonClicked += new EventHandler<GraphingEventArgs>(Draw);
            output = new OutputWindow();
            output.Left = 600;
            output.ReRenderRequested += new EventHandler<EventArgs>(ReRender);
            output.Show();
        }

        private Vector3[][] keyPoints = null;

        private void Draw(object sender, GraphingEventArgs e)
        {
            keyPoints = FunctionManager.KeyPoints(e.function, e.minX, e.maxX, e.minY, e.maxY, e.step);
            Render();
        }

        public void Render() // Render the graph of the function.
        {
            var Grid = output.Grid;
            Grid.Children.RemoveRange(6,Int32.MaxValue);
            Vector3[] axes = new Vector3[6] { // This code draws the axes.
                Projector.Project(new Vector3(0, 0, 1000)),
                Projector.Project(new Vector3(0, 0, -1000)),
                Projector.Project(new Vector3(0, 1000, 0)),
                Projector.Project(new Vector3(0, -1000, 0)),
                Projector.Project(new Vector3(1000, 0, 0)),
                Projector.Project(new Vector3(-1000, 0, 0)) };
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 100 * axes[0].X,
                X2 = 250 + 100 * axes[1].X,
                Y1 = 250 + 100 * axes[0].Y,
                Y2 = 250 + 100 * axes[1].Y,
                Stroke = Brushes.Blue,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            });
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 100 * axes[2].X,
                X2 = 250 + 100 * axes[3].X,
                Y1 = 250 + 100 * axes[2].Y,
                Y2 = 250 + 100 * axes[3].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            });
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 100 * axes[4].X,
                X2 = 250 + 100 * axes[5].X,
                Y1 = 250 + 100 * axes[4].Y,
                Y2 = 250 + 100 * axes[5].Y,
                Stroke = Brushes.Green,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            }); // Everything before here was just to draw the axes.

            foreach (Vector3[] keyPointGroup in keyPoints) // This loop draws the function itself.
            {
                Vector3[] points = new Vector3[4]
                {
                    Projector.Project(keyPointGroup[0]),
                    Projector.Project(keyPointGroup[1]),
                    Projector.Project(keyPointGroup[2]),
                    Projector.Project(keyPointGroup[3]),
                };
                if (Math.Abs(points[0].Y) < 1000 && Math.Abs(points[1].Y) < 1000 && Math.Abs(points[2].Y) < 1000 && Math.Abs(points[3].Y) < 1000)
                {
                    Grid.Children.Add(new Line()
                    {
                        X1 = 250 + 100 * (points[0].X),
                        X2 = 250 + 100 * (points[1].X),
                        Y1 = 250 + 100 * (points[0].Y),
                        Y2 = 250 + 100 * (points[1].Y),
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                        Opacity = 3 * Math.Exp(points[0].Z),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    });
                    Grid.Children.Add(new Line()
                    {
                        X1 = 250 + 100 * (points[2].X),
                        X2 = 250 + 100 * (points[3].X),
                        Y1 = 250 + 100 * (points[2].Y),
                        Y2 = 250 + 100 * (points[3].Y),
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                        Opacity = 3 * Math.Exp(points[2].Z),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    });
                }
            }
        }
        public void ReRender(object sender, EventArgs empty)
        {
            if (keyPoints == null) return;
            Render();
        }
    }
}
