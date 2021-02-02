using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            carManager.Add(new Car { Id = 5, Description = "Berkeden", BrandId = 2});

            foreach (var item in carManager.GetByBrandId(2))
            {
                Console.WriteLine(item.Description);
            }

            Console.WriteLine("****************************************");

            foreach (var item in carManager.GetAll())
            {
                Console.WriteLine(item.Description);
            }

            Console.WriteLine("****************************************");

            Console.WriteLine(carManager.GetById(2).Description);

        }
    }
}
