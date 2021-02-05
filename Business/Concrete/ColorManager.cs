using Business.Abstract;
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

        public void Add(Color entity)
        {
            if (entity.Name.Length >= 2)
            {
                _colorDal.Add(entity);
            }
            else
            {
                throw new Exception("Color name must be at least 2 letters");
            }
        }

        public void Delete(Color entity)
        {
            _colorDal.Delete(entity);
        }

        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public Color GetById(int id)
        {
            return _colorDal.Get(p => p.Id == id);
        }

        public void Update(Color entity)
        {
            _colorDal.Update(entity);
        }
    }
}
