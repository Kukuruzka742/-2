using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace синглтон
{
    public class Logger
    {
        private static Logger instance = null;
        private string userName;

        protected Logger(string n)
        {
            userName = n;
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine("");
            }
        }

        public static Logger Initialize(string name)
        {
            if (instance == null)
            {
                instance = new Logger(name);
            }
            return instance;
        }

        public void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: {userName} {message}");
            }
        }

        public void Message(string message)
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }

}
