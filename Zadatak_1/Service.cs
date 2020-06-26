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
        public static bool pc1, pc2, pc3, pc4, pc5, pc6, pc7, pc8, pc9, pc10 = false;
        public static EventWaitHandle ewh = new AutoResetEvent(false);

        public static void Pc(Printer p)
        {
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
            ValidatePC(Thread.CurrentThread.Name);
            smp1.Release();
            ewh.Set();
        }

        public static void PrintA4(Printer printer)
        {
            smp2.Wait();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Printing A4, for {0}...", Thread.CurrentThread.Name);
            Console.ResetColor();
            Thread.Sleep(1000);
            PrintingDoneA4(Thread.CurrentThread.Name);
            ValidatePC(Thread.CurrentThread.Name);
            smp2.Release();
            ewh.Set();
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

        public static void ValidatePC(string s)
        {
            if(s == "PC_1") { pc1 = true; }
            else if(s == "PC_2") { pc2 = true; }
            else if (s == "PC_3") { pc3 = true; }
            else if (s == "PC_4") { pc4 = true; }
            else if (s == "PC_5") { pc5 = true; }
            else if (s == "PC_6") { pc6 = true; }
            else if (s == "PC_7") { pc7 = true; }
            else if (s == "PC_8") { pc8 = true; }
            else if (s == "PC_9") { pc9 = true; }
            else if (s == "PC_10") { pc10 = true; }
            
            if(pc1 && pc2 && pc3 && pc4 && pc5 && pc6 && pc7 && pc8 && pc9 && pc10)
            {
                Program.all = true;
            }
        }
    }
}
