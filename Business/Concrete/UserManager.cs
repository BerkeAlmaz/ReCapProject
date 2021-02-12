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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User entity)
        {
            if (entity.FirstName.Length > 0 && entity.LastName.Length > 0)
            {
                _userDal.Add(entity);
                return new SuccessResult();
            }
            return new ErrorResult();
            
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            return new SuccessDataResult<User>(result);
        }

        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult();
        }
    }
}
