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
            string[] subdirectoryEntries = Directory.GetDirectories(@"D:\1");
            foreach (string directoryFullName in subdirectoryEntries)
            {
                string directoryName = new FileInfo(directoryFullName).Name;
                Console.WriteLine(directoryName);
            }
        }
    }
}
