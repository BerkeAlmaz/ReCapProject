using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.Name.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
            }
            else
            {
                throw new Exception("Name must be at least 2 letters");
            }
        }

        public void Delete(Car car)
        {
            //business codes
            _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            //business codes
            return _carDal.GetAll();
        }

        public List<Car> GetAllByBrandId(int brandId)
        {
            return _carDal.GetAll(p => p.BrandId == brandId);
        }

        public List<Car> GetAllByColorId(int colorId)
        {
            return _carDal.GetAll(p => p.ColorId == colorId);
        }

        public List<CarDetailDto> GetAllCarDetails()
        {
            return _carDal.GetAllCarDetails();
        }

        public Car GetById(int id)
        {
            return _carDal.Get(p => p.Id == id);
        }

        public CarDetailDto GetCarDetail(int id)
        {
            return _carDal.GetCarDetail(id);
        }

        public void Update(Car car)
        {
            //business codes
            _carDal.Update(car);
        }
    }
}
