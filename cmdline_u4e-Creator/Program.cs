using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmdline_u4e_Creator
{
    class Program
    {
        private static LogoRenderer logo = new LogoRenderer();

        //private static Imaging bild = new Imaging();

        


        static void Main(string[] args)
        {
            Console.Title = "u4e-Creator Commandline";
            //logo.DrawHeader("usenet4ever - Das freundliche Usenet-Board !");



            

            //Bitmap Ascii = Imaging.GetImageFromURL(@"https://www.usenetrevolution.info/vb/bilder/logo3.gif");
            //Bitmap AsciiBild1 = Imaging.ConvertToGrayscale(Ascii);
            //Bitmap AsciiBild2 = Imaging.GetAvarageGrayscale(AsciiBild1);
            //string AsciiBild3 = Imaging.ConvertToAsciiArt(Ascii);
            //Console.Write(AsciiBild3);

            Console.WriteLine("1.Basis-Quellverzeichnis einlesen - Bitte geben Sie das Verzeichnis ein, das Bearbeitet werden soll:");
            Console.Read();
        }

        //private static QuelleVZ ChooseVZ()
        //{

        //}
    }
}
