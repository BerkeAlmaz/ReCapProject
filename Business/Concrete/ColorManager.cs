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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color entity)
        {
            if (entity.Name.Length >= 2)
            {
                _colorDal.Add(entity);
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new SuccessResult(Messages.NameError2Letter);
            }
        }

        public IResult Delete(Color entity)
        {
            _colorDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Color>> GetAll()
        {
            var result = _colorDal.GetAll();
            return new SuccessDataResult<List<Color>>(result);
        }

        public IDataResult<Color> GetById(int id)
        {
            var result = _colorDal.Get(p => p.Id == id);
            return new SuccessDataResult<Color>(result);
        }

        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
