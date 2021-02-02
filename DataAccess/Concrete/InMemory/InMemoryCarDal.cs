using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car { Id=1, BrandId=2, ColorId=3, DailyPrice=300,  ModelYear=2015, Description="Sahibinden satılık"},
                new Car { Id=2, BrandId=1, ColorId=3, DailyPrice=50,  ModelYear=2010, Description="Temiz" },
                new Car { Id=3, BrandId=4, ColorId=1, DailyPrice=100,  ModelYear=2011, Description="Kirli" },
                new Car { Id=4, BrandId=2, ColorId=4, DailyPrice=200,  ModelYear=2004, Description="Acil doktordan" }
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var delete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(delete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetByBrandId(int id)
        {
            return _cars.Where(c => c.BrandId == id).ToList();
        }

        public void Update(Car car)
        {
            var update = _cars.SingleOrDefault(c => c.Id == car.Id);
            car.Id = update.Id;
            car.ModelYear = update.ModelYear;
            car.ColorId = update.ColorId;
            car.BrandId = update.BrandId;
            car.DailyPrice = update.DailyPrice;
            car.Description = update.Description;
        }
    }
}
