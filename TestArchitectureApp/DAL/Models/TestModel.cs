using System.ComponentModel;

namespace DAL.Models
{
    public class TestModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}