using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            System.Diagnostics.Trace.WriteLine(e);
            output.Render(e.function,e.minX,e.maxX,e.minY,e.maxY,e.step,e.angle);
        }
    }
}
