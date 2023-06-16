using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp4
{
    internal class Settings
    {
        public Brush background1;

        public Brush Getbackground()
        {
            return background1;
        }

        public void Setbackground(Brush value)
        {
            background1 = value;
        }
    }
}
