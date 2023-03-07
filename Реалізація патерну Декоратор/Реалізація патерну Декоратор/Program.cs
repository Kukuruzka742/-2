using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Реалізація_патерну_Декоратор.Program;

namespace Реалізація_патерну_Декоратор
{

    internal class Program
    {
        public abstract class ArrayDecorator<T> : MyArray<T>
        {
            protected MyArray<T> arr;
            public ArrayDecorator(MyArray<T> arr) : base(arr.Length)
            {
                this.arr = arr;
            }
            public abstract void Print();
        }

        public class ConsolArray<T> : ArrayDecorator<T>
        {
            public ConsolArray(MyArray<T> arr) : base(arr) { }
            public override void Print()
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }
        }

        public class VisualArray<T> : ArrayDecorator<T>
        {
            public VisualArray(MyArray<T> arr) : base(arr) { }
            public override void Print()
            {
                Form1 f1 = new Form1();

                for (int i = 0; i < arr.Length; i++)
                {
                    f1.textBox1.Text += arr[i].ToString() + " ";
                }
                f1.textBox1.ReadOnly = true;
                f1.ShowDialog();
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер массива: ");
            int SIZE = (Convert.ToInt32(Console.ReadLine()));
            MyArray<int> myArray = new MyArray<int>(SIZE);
            ArrayDecorator<int> ca = new ConsolArray<int>(myArray);
            ca.Print();
            ca = new VisualArray<int>(ca);
            ca.Print();
            Console.ReadLine();
        }
    }
}
