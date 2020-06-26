using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Service
    {
        public static SemaphoreSlim smp1 = new SemaphoreSlim(1);
        public static SemaphoreSlim smp2 = new SemaphoreSlim(1);
        public static Random r = new Random();

        public static void Pc()
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
            Console.WriteLine("{0} has sent {1} format print request.", Thread.CurrentThread.Name, p.Format);
            Console.WriteLine("color: {0}, orientation: {1}.", p.Color, p.Orientation);
            if (p.Format == "A3")
            {
                PrintA3(p);
            }
            else
            {
                PrintA4(p);
            }
        }

        public static void PrintA3(Printer printer)
        {
            smp1.Wait();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Printing A3, for {0}...", Thread.CurrentThread.Name);
            Console.ResetColor();
            Thread.Sleep(1000);
            PrintingDoneA3(Thread.CurrentThread.Name);
            smp1.Release();
        }

        public static void PrintA4(Printer printer)
        {
            smp2.Wait();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Printing A4, for {0}...", Thread.CurrentThread.Name);
            Console.ResetColor();
            Thread.Sleep(1000);
            PrintingDoneA4(Thread.CurrentThread.Name);
            smp2.Release();
        }

        public delegate void Notification();

        public static event Notification OnNotification;

        public static void PrintingDoneA3(string format)
        {
            OnNotification = () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Printing A3 for {0} completed.", format);
                Console.ResetColor();
            };
            OnNotification.Invoke();
        }

        public static void PrintingDoneA4(string format)
        {
            OnNotification = () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Printing A4 for {0} completed.", format);
                Console.ResetColor();
            };
            OnNotification.Invoke();
        }
    }
}
