using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult Add(Car car)
        {
            if (car.Name.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new ErrorResult(Messages.NameError2Letter);
            }
        }

        public IResult Delete(Car car)
        {
            //business codes
            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            //business codes
            var result = _carDal.GetAll();
            return new SuccessDataResult<List<Car>>(result);
        }

        public IDataResult<List<Car>> GetAllByBrandId(int brandId)
        {
            var result = _carDal.GetAll(p => p.BrandId == brandId);
            return new SuccessDataResult<List<Car>>(result);
        }

        public IDataResult<List<Car>> GetAllByColorId(int colorId)
        {
            var result = _carDal.GetAll(p => p.ColorId == colorId);
            return new SuccessDataResult<List<Car>>(result);
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetails()
        {
            var result = _carDal.GetAllCarDetails();
            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(p => p.Id == id);
            return new SuccessDataResult<Car>(result);
        }

        public IDataResult<CarDetailDto> GetCarDetail(int id)
        {
            var result =_carDal.GetCarDetail(id);
            return new SuccessDataResult<CarDetailDto>(result);
        }

        public IResult Update(Car car)
        {
            //business codes
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }
    }
}
