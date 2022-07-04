using ContosoPets.Data;
using ContosoPets.Models;

namespace ContosoPets
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create();
            //Read();
            //Update();
            //Read();
            //Delete();
            Read();
        }

        private static void Create()
        {
            using ContosoPetsContext context = new ContosoPetsContext();
            Product squeakyBone = new Product()
            {
                Name = "Squeaky Dog Bone",
                Price = 4.99M
            };
            context.Products.Add(squeakyBone);

            Product tennisBalls = new Product()
            {
                Name = "Tennis Ball 3-Pack",
                Price = 9.99M
            };

            context.Add(tennisBalls);

            context.SaveChanges();
        }

        private static void Read()
        {
            using ContosoPetsContext context = new ContosoPetsContext();
            var products = from product in context.Products
                           where product.Price > 5.00m
                           orderby product.Name
                           select product;

            foreach (var product in products)
            {

                Console.WriteLine($"Id: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine(new String('-', 20));

            }
        }

        private static void Update()
        {
            using ContosoPetsContext context = new ContosoPetsContext();
            var a_product = (from product in context.Products
                             where product.Name == "Squeaky Dog Bone"
                             select product).FirstOrDefault();

            if (a_product is Product)
            {
                a_product.Price = 7.99m;
            }
            context.SaveChanges();
        }

        private static void Delete()
        {
            using ContosoPetsContext context = new ContosoPetsContext();
            var a_product = (from product in context.Products
                             where product.Name == "Squeaky Dog Bone"
                             select product).FirstOrDefault();

            if (a_product is Product)
            {
                context.Remove(a_product);
            }
            context.SaveChanges();
        }
    }
}