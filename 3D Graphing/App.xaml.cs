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
            output.Show();
        }

        private void Draw(object sender, GraphingEventArgs e)
        {
            Render(e.function,e.minX,e.maxX,e.minY,e.maxY,e.step,e.angle);
        }

        public void Render(string function, float X1, float X2, float Y1, float Y2, float step, float angle) // Render the graph of the function.
        {
            var Grid = output.Grid;
            Grid.Children.RemoveRange(6,Int32.MaxValue);
            Projector.rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);
            Vector3[] axes = new Vector3[6] { // This code draws the axes.
                Projector.Project(new Vector3(0, 0, 10)),
                Projector.Project(new Vector3(0, 0, -10)),
                Projector.Project(new Vector3(0, 10, 0)),
                Projector.Project(new Vector3(0, -10, 0)),
                Projector.Project(new Vector3(10, 0, 0)),
                Projector.Project(new Vector3(-10, 0, 0)) };
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 200 * axes[0].X,
                X2 = 250 + 200 * axes[1].X,
                Y1 = 250 + 200 * axes[0].Y,
                Y2 = 250 + 200 * axes[1].Y,
                Stroke = Brushes.Blue,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            });
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 300 * axes[2].X,
                X2 = 250 + 300 * axes[3].X,
                Y1 = 250 + 300 * axes[2].Y,
                Y2 = 250 + 300 * axes[3].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            });
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 300 * axes[4].X,
                X2 = 250 + 300 * axes[5].X,
                Y1 = 250 + 300 * axes[4].Y,
                Y2 = 250 + 300 * axes[5].Y,
                Stroke = Brushes.Green,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            }); // Everything before here was just to draw the axes.

            Vector3[][] rawKeyPoints = FunctionManager.KeyPoints(function, X1, X2, Y1, Y2, step); 

            foreach (Vector3[] rawPoints in rawKeyPoints) // This loop draws the function itself.
            {
                Vector3[] points = new Vector3[4]
                {
                    Projector.Project(rawPoints[0]),
                    Projector.Project(rawPoints[1]),
                    Projector.Project(rawPoints[2]),
                    Projector.Project(rawPoints[3]),
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
    }
}
