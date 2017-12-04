using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waps.client
{
    public class Output
    {
        public static void Error(string iErrorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n");
            Console.WriteLine(string.Format("ERROR:\t{0}", iErrorMsg));
            Console.WriteLine("\n*************按回车退出");
            Console.ReadLine();
        }

        public static void Hint(string iHineMsg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("{0}", iHineMsg));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Message(string iMsg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("{0}", iMsg));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Result(string iErrorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n***********************************************************");
            Console.WriteLine(string.Format("\t{0}\n\n", iErrorMsg));
        }
    }
}
