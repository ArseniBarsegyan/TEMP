using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using CsvHelper;
using ManagerService.Models;

namespace ManagerServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            //RecordManager();
        }

        static void RecordManager()
        {
            var manager = CreateManagerDto(@"D:\ManagerServer\Barsegyan_03062017.csv");

            using (var writer = new StreamWriter(@"D:\Barsegyan.txt"))
            {
                foreach (var product in manager.Products)
                {
                    writer.WriteLine($"{product.Name} {product.Client} {product.Price}");
                }
            }
        }

        //Incorrect parse (need to fix name substring and date substring)
        static ManagerDto CreateManagerDto(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            var managerName = fileName.Substring(fileName.LastIndexOf('\\') + 1, fileName.Length - fileName.LastIndexOf('_') - 4);
            var managerDate = fileName.Substring(fileName.LastIndexOf('_') + 1, fileName.Length + 4 - fileName.LastIndexOf('.'));
            var productsList = new List<ProductDto>();

            using (var parser = new CsvParser(new StreamReader(fileName)))
            {
                while (true)
                {
                    var row = parser.Read();
                    if (row == null)
                    {
                        break;
                    }

                    productsList.Add(new ProductDto()
                    {
                        Client = row[0].Trim(),
                        Name = row[1].Trim(),
                        Price = decimal.Parse(row[2].Trim())
                    });
                }
            }

            var manager = new ManagerDto()
            {
                LastName = managerName,
                Date = DateTime.ParseExact(managerDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture),
                Products = productsList
            };
            return manager;
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

                var salesInfos = FillInSalesForm();
                var fileName = $"{managerName}_{DateTime.Now:ddMMyyyy}";
                CreateCsvFile(fileName, salesInfos);
            }
        }

        static IEnumerable<string> FillInSalesForm()
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
