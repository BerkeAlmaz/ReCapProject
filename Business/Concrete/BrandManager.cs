using Business.Abstract;
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

        public void Add(Brand entity)
        {
            if (entity.Name.Length >= 2)
            {
                _brandDal.Add(entity);
            }
            else
            {
                throw new Exception("Color name must be at least 2 letters");
            }
        }

        public void Delete(Brand entity)
        {
            _brandDal.Delete(entity);
        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public Brand GetById(int id)
        {
            return _brandDal.Get(p => p.Id == id);
        }

        public void Update(Brand entity)
        {
            _brandDal.Update(entity);
        }
    }
}
