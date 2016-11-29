using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;

namespace cmdline_u4e_Creator
{
   
    public class Imaging
    {

            /// <summary>
            /// Converts a colored bitmap to a grayscale bitmap.
            /// http://loop.servehttp.com/~vertexwahn/oldwiki/public_html_an_turing/FarbbildGrauwertbild.pdf
            /// </summary>
            /// <param name="sourceBitmap"></param>
            /// <returns></returns>
            public static Bitmap ConvertToGrayscale(Bitmap sourceBitmap)
            {
            Bitmap bitmap = new Bitmap(sourceBitmap.Width,
                        sourceBitmap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                for (int y = 0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        System.Drawing.Color color = sourceBitmap.GetPixel(x, y);
                        int grayscale = ((77 * color.R) + (151 * color.G) + (28 * color.B)) >> 8;
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, grayscale, grayscale, grayscale));
                    }

                return bitmap;
            }

            /// <summary>
            /// Gets the bitmap from URL.
            /// http://dotnet-snippets.de/dns/image-aus-url-laden-SID509.aspx 
            /// </summary>
            /// <param name="url">The URL.</param>
            /// <returns></returns>
            public static Bitmap GetImageFromURL(string url)
            {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                System.IO.Stream stream = httpWebReponse.GetResponseStream();
                return new Bitmap(System.Drawing.Image.FromStream(stream));
            }

            /// <summary>
            /// Computes the avarange grayscale value of a certain bitmap region.
            /// </summary>
            /// <param name="grayscaleBitmap">A grayscale bitmap.</param>
            /// <param name="rectangle">Defines a region.</param>
            /// <returns></returns>
            public static int GetAvarageGrayscale(Bitmap grayscaleBitmap,
                System.Drawing.Rectangle rectangle)
            {
                int average = 0;

                try
                {
                    for (int y = rectangle.Y; y < rectangle.Bottom; y++)
                        for (int x = rectangle.X; x < rectangle.Right; x++)
                        {
                            average += grayscaleBitmap.GetPixel(x, y).R;
                        }
                }
                catch (System.Exception ex)
                {
                    return 128;
                }

                return average / (rectangle.Width * rectangle.Height);
            }

            /// <summary>
            /// Computes the difference between to different bitmap regions.
            /// </summary>
            /// <param name="grayscaleBitmapA">First bitmap.</param>
            /// <param name="compareRectA">First bitmap region.</param>
            /// <param name="grayscaleBitmapB">Secound bitmap</param>
            /// <param name="compareRectB">Secound bitmap region.</param>
            /// <returns></returns>
            public static int ComputeDistance(Bitmap grayscaleBitmapA,
                                System.Drawing.Rectangle compareRectA,
                                Bitmap grayscaleBitmapB,
                                System.Drawing.Rectangle compareRectB)
            {
                int averageA = GetAvarageGrayscale(grayscaleBitmapA, compareRectA);
                int averageB = GetAvarageGrayscale(grayscaleBitmapB, compareRectB);

                return System.Math.Abs(averageA - averageB);
            }

            /// <summary>
            /// Applies an edge filter to the bitmap.
            /// </summary>
            /// <param name="bitmap">A bitmap.</param>
            /// <returns>Edge bitmap.</returns>
            public static Bitmap DetectEdges(Bitmap bitmap)
            {
            Bitmap grayscaleMap = ConvertToGrayscale(bitmap);

            Bitmap edgeMap =
                    new Bitmap(grayscaleMap.Width, grayscaleMap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                for (int y = 1; y < edgeMap.Height - 1; y++)
                {
                    for (int x = 1; x < edgeMap.Width - 1; x++)
                    {
                        int sum = System.Math.Abs(grayscaleMap.GetPixel(x - 1, y - 1).R + grayscaleMap.GetPixel(x, y - 1).R + grayscaleMap.GetPixel(x + 1, y - 1).R
                            - grayscaleMap.GetPixel(x - 1, y + 1).R - grayscaleMap.GetPixel(x, y + 1).R - grayscaleMap.GetPixel(x + 1, y + 1).R);

                        System.Drawing.Color z1 = grayscaleMap.GetPixel(x - 1, y - 1);
                        System.Drawing.Color z2 = grayscaleMap.GetPixel(x, y - 1);
                        System.Drawing.Color z3 = grayscaleMap.GetPixel(x + 1, y - 1);
                        System.Drawing.Color z4 = grayscaleMap.GetPixel(x - 1, y + 1);
                        System.Drawing.Color z5 = grayscaleMap.GetPixel(x, y + 1);
                        System.Drawing.Color z6 = grayscaleMap.GetPixel(x + 1, y + 1);

                        int foo = z1.R + z2.R + z3.R - z4.R - z5.R - z6.R;
                        if (sum > 255)
                            sum = 255;

                        sum = 255 - sum;

                        edgeMap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, sum, sum, sum));
                    }
                }

