using System;
using CloudBox.DAL.Repositories;

namespace TestRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new Repository("server=localhost;user=root;database=cloudbox;port=3306;password=LukE4321LukE");
            
            repository.RenameFileInDataBase(1, "someDoc_2.doc", "someDoc_1.doc");
            var files = repository.GetAllFilesByUserId(1);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}
