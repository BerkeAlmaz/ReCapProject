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
            var result = _rentalDal.Get(r => r.CarId == entity.CarId);
            if (result == null)
            {
                _rentalDal.Add(entity);
                return new SuccessResult(Messages.Added);
            }
            return new ErrorResult(Messages.Error);


        }

        public IResult Delete(Rental entity)
        {
            if (entity.ReturnDate != null) //araba kiralanmışsa
            {
                _rentalDal.Delete(entity);
                return new SuccessResult(Messages.Deleted);
            }
            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            var result = _rentalDal.GetAll();
            return new SuccessDataResult<List<Rental>>(result);
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
