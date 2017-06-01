using System;
using ManagerServiceConsole;

namespace ManagerService.Models
{
    public class CsvFile
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Manager Manager { get; set; }
    }
}
