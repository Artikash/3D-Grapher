﻿using System;
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

        private void AngleChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AngleDisplay.Text = (int)(180 * Angle.Value / Math.PI) + " Degrees";
        }

        private void Draw(object sender, RoutedEventArgs e)
        {
            if (Convert.ToSingle(MaxX.Text) < Convert.ToSingle(MinX.Text) || Convert.ToSingle(MaxY.Text) < Convert.ToSingle(MinY.Text))
            {
                MessageBox.Show("Invalid Bounds", "Invalid Bounds", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                FunctionManager.KeyPoints(Equation.Text, Convert.ToSingle(MinX.Text), Convert.ToSingle(MaxX.Text), Convert.ToSingle(MinY.Text), Convert.ToSingle(MaxY.Text),100 * Convert.ToSingle(Step.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Equation", "Invalid Equation", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            DrawButtonClicked(this, new GraphingEventArgs(
           (float)Angle.Value, Equation.Text, Convert.ToSingle(MinX.Text), Convert.ToSingle(MaxX.Text), Convert.ToSingle(MinY.Text), Convert.ToSingle(MaxY.Text), Convert.ToSingle(Step.Text)));
        }
    }
}
