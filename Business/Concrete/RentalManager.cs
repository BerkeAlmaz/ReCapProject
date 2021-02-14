using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
 
        public IResult Add(Rental entity)
        {
            var test = _rentalDal.GetAll(p => p.CarId == entity.CarId);
            if (test.Count == 0)
            {
                _rentalDal.Add(entity);
                return new SuccessResult(Messages.Added);
            }
            foreach (var item in test)
            {
                if (item.ReturnDate != new DateTime(0001,01,01))
                {
                    _rentalDal.Add(entity);
                    return new SuccessResult(Messages.Added);
                }
            }
            return new ErrorResult(Messages.Error);

        }

        public IResult Delete(Rental entity)
        {

            _rentalDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
     
        }

        public IDataResult<List<Rental>> GetAll()
        {
            List<Rental> result = _rentalDal.GetAll();
            return new SuccessDataResult<List<Rental>>(result);
        }

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

        public IDataResult<Rental> GetById(int id)
        {
            var result = _rentalDal.Get(r => r.Id == id);
            return new SuccessDataResult<Rental>(result);
        }

        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
