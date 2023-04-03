using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Билдер_отчетов
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            ReportDirector director = new ReportDirector(new TextReportBuilder());
            ReportDirector htmldirector = new ReportDirector(new HTMLReportBuilder());

            //------------------------------------------------------------------------

            director.ConstructReport("Заголовок звіту", "Текст звіту", "Підпис");
            string report = director.GetReport();

            Console.WriteLine(report);
            Console.ReadLine();

            using (StreamWriter writer = new StreamWriter("txt.txt", false))
            {
                writer.WriteLine(report);
            }
            string filePath = "txt.txt";
            Process.Start(filePath);

            //-------------------------------------------------------------------------

            htmldirector.ConstructReport("Заголовок звіту", "Текст звіту", "Підпис");
            report = htmldirector.GetReport();

            Console.WriteLine(report);
            Console.ReadLine();

            using (StreamWriter writer = new StreamWriter("index.html", false))
            {
                writer.WriteLine(report);
            }
            filePath = "index.html";
            Process.Start(filePath);

        }
    }
}
