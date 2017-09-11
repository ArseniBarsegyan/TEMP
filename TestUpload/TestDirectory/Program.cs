using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = "as:asd";
            var el1 = name.Split(':')[0];
            var el2 = name.Split(':')[1];
            Console.WriteLine(el1);
            Console.WriteLine(el2);
        }
    }
}
