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

            Console.ReadKey();



        }
    }
}
