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
                Console.WriteLine("Password from DB:");
                string password = client.GetPasswordFromDb();
                Console.WriteLine(password);
            }
            Console.ReadLine();
        }
    }
}
