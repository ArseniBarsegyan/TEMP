using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        public ClientService()
        {
            TestData = new List<string>();
        }

        public IList<string> TestData { get; }

        public string GetValue(int id)
        {
            return TestData.ElementAt(id);
        }

        public void AddValue(string value)
        {
            TestData.Add(value);
        }

        public void RemoveValue(string value)
        {
            TestData.Remove(value);
        }
    }
}