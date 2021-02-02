using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
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
            //business codes
            _carDal.Add(car);
        }

        public void Delete(Car car)
        {
            //business codes
            _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetByBrandId(int id)
        {
            //business codes
            return _carDal.GetByBrandId(id);
        }

        public void Update(Car car)
        {
            //business codes
            _carDal.Update(car);
        }
    }
}
