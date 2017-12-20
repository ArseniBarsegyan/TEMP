using System;
using System.Collections.Generic;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create item
            var item = CreateItem();
            //adding first generation of categories to item
            AddFirstCategories(item);
        }
        
        //Provides list of first-level categories
        private static void AddFirstCategories(Item item)
        {
            //Request category name
            var firstGeneration = new List<Category>();
            Console.WriteLine($"Enter category name for {item.Name} or 'ok' for next step");
            
            var name = Console.ReadLine();
            while (!name.Equals("ok"))
            {
                firstGeneration.Add(new Category
                {
                    Name = name,
                    Parent = null,
                });
                name = Console.ReadLine();
            }
            item.Categories = firstGeneration;
        }

        private static void AddCategories(List<Category> categories)
        {
            var list = new List<Category>();

            foreach (var cat in categories)
            {
                Console.WriteLine("Enter 1-Add Weight or 2-Add Rate or 3-Add subcategory to current category");
                var answer = Console.ReadLine();
                if (answer.Equals("1") || answer.Equals("2"))
                    RateAllCategories(categories);
                var category = new Category
                {
                    Name = Console.ReadLine(),
                    Parent = cat,
                };
                list.Add(category);
            }
        }

        private static void RateAllCategories(List<Category> categories)
        {
            foreach (var category in categories)
            {
                AddWeightToCategory(category);
            }
        }

        //Count weight and rate of category
        private static void AddWeightToCategory(Category category)
        {
            Console.WriteLine("Add weight to category between 1 and 100");
            var weight = int.Parse(Console.ReadLine());
            category.Weight = weight;

            Console.WriteLine("Add rate to category between 1 and 10");
            var rate = int.Parse(Console.ReadLine());
            category.Rate = rate;
        }

        //Creating root item for rate
        private static Item CreateItem()
        {
            Console.WriteLine("Enter item name");
            var name = Console.ReadLine();

            return new Item
            {
                Name = name,
                Categories = new List<Category>()
            };
        }
    }
}
