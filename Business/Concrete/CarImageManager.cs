using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {

            _carImageDal = carImageDal;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage entity)
        {

            IResult result = BusinessRules.Run(CheckIfImage(file), ChechImageCount(entity));
            if (result != null)
            {
                return result;
            }

            entity.ImagePath = ImageFileHelper.UploadFile(file);
            entity.Date = DateTime.Now;

            _carImageDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage entity)
        {
            var result = _carImageDal.Get(c => c.Id == entity.Id);
            ImageFileHelper.DeleteFile(result.ImagePath);

            _carImageDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(IFormFile file, CarImage entity)
        {
            var result = _carImageDal.Get(c => c.Id == entity.Id);

            entity.ImagePath = ImageFileHelper.UpdateFile(result.ImagePath,file);
            entity.Date = DateTime.Now;

            _carImageDal.Update(entity);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(result);
        }

        private IResult CheckIfImage(IFormFile file)
        {
            string[] extensions = new string[] { ".jpg", ".jpeg", ".png" };
            string extension = Path.GetExtension(file.FileName).ToLower();

            foreach (var ext in extensions)
            {
                if (extension == ext)
                {
                    return new SuccessResult();
                }
            }

            return new ErrorResult();
        }

        private IResult ChechImageCount(CarImage entity)
        {
            var result =_carImageDal.GetAll(c => c.CarId == entity.CarId);
            if (result.Count>=5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
