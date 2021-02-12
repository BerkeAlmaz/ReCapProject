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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer entity)
        {
            _customerDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Customer entity)
        {
            _customerDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(c => c.Id == id);
            return new SuccessDataResult<Customer>(result);
        }

        public IResult Update(Customer entity)
        {
            _customerDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
