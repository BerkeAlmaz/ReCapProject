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
            var results = _rentalDal.GetAll(p => p.CarId == entity.CarId);

            foreach (var item in results)
            {
                if (item != null)
                {
                    var date = DateTime.Compare(entity.ReturnDate, item.ReturnDate); // soldaki tarih sağdakinden geçmişteyse değeri 0'dan küçüktür
                    if (date < 0)
                    {
                        return new ErrorResult(Messages.Error);
                    }
                }
            }
            _rentalDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Rental entity)
        {
            
            TimeSpan span = entity.RentDate.Subtract(DateTime.Now);
            if (span.Minutes > 0)
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
