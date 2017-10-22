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
        public OutputWindow()
        {
            InitializeComponent();
        }

        public void Render(string function, float X1, float X2, float Y1, float Y2, float step, float angle)
        {
            Projector.rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);
            Grid.Children.Clear();
            Vector2[] axes = Projector.Project(new Vector3[6] {
                new Vector3(0, 0, 10),
                new Vector3(0, 0, -10),
                new Vector3(0, 10, 0),
                new Vector3(0, -10, 0),
                new Vector3(10, 0, 0),
                new Vector3(-10, 0, 0) }
            );
            Grid.Children.Add(new Line()
            {
                X1 = 250 + 200 * axes[0].X,
                X2 = 250 + 200 * axes[1].X,
                Y1 = 250 + 200 * axes[0].Y,
                Y2 = 250 + 200 * axes[1].Y,
                Stroke = Brushes.Gray,
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
                Stroke = Brushes.Gray,
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
                Stroke = Brushes.Gray,
                StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            });
            double Range = Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));
            foreach (Vector2[] points in FunctionManager.KeyPoints(function, X1, X2, Y1, Y2, step))
            {
                if (Math.Abs(points[0].Y) < 1000 && Math.Abs(points[1].Y) < 1000 && Math.Abs(points[2].Y) < 1000 && Math.Abs(points[3].Y) < 1000)
                {
                    Grid.Children.Add(new Line()
                    {
                        X1 = 250 + 400 * (points[0].X) / Range,
                        X2 = 250 + 400 * (points[1].X) / Range,
                        Y1 = 250 + 400 * (points[0].Y) / Range,
                        Y2 = 250 + 400 * (points[1].Y) / Range,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    });
                    Grid.Children.Add(new Line()
                    {
                        X1 = 250 + 400 * (points[2].X) / Range,
                        X2 = 250 + 400 * (points[3].X) / Range,
                        Y1 = 250 + 400 * (points[2].Y) / Range,
                        Y2 = 250 + 400 * (points[3].Y) / Range,
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
