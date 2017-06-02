using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using ManagerService.Models;

namespace ManagerServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            GetCsvReadResult("D:\\ManagerServer\\Barsegyan_02062017.csv");
        }

        static FileContent GetCsvReadResult(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            using (var parser = new CsvParser(new StreamReader(fileName)))
            {
                var fileContent = new FileContent(fileName, new List<string>(), new Dictionary<string, decimal>());
                while (true)
                {
                    var row = parser.Read();
                    if (row == null)
                        break;
                    foreach (var element in row)
                    {
                        Console.WriteLine(element);
                    }
                }
            }
            return null;
        }

        static void Run()
        {
            for(;;)
            {
                Console.WriteLine("Enter your last name or type 'quit' to close the program");
                var command = Console.ReadLine();

                if (command != null && command.ToLower().Equals("quit"))
                {
                    break;
                }

                var managerName = command;
                Console.WriteLine("Enter [client name], [product name], [product price]");

                var salesInfos = FillSalesForm();
                var fileName = $"{managerName}_{DateTime.Now:ddMMyyyy}";
                CreateCsvFile(fileName, salesInfos);
            }
        }

        static IEnumerable<string> FillSalesForm()
        {
            var lines = new List<string>();

            for (;;)
            {
                var command = Console.ReadLine();
                if (command != null && command.ToLower().Equals("ok"))
                {
                    break;
                }

                if (command == null) continue;
                lines.Add(command);
            }
            return lines;
        }

        static void CreateCsvFile(string fileName, IEnumerable<string> lines)
        {
            var errorMessage = "Please, check input format";

            if (!Directory.Exists(ConfigurationManager.AppSettings["outputFileDirectory"]))
            {
                Directory.CreateDirectory(ConfigurationManager.AppSettings["outputFileDirectory"]);
            }
            try
            {
                using (var fileStream = new StreamWriter(ConfigurationManager.AppSettings["outputFileDirectory"]
                                                         + $"{fileName}.csv", false, Encoding.Default))
                {
                    foreach (var line in lines)
                    {
                        var lineContent = line.Split(',');
                        var client = lineContent[0];
                        var productName = lineContent[1];
                        var productPrice = decimal.Parse(lineContent[2]);
                        fileStream.WriteLine($"{client},{productName},{productPrice}");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine(errorMessage);
                File.Delete(ConfigurationManager.AppSettings["outputFileDirectory"]
                            + $"{fileName}.csv");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine(errorMessage);
                File.Delete(ConfigurationManager.AppSettings["outputFileDirectory"] 
                    + $"{fileName}.csv");
            }
        }
    }
}
