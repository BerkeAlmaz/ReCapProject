using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, DatabaseContext>, ICarDal
    {
        public List<CarDetailDto> GetAllCarDetails()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             select new CarDetailDto 
                             { 
                                 CarName = c.Name, BrandName = b.Name, ColorName = co.Name, DailyPrice = c.DailyPrice,
                                 CarId = c.Id
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetail(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             select new CarDetailDto
                             {
                                 CarName = c.Name,
                                 BrandName = b.Name,
                                 ColorName = co.Name,
                                 DailyPrice = c.DailyPrice,
                                 CarId = c.Id
                             };
                return result.FirstOrDefault(p => p.CarId == id);
            }
        }
    }
}
