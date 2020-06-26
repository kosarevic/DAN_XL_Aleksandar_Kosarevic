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
        //Semaphore made to ensure only one thread may execute coresponding part of code.
        public static SemaphoreSlim smp1 = new SemaphoreSlim(1);
        public static SemaphoreSlim smp2 = new SemaphoreSlim(1);
        //Bool values made to keep track of every pc heaving printed at least once.
        public static bool pc1, pc2, pc3, pc4, pc5, pc6, pc7, pc8, pc9, pc10 = false;
        //Event handle tracks when printing proces completes.
        public static EventWaitHandle ewh = new AutoResetEvent(false);
        /// <summary>
        /// Method purpose is to separate printing requests based on format chosen.
        /// </summary>
        /// <param name="p"></param>
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
        /// <summary>
        /// Method simulates printing of A3 size document.
        /// </summary>
        /// <param name="printer"></param>
        public static void PrintA3(Printer printer)
        {
            //Semaphore applyed.
            smp1.Wait();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Printing A3, for {0}...", Thread.CurrentThread.Name);
            Console.ResetColor();
            Thread.Sleep(1000);
            //Delegate called.
            PrintingDoneA3(Thread.CurrentThread.Name);
            //Validation checked.
            ValidatePC(Thread.CurrentThread.Name);
            smp1.Release();
            //Block event removed.
            ewh.Set();
        }
        /// <summary>
        /// Method simulates printing of A4 sized document.
        /// </summary>
        /// <param name="printer"></param>
        public static void PrintA4(Printer printer)
        {
            //Semaphore applyed.
            smp2.Wait();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Printing A4, for {0}...", Thread.CurrentThread.Name);
            Console.ResetColor();
            Thread.Sleep(1000);
            //Delegate called.
            PrintingDoneA4(Thread.CurrentThread.Name);
            //Validation checked.
            ValidatePC(Thread.CurrentThread.Name);
            smp2.Release();
            //Block event removed.
            ewh.Set();
        }
        /// <summary>
        /// Delegate and Notification for signaling printing completion.
        /// </summary>
        public delegate void Notification();

        public static event Notification OnNotification;
        /// <summary>
        /// Completion notification for A3 format document.
        /// </summary>
        /// <param name="format"></param>
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
        /// <summary>
        /// Completion notification for A4 format document.
        /// </summary>
        /// <param name="format"></param>
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
        /// <summary>
        /// Method checks if pc had at least one printing of document, and sets coresponding bool value.
        /// </summary>
        /// <param name="s"></param>
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
            //If all PC-s had at least one printing, application will end with bool "all" set to True.
            if(pc1 && pc2 && pc3 && pc4 && pc5 && pc6 && pc7 && pc8 && pc9 && pc10)
            {
                Program.all = true;
            }
        }
    }
}
