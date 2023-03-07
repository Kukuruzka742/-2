using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Реалізація_патерну_Декоратор
{

    internal class Program
    {
        public abstract class ArrayDecorator<T>
        {
            protected T[] mass;
            public ArrayDecorator(T[] mass)
            { 
                this.mass = mass;
            }
            public abstract void Print();
        }

        public class ConsolArray<T> : ArrayDecorator<T>
        {
            public ConsolArray(T[] mass) : base(mass) {  }
            public override void Print()
            {
                foreach (T t in mass)
                {
                    Console.Write(t + " ");
                }
            }
        }

        public class VisualArray<T> : ArrayDecorator<T>
        {
            public VisualArray(T[] mass) : base(mass) { }
            public override void Print()
            {
                Form1 f1 = new Form1();
                
                foreach (T t in mass)
                {
                    f1.textBox1.Text += t + " ";
                }
                f1.textBox1.ReadOnly = true;
                f1.ShowDialog();
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер массива: ");
            int SIZE = (Convert.ToInt32(Console.ReadLine()));
            int[] mass = Enumerable.Range(0, SIZE).ToArray();

            ArrayDecorator<int> ca = new ConsolArray<int>(mass);
            ca.Print();
            ca = new VisualArray<int>(mass);
            ca.Print();
            Console.ReadLine();
        }
    }
}
