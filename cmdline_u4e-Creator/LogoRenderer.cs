using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cmdline_u4e_Creator
{
    internal class LogoRenderer
    {
        private const int PADDING = 6;
        private const int LINE_HEIGHT = 4;



        internal void DrawHeader(string titletext)
        {
            Console.Write(CreateTopLine());

            for (int counter = 0; counter < LINE_HEIGHT; counter++)
            {

                // Anfang - Posi +5   
                // Ende - Posi WindowWidth - 5
                Console.Write(CreateMidLine());
                //var counter2 = string.Format("Top:{0}", counter);
                //Console.WriteLine(counter2);
            }

            DrawTitle(titletext);

            for (int counter = 0; counter < LINE_HEIGHT; counter++)
            {
                Console.Write(CreateMidLine());
                //var counter3 = string.Format("Bottom:{0}\r", counter);
                //Console.WriteLine(counter3);
            }
            Console.Write(CreateBottomLine());
        }



        void DrawTitle(string titletext)
        {
            //var titlebegin = string.Format(" {0," + (((Console.WindowWidth) /2) -5) + "}", "*");
            //Console.WriteLine(titlebegin);
           
            //Console.Write("    *");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var title = string.Format(" {0," + (Console.WindowWidth) / 2 + "}", titletext , " {1," + (((Console.WindowWidth) /2 -5)) + "}","*");
            Console.Write(title);
            Console.ResetColor();
            Console.WriteLine("    *");
            //var titleEnd = string.Format(" {0," + (((Console.WindowWidth) / 2) -15) + "}", "*" );
            //Console.Write(titleEnd);
            
        }
        
        private string CreateMidLine()
        {
            var midLineText = string.Format("\u8968");
            //var spaceText = string.Format("\e155  ", Console.WindowWidth - PADDING);
            //var midLineText = string.Format("  |{0}|  ", spaceText);

            return midLineText;

        }


        private string CreateTopLine()
        {
            Console.Write("");
            for (int counter = 0; counter < (Console.WindowWidth -15); counter++)
            {
                //var counter2 = string.Format("Top:{0}", counter);
                //Console.Write(counter2);
                Console.Write("=");
            }
            Console.WriteLine(" ");
            //var topLineText = string.Format("X\n");
            var barText = string.Format("*", Console.WindowWidth - PADDING);
            var topLineText = string.Format("  {0}~  ", barText);
            return topLineText;

        }



        private string CreateBottomLine()
        {
            var bottomLineText = string.Format("Z");
            //var bottomText = string.Format("\r=", Console.WindowWidth - PADDING);
            //var bottomLineText = string.Format("  ~{0}~  ", bottomText);
            return bottomLineText;

        }
    }
}
