using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Numerics;

namespace _3D_Graphing
{
    /// <summary>
    /// Interaction logic for OutputWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {
        Vector3[][] rawKeyPoints = null;
        string prevFunction = "";
        public OutputWindow()
        {
            InitializeComponent();
        }

        public void Render(string function, float X1, float X2, float Y1, float Y2, float step, float angle) // Render the graph of the function.
        {
            Projector.rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);
            Grid.Children.Clear();
            Vector2[] axes = new Vector2[6] {
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
            if (rawKeyPoints == null || prevFunction != function)
            {
                rawKeyPoints = FunctionManager.KeyPoints(function, X1, X2, Y1, Y2, step); // Don't recalculate the keypoints if the function is the same!
            }
            prevFunction = function;
            foreach (Vector3[] rawPoints in rawKeyPoints) // This loop draws the function itself.
            {
                Vector2[] points = new Vector2[4]
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
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    });
                }
            }
        }
    }
}
