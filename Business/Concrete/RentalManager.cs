using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental entity)
        {
            _rentalDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            List<Rental> result = _rentalDal.GetAll();
            return new SuccessDataResult<List<Rental>>(result);
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetAllRentalDetails()
        {
            var result = _rentalDal.GetAllRentalDetails();
            return new SuccessDataResult<List<RentalDetailDto>>(result);
        }

        [CacheAspect]
        public IDataResult<Rental> GetByCarIdCurrent(int id)
        {
            var result = _rentalDal.GetAll(p => p.CarId == id);
            if (result.Count > 0 )
            {
                foreach (var item in result)
                {
                    if (item.ReturnDate == new DateTime(0001,01,01))
                    {
                        var data =_rentalDal.Get(p => p.CarId == id && p.ReturnDate == new DateTime(0001, 01, 01));
                        return new SuccessDataResult<Rental>(data);
                    }
                }
            }
            return new ErrorDataResult<Rental>(Messages.Error);
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            var result = _rentalDal.Get(r => r.Id == id);
            return new SuccessDataResult<Rental>(result);
        }

        [CacheAspect]
        public IDataResult<RentalDetailDto> GetRentalDetail(int id)
        {
            var result = _rentalDal.GetRentalDetail(id);
            return new SuccessDataResult<RentalDetailDto>(result);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
