using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HueColorConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Startup
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            var attribute = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                    .Cast<AssemblyDescriptionAttribute>().FirstOrDefault();
            var appName = string.Format("{0} v{1}", typeof(Program).Assembly.GetName().Name, versionInfo.ProductVersion);

            Console.Title = appName;
            Console.WindowWidth = 80;
            Console.BufferWidth = 80;
            Console.WindowHeight = 36;


            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(appName);
            Console.ResetColor();
            if (attribute != null)
                Console.WriteLine(attribute.Description);
            Console.WriteLine();
            Console.WriteLine(versionInfo.LegalCopyright);
            Console.WriteLine(String.Empty.PadLeft(80, '-'));

            #endregion

            Console.WriteLine("Converters:");
            Console.WriteLine("1. RGB hexvalue to HSB (hue, sat, bri)");
            Console.WriteLine("2. RGB (red, green, blue) to HSB (hue, sat, bri)");
            Console.WriteLine("3. HSB (hue, sat, bri) to RGB hexvalue");
            Console.WriteLine();
            Console.Write("Enter number of converter you wan't to use: ");
            var converterSelection = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine(String.Empty.PadLeft(80, '-'));

            switch (converterSelection.KeyChar)
            {
                case '1':
                    Console.WriteLine("Convert RGB hexvalue to HSB (hue, sat, bri)");
                    Console.WriteLine(String.Empty.PadLeft(80, '-'));
                    Console.WriteLine("Enter RGB hexcode (6 digits):");
                    Console.Write("#");
                    var input = Console.ReadLine();
                    Console.WriteLine();
                    var rgbInput = new RGBColor();
                    try
                    {
                        rgbInput = new RGBColor(input);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input!");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                        return;
                    }

                    var hsb = rgbInput.GetHSB();
                    Console.WriteLine("Hue: {0}", hsb.Hue);
                    Console.WriteLine("Sat: {0}", hsb.Saturation);
                    Console.WriteLine("Bri: {0}", hsb.Brightness);

                    break;
                case '2':
                    Console.WriteLine("Convert RGB (red, green, blue) to HSB (hue, sat, bri)");
                    Console.WriteLine(String.Empty.PadLeft(80, '-'));
                    Console.WriteLine("Enter the RGB values (0-255):");
                    Console.Write("R: ");
                    var r = Console.ReadLine().ToByte();
                    Console.Write("G: ");
                    var g = Console.ReadLine().ToByte();
                    Console.Write("B: ");
                    var b = Console.ReadLine().ToByte();

                    Console.WriteLine();
                    var rgbInput2 = new RGBColor(r, g, b);

                    var hsb2 = rgbInput2.GetHSB();
                    Console.WriteLine("Hue: {0}", hsb2.Hue);
                    Console.WriteLine("Sat: {0}", hsb2.Saturation);
                    Console.WriteLine("Bri: {0}", hsb2.Brightness);

                    break;
                case '3':
                    Console.WriteLine("Convert HSB (hue, sat, bri) to RGB hexvalue");
                    Console.WriteLine(String.Empty.PadLeft(80, '-'));
                    Console.WriteLine("Enter the HSB values:");
                    Console.Write("Hue: ");
                    var hue = Console.ReadLine().ToUInt16(); //This is a wrapping value between 0 and 65535
                    Console.Write("Sat: ");
                    var sat = Console.ReadLine().ToByte();
                    Console.Write("Bri: ");
                    var bri = Console.ReadLine().ToByte();

                    Console.WriteLine();
                    var hsbInput = new HSB(hue, sat, bri);

                    Console.WriteLine("RGB: #{0}", hsbInput.GetRGB().ToHex());

                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input!");
                    break;
            }



            Console.ReadKey();

        }


    }

    public static class StringExtensions
    {
        public static byte ToByte(this string inputString)
        {
            byte value;

            if (!Byte.TryParse(inputString, out value))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return value;
        }

        public static UInt16 ToUInt16(this string inputString)
        {
            UInt16 value;

            if (!UInt16.TryParse(inputString, out value))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return value;
        }
    }
}