                return edgeMap;
            }

            /// <summary>
            /// Converts a bitmap to ascii art.
            /// </summary>
            /// <param name="bitmap">A bitmap.</param>
            /// <returns>Ascii art.</returns>
            /// <example>
            /* 0:::::::::::::::::::::::::::::$MMMMM%:::::::::::::::::::::::::::::::::::::M
                %                            (#''` !M8                                    M
                %                    :QMMMMMMMM849640MMM@(                                M
                %                 "MMMMMMM4*' HM:  !MB#MMMMMM$                            M
                %               'MMMM3        ''$844$    `6MMMMM'                         M
                % '#MNN#H@@@@@@HMMM!          '''`           $MMM3                        M
                %1M@99889QB81!!M*M4'`                          @QM:                       M
                %1Q'"B000008MMM*`MM:`''`                       !M@3                :&$;`  M
                %0`  $`      !B  `*MM6  ''''`                 'MM(!:            $MMMMMMMMMM
                %!&  `8'     #`    ''1MM@H3'`'''`         '$MMMH   %          4M$''!@BMMM4M
                % ;0   46   (3       `''''1#@HBH#&$&$448MMMM0'     :'       (M& ':@BQ@"   M
                %  !B(16BMQ`B                       :894:          `*     'MH   B8Q0`     M
                %    &MM` *M&                      !4               ;:"Q#M#'  !@3Q`       M
                %      4MM;&`                      "                (01'`'''`"Q0"         M
                %        :MN                      `(                `'''`  '84B'          M
                %                                 &                 `` `''QB09`           M
                %          !                     :(                 '389088@;             M
                %          1:                   '9                  !"6QQQ!               M
                %          `#                  :B                  `84'                   M
                %           (#                04                   &'                     M
                %            1M6           `9B!                   $:                      M
                %             !M#006(:'"6490`                   `Q:                       M
                %               4B! :(1!                      `48`                        M
                %                 4@9'                      ;@B!                          M
                %                   '9QB0!              ;8QB3                             M
                %                       `&88909444099898%                                 M
                0:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::M 
            */
            /// </example>
            public static string ConvertToAsciiArt(Bitmap bitmap)
            {
            Bitmap edgeMap = DetectEdges(bitmap);

                string output = "";

                for (int y = 0; y < edgeMap.Height; y += 13)
                {
                    for (int x = 0; x < edgeMap.Width; x += 8)
                    {
                        int minDistance = 999999999;
                        char minChar = ' ';

                        for (int i = 32; i < 127; i++)
                        {
                            char c = (char)i;
                            string str = "" + c;

                        Bitmap characterMap =
                                new Bitmap(8, 13, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(characterMap);
                            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White),
                                            new System.Drawing.Rectangle(0, 0, characterMap.Width, characterMap.Height));
                            g.DrawString(str,
                                new System.Drawing.Font("Courier New", 5),
                                new System.Drawing.SolidBrush(System.Drawing.Color.Black), -2, -2);

                            int tmp = ComputeDistance(characterMap,
                                new System.Drawing.Rectangle(0, 0, characterMap.Width, characterMap.Height),
                                edgeMap,
                                new System.Drawing.Rectangle(x, y, characterMap.Width, characterMap.Height));

                            if (tmp < minDistance)
                            {
                                minDistance = tmp;
                                minChar = c;
                            }
                        }

                        output += minChar;
                    }

                    output += "\r\n";
                }

                return output;
            }
    }
    
}

