using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewGameChakers
{
    public static class Extensions
    {
        public static Ellipse GetGreenEllipse(this Grid grid)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                if (grid.Children[i] is Ellipse)
                {
                    var ellipse = (Ellipse)grid.Children[i];
                    if (ellipse.Fill == Brushes.Green)
                    {
                        return ellipse;
                    }
                }
            }
            return null;
        }
    }
}
