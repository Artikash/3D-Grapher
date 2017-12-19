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
            output.MouseDragged += new EventHandler<EventArgs>(ReRender);
            output.Show();
        }

        private void Draw(object sender, GraphingEventArgs e)
        {
            Render(e.function,e.minX,e.maxX,e.minY,e.maxY,e.step);
        }

        private string FUNCTION;
        private float X1, X2, Y1, Y2, STEP;
        public void Render(string function, float x1, float x2, float y1, float y2, float step) // Render the graph of the function.
        {
            FUNCTION = function;
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
            STEP = step;
            var Grid = output.Grid;
            Grid.Children.RemoveRange(6,Int32.MaxValue);
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

            Vector3[][] rawKeyPoints = FunctionManager.KeyPoints(function, x1, x2, y1, y2, step); 

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
        public void ReRender(object sender, EventArgs empty)
        {
            Render(FUNCTION, X1, X2, Y1, Y2, STEP);
        }
    }
}
