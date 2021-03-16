using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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


        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("product.add,admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            //business codes
            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            //business codes
            var result = _carDal.GetAll();
            return new SuccessDataResult<List<Car>>(result);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllByBrandId(int brandId)
        {
            var result = _carDal.GetAll(p => p.BrandId == brandId);
            return new SuccessDataResult<List<Car>>(result);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllByColorId(int colorId)
        {
            var result = _carDal.GetAll(p => p.ColorId == colorId);
            return new SuccessDataResult<List<Car>>(result);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarDetails()
        {
            var result = _carDal.GetAllCarDetails();
            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(p => p.Id == id);
            return new SuccessDataResult<Car>(result);
        }

        [CacheAspect]
        public IDataResult<CarDetailDto> GetCarDetail(int id)
        {
            var result =_carDal.GetCarDetail(id);
            return new SuccessDataResult<CarDetailDto>(result);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            List<CarDetailDto> carDetails = new List<CarDetailDto>();

            var get = _carDal.GetAll(p => p.BrandId == brandId);
            foreach (var item in get)
            {
                var getCarDetail = _carDal.GetCarDetail(item.Id);
                carDetails.Add(getCarDetail);
            }
            return new SuccessDataResult<List<CarDetailDto>>(carDetails);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            List<CarDetailDto> carDetails = new List<CarDetailDto>();

            var get = _carDal.GetAll(p => p.ColorId == colorId);
            foreach (var item in get)
            {
                var getCarDetail = _carDal.GetCarDetail(item.Id);
                carDetails.Add(getCarDetail);
            }
            return new SuccessDataResult<List<CarDetailDto>>(carDetails);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            //business codes
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }
    }
}
