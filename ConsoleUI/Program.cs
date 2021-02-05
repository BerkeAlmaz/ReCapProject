using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            carManager.Add(new Car { Id = 1, BrandId = 2, ColorId = 1, DailyPrice = 400, Description = "bmw", ModelYear = 2020 });
            carManager.Add(new Car { Id = 2, BrandId = 4, ColorId = 3, DailyPrice = 450, Description = "audi", ModelYear = 2014 });
            carManager.Add(new Car { Id = 3, BrandId = 5, ColorId = 1, DailyPrice = 755, Description = "volkswagen", ModelYear = 2010 });
            carManager.Add(new Car { Id = 4, BrandId = 2, ColorId = 4, DailyPrice = 7000, Description = "bmw2", ModelYear = 2000 });
            carManager.Update(new Car { Id = 4, BrandId = 2, ColorId = 4, DailyPrice = 7000, Description = "bmw3", ModelYear = 2000 });
            carManager.Delete(new Car { Id = 2, BrandId = 4, ColorId = 3, DailyPrice = 450, Description = "audi", ModelYear = 2014 });
            carManager.Update(new Car { Id = 3, BrandId = 5, ColorId = 1, DailyPrice = 755, Description = "volkswagen", ModelYear = 2010 });
            //DAILY PRICE = 0 - ERROR - carManager.Add(new Car { Id = 5, BrandId = 7, ColorId = 4, DailyPrice = 0, Description = "slefusz", ModelYear = 2000 });
            //DESCRIPTION.LENGHT < 2 - ERROR - carManager.Add(new Car { Id = 5, BrandId = 2, ColorId = 4, DailyPrice = 452, Description = "a", ModelYear = 1982 });


            foreach (var car in carManager.GetAllByBrandId(2))
            {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("---------------------------------------");

            foreach (var car in carManager.GetAllByColorId(1))
            {
                Console.WriteLine(car.Description);
            }


            Console.WriteLine("---------------------------------------");

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }


            colorManager.Add(new Color { Id = 1, Name = "Black" });
            //colorManager.Delete(new Color { Id = 1 });

            brandManager.Add(new Brand { Id = 1, Name = "BMW" });
            //brandManager.Delete(new Brand { Id = 1 });

        }
    }
}
