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
        internal EventHandler<EventArgs> MouseDragged;
        Point basePosition;
        bool mouseDown;
        
        public OutputWindow()
        {
            InitializeComponent();
        }

        private void Mouse1Down(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            basePosition = e.GetPosition(this);
            mouseDown = true;
        }

        private void Mouse1Up(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseDown = false;
        }

        private new void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mouseDown)
            {
                double changeInX = e.GetPosition(this).X - basePosition.X;
                double changeInY = e.GetPosition(this).Y - basePosition.Y;
                basePosition = e.GetPosition(this);
                Projector.SetAngle(Projector.GetTheta() + (float)changeInX / 250.0f, Projector.GetPhi() - (float)changeInY / 250.0f);
                MouseDragged(this, new EventArgs());
            }
        }
    }
}
