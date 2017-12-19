using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        private static List<Category> _categories = new List<Category>();

        static void Main(string[] args)
        {
            //Creatin root category and put in into storage
            var rootCategory = CreateRootCategory();
            _categories.Add(rootCategory);

            //Start creating categories until user enter 'exit'
            AddCategory(rootCategory);
            //Start counting prices of all categories
            AddWeightOrRate();
        }

        private static void AddCategory(Category parentCategory)
        {
            //Request category name
            Console.WriteLine("Enter category name or exit to stop");
            var name = Console.ReadLine();

            //if enter exit break loop
            if (name != null && !name.Equals("exit"))
            {
                var category = new Category
                {
                    Name = name,
                    Parent = parentCategory
                };
                _categories.Add(category);
                AddCategory(category);
            }
            //If loop ended then proceed with rate for current category
            //so call rate method here
        }
        
        private static void AddWeightOrRate(Category category)
        {
            Console.WriteLine("Chosee 1-Add Weight or 2-Add Rate");
            var entry = Console.ReadLine();

            if (entry == "1")
            {
                var totalWeight = 100;
            }
            else if (entry == "2")
            {
            }
        }

        private static Category CreateRootCategory()
        {
            Console.WriteLine("Enter item");
            var name = Console.ReadLine();
            return new Category
            {
                Name = name,
                Parent = null,
                Rate = 0
            };
        }
    }
}
