using Business.Abstract;
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

        public IResult Add(IFormFile file, CarImage entity)
        {

            IResult result = BusinessRules.Run(CheckIfImage(file));
            if (result != null)
            {
                return result;
            }

            entity.ImagePath = ImageFileHelper.UploadFile(file);
            entity.Date = DateTime.Now;

            _carImageDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(CarImage entity)
        {
            var result = _carImageDal.Get(c => c.Id == entity.Id);
            ImageFileHelper.DeleteFile(result.ImagePath);

            _carImageDal.Delete(entity);
            return new SuccessResult();
        }

        public IResult Update(IFormFile file, CarImage entity)
        {
            var result = _carImageDal.Get(c => c.Id == entity.Id);

            entity.ImagePath = ImageFileHelper.UpdateFile(result.ImagePath,file);
            entity.Date = DateTime.Now;

            _carImageDal.Update(entity);
            return new SuccessResult();
        }

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
                    return new SuccessDataResult<IFormFile>(file);
                }
            }

            return new ErrorDataResult<IFormFile>();
        }
    }
}
