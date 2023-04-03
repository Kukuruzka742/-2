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
            Console.WriteLine("1. Патерн Builder за створення звіту\n" +
                              "2. Патерн Builder з можливістю клонування створеного звіту(Прототип)");
            if (Console.ReadLine() == "1")
            {
                Console.Clear();
                ReportDirector director = new ReportDirector(new TextReportBuilder());

                //------------------------------------------------------------------------

                director.ConstructReport("Заголовок звіту", "Текст звіту", "Підпис");

                Console.WriteLine(director);
                Console.ReadLine();

                using (StreamWriter writer = new StreamWriter("txt.txt", false))
                {
                    writer.WriteLine(director);
                }
                string filePath = "txt.txt";
                Process.Start(filePath);

                //-------------------------------------------------------------------------

                ReportDirector htmldirector = new ReportDirector(new HTMLReportBuilder());

                htmldirector.ConstructReport("Заголовок звіту", "Текст звіту", "Підпис");

                Console.WriteLine(htmldirector);
                Console.ReadLine();

                using (StreamWriter writer = new StreamWriter("index.html", false))
                {
                    writer.WriteLine(htmldirector);
                }
                filePath = "index.html";
                Process.Start(filePath);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Реализация с Clone\n");

                ReportDirector director1 = new ReportDirector(new TextReportBuilder());
                ReportDirector director2 = director1.Clone();

                director1.ConstructReport("Заголовок орегінального звіту", "Текст орегінального звіту", "Орегінальний підпис");
                director2.ConstructReport("Заголовок скопійованого звіту", "Текст скопійованого звіту", "Скопійований підпис");

                Console.WriteLine(director1);
                Console.ReadLine();

                using (StreamWriter writer = new StreamWriter("txt1.txt", false))
                {
                    writer.WriteLine(director1);
                }

                Console.WriteLine(director2);
                Console.ReadLine();

                using (StreamWriter writer = new StreamWriter("txt2.txt", false))
                {
                    writer.WriteLine(director2);
                }

                Console.WriteLine(director1.GetHashCode());
                Console.WriteLine(director2.GetHashCode());
                //-------------------------------------------------------------------------------------

                Console.WriteLine("\nРеализация с DeepCopy\n");

                ReportDirector director12 = new ReportDirector(new TextReportBuilder());
                ReportDirector director22 = director12.DeepCopy();

                director12.ConstructReport("Заголовок орегінального звіту", "Текст орегінального звіту", "Орегінальний підпис");
                director22.ConstructReport("Заголовок скопійованого звіту", "Текст скопійованого звіту", "Скопійований підпис");
                
                Console.WriteLine(director12);
                Console.ReadLine();

                using (StreamWriter writer = new StreamWriter("txt12.txt", false))
                {
                    writer.WriteLine(director12);
                }

                Console.WriteLine(director22);
                Console.ReadLine();

                using (StreamWriter writer = new StreamWriter("txt22.txt", false))
                {
                    writer.WriteLine(director22);
                }

                Console.WriteLine(director12.GetHashCode());
                Console.WriteLine(director22.GetHashCode());

                /*
                Метод GetHashCode позволяет возвратить некоторое числовое значение, 
                которое будет соответствовать данному объекту или его хэш-код. 
                По данному числу, например, можно сравнивать объекты.
                 */

                Console.ReadKey();
            }
        }
    }
}
