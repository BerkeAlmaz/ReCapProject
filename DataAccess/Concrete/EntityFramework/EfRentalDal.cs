using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, DatabaseContext>, IRentalDal
    {
        public List<RentalDetailDto> GetAllRentalDetails()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from r in context.Rentals
                             join c in context.Customers
                             on r.CustomerId equals c.Id
                             join u in context.Users
                             on c.UserId equals u.Id
                             join ca in context.Cars
                             on r.CarId equals ca.Id
                             select new RentalDetailDto
                             {
                                 CarName = ca.Name,
                                 CompanyName = c.CompanyName,
                                 CustomerFirstName = u.FirstName,
                                 CustomerLastName = u.LastName,
                                 RentalId = r.Id,
                                 RentDate = r.RentDate
                             };
                return result.ToList();
            }
        }

        public RentalDetailDto GetRentalDetail(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from r in context.Rentals
                             join c in context.Customers
                             on r.CustomerId equals c.Id
                             join u in context.Users
                             on c.UserId equals u.Id
                             join ca in context.Cars
                             on r.CarId equals ca.Id
                             select new RentalDetailDto
                             {
                                 CarName = ca.Name,
                                 CompanyName = c.CompanyName,
                                 CustomerFirstName = u.FirstName,
                                 CustomerLastName = u.LastName,
                                 RentalId = r.Id,
                                 RentDate = r.RentDate
                             };
                return result.FirstOrDefault(r => r.RentalId == id);

            }
            
        }
    }
}
