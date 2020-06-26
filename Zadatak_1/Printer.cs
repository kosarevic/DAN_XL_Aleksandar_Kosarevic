using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Printer
    {
        public string Format { get; set; }
        public string Color { get; set; }
        public string Orientation { get; set; }

        public Printer()
        {
        }

        public Printer(string format, string color, string orientation)
        {
            Format = format;
            Color = color;
            Orientation = orientation;
        }
    }
}
