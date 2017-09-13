using System;
using ServiceConsoleClient.ServiceReference1;

namespace ServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CloudBoxServiceClient client = new CloudBoxServiceClient())
            {
                bool result = client.ValidatePassword("123");
                Console.WriteLine(result);
            }
            Console.ReadLine();
        }
    }
}
