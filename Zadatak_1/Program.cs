using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        public static List<string> colors = new List<string>();
        public static Random r = new Random();
        public static bool all = false;

        static void Main(string[] args)
        {
            Read();

            while (!all)
            {
                string[] format = new string[2];
                format[0] = "A3";
                format[1] = "A4";
                string[] orientation = new string[2];
                orientation[0] = "Portrait";
                orientation[1] = "Landscape";
                Printer p = new Printer();
                p.Format = format[r.Next(0, 2)];
                p.Color = Program.colors[r.Next(0, Program.colors.Count())];
                p.Orientation = orientation[r.Next(0, 2)];

                Thread t = new Thread(() => Service.Pc(p));
                t.Name = string.Format("PC_{0}", r.Next(1,11));

                Console.WriteLine("{0} has sent {1} format print request.", t.Name, p.Format);
                Console.WriteLine("color: {0}, orientation: {1}.", p.Color, p.Orientation);

                if (Service.smp1.CurrentCount == 1 && Service.smp2.CurrentCount == 1 && !all)
                {
                    t.Start();
                }
                Thread.Sleep(100);
            }

            Service.ewh.WaitOne();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("All 10 PC have printed at least once.");
            Console.ResetColor();

            Console.ReadLine();
        }

        static void Read()
        {
            StreamReader sr = new StreamReader("..//../Files/Palette.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                colors.Add(line);
            }
            sr.Close();
        }
    }
}
