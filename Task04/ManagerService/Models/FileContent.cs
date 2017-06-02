using System;
using System.Collections.Generic;

namespace ManagerService.Models
{
    public class FileContent
    {
        public FileContent(string fileName, ICollection<string> clients, IDictionary<string, decimal> products)
        {
            FileName = fileName;
        }

        public DateTime Date { get; }
        public string FileName { get; }
        public ICollection<string> Clients { get; }
        public IDictionary<string, decimal> Products { get; }
        public string ManagerLastName { get; private set; }

        public void AddClients(string client)
        {
            Clients.Add(client);
        }

        public void AddProduct(string product, decimal productPrice)
        {
            Products.Add(product, productPrice);
        }

        private void GetManagerLastName()
        {
            ManagerLastName = FileName.Substring(0, FileName.Length - FileName.IndexOf('_'));
        }
    }
}
