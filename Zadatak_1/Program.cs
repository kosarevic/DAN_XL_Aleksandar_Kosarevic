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
        static void Main(string[] args)
        {
            Read();

            for (int i = 1; i <= 10; i++)
            {
                Thread t = new Thread(Service.Pc);
                t.Name = string.Format("PC_{0}", i);
                t.Start();
                Thread.Sleep(100);
            }

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
