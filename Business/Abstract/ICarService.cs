using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService : IService<Car>
    {
        List<Car> GetAllByColorId(int colorId);
        List<Car> GetAllByBrandId(int brandId);
        List<CarDetailDto> GetAllCarDetails();
        CarDetailDto GetCarDetail(int id);

    }
}
