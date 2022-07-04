// See https://aka.ms/new-console-template for more information
using ContosoPets.Data;
using ContosoPets.Models;

namespace ContosoPets
{
    class Program
    {
        static void Main(string[] args)
        {
            using ContosoPetsContext context = new ContosoPetsContext();
            var squeakyBone = context.Products
                 .Where(p => p.Name == "Squakey Dog Bone" && p.ProductId == 1)
                 .FirstOrDefault();

            if (squeakyBone is Product)
            {
                squeakyBone.Price = 7.99m;
            }
            context.SaveChanges();
            var squeakyBone2 = context.Products
                .Where(p => p.Name == "Squakey Dog Bone" && p.ProductId == 3)
                .FirstOrDefault();
            if (squeakyBone2 is Product)
            {
                context.Remove(squeakyBone2);
            }
            context.SaveChanges();
            var products = from product in context.Products
                           
                           orderby product.Name
                           select product;
            foreach(Product p in products)
            {
                Console.WriteLine($"Id:     {p.ProductId}");
                Console.WriteLine($"Name    {p.Name}");
                Console.WriteLine($"Price:  {p.Price}");
                Console.WriteLine(new String('-', 20));
            }
            


        }

    }
}
