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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand entity)
        {
            if (entity.Name.Length >= 2)
            {
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new ErrorResult(Messages.NameError2Letter);
            }
        }

        public IResult Delete(Brand entity)
        {
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            var result = _brandDal.GetAll();
            return new SuccessDataResult<List<Brand>>(result);
        }

        public IDataResult<Brand> GetById(int id)
        {
            var result = _brandDal.Get(p => p.Id == id);
            return new SuccessDataResult<Brand>(result);
        }

        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
