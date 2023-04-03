using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace синглтон
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            Console.Clear();
            Logger logger = Logger.Initialize(name);
            logger.Log(" запустил приложение");
            double a, b;
            char operation;

            while (true)
            {
                Console.Write("Введите первое число: ");
                a = Convert.ToDouble(Console.ReadLine());
                logger.Log(" ввел первое число а = " + a.ToString());
                Console.Clear();
                Console.Write("Введите операцию: ");
                operation = Convert.ToChar(Console.ReadLine()[0]);
                logger.Log(" ввел операцию " + operation.ToString());
                while (true)
                {
                    if (operation != '+' && operation != '-' && operation != '*' && operation != '/')
                    {
                        Console.WriteLine("Недопустимая операция, попробуйте ещё раз");
                        logger.Log(" ввел недопустимую операцию");
                        operation = Convert.ToChar(Console.ReadLine());
                    }
                    else
                    {
                        logger.Log(" ввел приавильную операцию " + operation.ToString());
                        break;
                    }
                }
                Console.Clear();
                Console.Write("Введите второе число: ");
                b = Convert.ToDouble(Console.ReadLine());
                logger.Log(" ввел второе число b = " + b.ToString());
                Console.Clear();

                switch (operation)
                {
                    case '+':
                        Console.WriteLine("Равно: " + (a + b).ToString());
                        logger.Message("Конечным результатом стало число " + (a + b).ToString());
                        break;
                    case '-':
                        Console.WriteLine("Равно: " + (a - b).ToString());
                        logger.Message("Конечным результатом стало число " + (a - b).ToString());
                        break;
                    case '*':
                        Console.WriteLine("Равно: " + (a * b).ToString());
                        logger.Message("Конечным результатом стало число " + (a * b).ToString());
                        break;
                    case '/':
                        Console.WriteLine("Равно: " + (a / b).ToString());
                        logger.Message("Конечным результатом стало число " + (a / b).ToString());
                        break;
                }

                Console.WriteLine("\nВы хотите продолжить? \n[1]Нет \n[2]Да");

                if (Console.ReadLine() == "1")
                {
                    logger.Log(" выходит из приложения");
                    break;
                }
                else
                {
                    logger.Log(" продожает");
                    Console.Clear();
                }
            }
            string filePath = "log.txt";
            Process.Start(filePath);
        }
    }
}
