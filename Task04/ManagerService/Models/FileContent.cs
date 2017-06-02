using System;
using System.Collections.Generic;

namespace ManagerService.Models
{
    public class FileContent
    {
        public FileContent(string fileName, ICollection<string> clients, IDictionary<string, decimal> products)
        {
            FileName = fileName;
            GetManagerLastName();
            SetTime();
        }

        public Guid FileId { get; private set; }
        private DateTime Date { get; set; }
        private string FileName { get; }
        private ICollection<string> Clients { get; }
        private IDictionary<string, decimal> Products { get; }
        private string ManagerLastName { get; set; }

        public void GenerateGuid()
        {
            FileId = Guid.NewGuid();
        }

        public void AddClient(string client)
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

        private void SetTime()
        {
            Date = DateTime.Parse(FileName.Substring(FileName.Length - FileName.IndexOf('_') + 1));
        }
    }
}
