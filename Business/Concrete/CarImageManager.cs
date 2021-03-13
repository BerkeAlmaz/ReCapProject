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
using System.Linq;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {

            _carImageDal = carImageDal;
            _carService = carService;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage entity)
        {

            IResult result = BusinessRules.Run(CheckIfImage(file), ChechImageCount(entity));
            if (result != null)
            {
                return result;
            }

            var getPhotos = _carImageDal.Get(c => c.CarId == entity.CarId && 
            c.ImagePath== @"C:\Users\B.ALMAZ\source\repos\ReCapProject\WebAPI\wwwroot\Images\nophoto.jpeg");

            if (getPhotos != null)
            {
                _carImageDal.Delete(getPhotos);
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
            var result = _carService.GetAll();

            foreach (var item in result.Data)
            {
                var control = BusinessRules.Run(CheckIfAnyImageExist(item.Id));
                if (control != null)
                {
                    _carImageDal.Add(new CarImage
                    {
                        CarId = item.Id,
                        Date = DateTime.Now,
                        ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\nophoto.jpeg"
                    });
                }
            }

            var test = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(test);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            var result = BusinessRules.Run(CheckIfAnyImageExist(id)); 

            if (result != null)
            {
                _carImageDal.Add(new CarImage
                {
                    CarId = id,
                    Date = DateTime.Now,
                    ImagePath = Environment.CurrentDirectory + @"\wwwroot\Images\nophoto.jpg"
                });
            }

            var test = _carImageDal.GetAll(c => c.CarId == id);
            return new SuccessDataResult<List<CarImage>>(test);

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

        private IResult CheckIfAnyImageExist(int carId)
        {
            var getImages = _carImageDal.GetAll(c => c.CarId == carId);
            if (getImages.Count==0)
            {
                return new ErrorResult();
            }
            return new SuccessResult();

        }

        
    }
}
