using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    /// <summary>
    /// Application simulates printing requests made by 10 computers.
    /// </summary>
    class Program
    {
        //Necesery variables for application functionality.
        public static List<string> colors = new List<string>();
        public static Random r = new Random();
        public static bool all = false;

        static void Main(string[] args)
        {
            //Colors are being added from palette.txt file.
            Read();
            //Loop produces threads randomly with each iteration.
            while (!all)
            {
                //Elements of Printer object are assigned random values.
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
                //Threads are produced and name is added randomly.
                Thread t = new Thread(() => Service.Pc(p));
                t.Name = string.Format("PC_{0}", r.Next(1,11));
                //When thread is made, it is displayed on the console.
                Console.WriteLine("{0} has sent {1} format print request.", t.Name, p.Format);
                Console.WriteLine("color: {0}, orientation: {1}.", p.Color, p.Orientation);
                //Condition checks if semaphore in consequential methods has avalaible slot.
                if (Service.smp1.CurrentCount == 1 && Service.smp2.CurrentCount == 1 && !all)
                {
                    //If so, thread may start.
                    t.Start();
                }
                //Sleep added to pause threat generation process for short time.
                Thread.Sleep(100);
            }
            //Reset handle waits for last threads to complete before allowing final code to execute.
            Service.ewh.WaitOne();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("All 10 PC-s have printed at least once.");
            Console.ResetColor();

            Console.ReadLine();
        }
        /// <summary>
        /// Method reads colors from file, and adds them to the list.
        /// </summary>
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
