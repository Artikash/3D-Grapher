using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Numerics;

namespace _3D_Graphing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public event EventHandler<GraphingEventArgs> DrawButtonClicked;

        public InputWindow()
        {
            InitializeComponent();
        }

        private void Draw(object sender, RoutedEventArgs e)
        {
            DrawButtonClicked(this, new GraphingEventArgs
                (Equation.Text,
                Convert.ToSingle(MinX.Text),
                Convert.ToSingle(MaxX.Text),
                Convert.ToSingle(MinY.Text), 
                Convert.ToSingle(MaxY.Text),
                Convert.ToSingle(Step.Text)));
        }
    }
}
