﻿using Business.Abstract;
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
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            UserManager userManager = new UserManager(new EfUserDal());

            //AddCar(carManager);
            //AddBrand(brandManager);
            //AddColor(colorManager;
            //GetAllCarDetails(carManager);
            //GetCarDetail(carManager);
            //RentalTest(rentalManager);


            //var test = rentalManager.Add(new Rental { CarId = 5, ReturnDate = default });
            //Console.WriteLine(test.Message);
            Console.WriteLine(rentalManager.GetByCarIdCurrent(5).Message);
            



            //Console.WriteLine(rentalManager.GetById(4).Data.Id);

        }

        private static void RentalTest(RentalManager rentalManager)
        {
            rentalManager.Add(new Rental { CarId = 2, CustomerId = 1, RentDate = DateTime.Today});

            var secondTry = rentalManager.Add(new Rental { CarId = 2, CustomerId = 3, RentDate = new DateTime(2021, 02, 12),});
            Console.WriteLine(secondTry.Message);

            rentalManager.Delete(new Rental { Id = 1 });
        }

        private static void GetCarDetail(CarManager carManager)
        {
            var data = carManager.GetCarDetail(1);

            Console.WriteLine(data.Data.CarName +
                " / " + data.Data.BrandName +
                " / " + data.Data.ColorName +
                " / " + data.Data.DailyPrice + "$");
        }

        private static void GetAllCarDetails(CarManager carManager)
        {
            foreach (var car in carManager.GetAllCarDetails().Data)
            {
                Console.WriteLine(car.CarName + " / " + car.BrandName + " / " + car.ColorName + " / " + car.DailyPrice + "$");
            }
        }

        private static void AddColor(ColorManager colorManager)
        {
            colorManager.Add(new Color { Id = 1, Name = "Black" });
            colorManager.Add(new Color { Id = 2, Name = "White" });
            colorManager.Add(new Color { Id = 3, Name = "Grey" });
            colorManager.Add(new Color { Id = 4, Name = "Blue" });
            colorManager.Add(new Color { Name = "Green" });
        }

        private static void AddBrand(BrandManager brandManager)
        {
            brandManager.Add(new Brand { Id = 1, Name = "Volkswagen" });
            brandManager.Add(new Brand { Id = 2, Name = "BMW" });
            brandManager.Add(new Brand { Id = 3, Name = "Audi" });
            brandManager.Add(new Brand { Id = 4, Name = "Opel" });
            brandManager.Add(new Brand { Name = "Honda" });
        }

        private static void AddCar(CarManager carManager)
        {
            carManager.Add(new Car { Id = 1, BrandId = 2, ColorId = 2, DailyPrice = 120, ModelYear = 2010, Name = "BMW M3" });
            carManager.Add(new Car { Id = 2, BrandId = 1, ColorId = 1, DailyPrice = 450, ModelYear = 1999, Name = "Volkswagen Polo" });
            carManager.Add(new Car { Id = 3, BrandId = 1, ColorId = 2, DailyPrice = 530, ModelYear = 2010, Name = "Volkswagen Golf" });
            carManager.Add(new Car { Id = 4, BrandId = 3, ColorId = 3, DailyPrice = 250, ModelYear = 2017, Name = "Audi A7" });
            carManager.Add(new Car { Id = 5, BrandId = 2, ColorId = 4, DailyPrice = 512, ModelYear = 2004, Name = "BMW 320d" });
            carManager.Add(new Car { Id = 6, BrandId = 2, ColorId = 3, DailyPrice = 457, ModelYear = 2020, Name = "BMW I8" });
            carManager.Add(new Car { Id = 7, BrandId = 3, ColorId = 4, DailyPrice = 859, ModelYear = 1970, Name = "Audi Q5" });
            carManager.Add(new Car { Id = 8, BrandId = 4, ColorId = 1, DailyPrice = 524, ModelYear = 2003, Name = "Opel Astra" });
            carManager.Add(new Car { BrandId = 5, ColorId = 5, DailyPrice = 700, ModelYear = 2010, Name = "Honda Civic" });
        }
    }
}
