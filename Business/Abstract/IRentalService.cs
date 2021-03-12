using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental entity);
        IResult Update(Rental entity);
        IResult Delete(Rental entity);
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int id);
        IDataResult<Rental> GetByCarIdCurrent(int id);
        IDataResult<List<RentalDetailDto>> GetAllRentalDetails();
        IDataResult<RentalDetailDto> GetRentalDetail(int id);
    }
}
