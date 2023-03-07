using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Реалізація_патерну_Декоратор
{
    public class MyArray<T>
    {
        protected T[] array;
        public int Length { get; }

        public MyArray(int length)
        {
            array = new T[length];
            Length = length;
        }

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
            {
                yield return array[i];
            }
        }
    }
}
