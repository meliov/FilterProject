using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Entity;

namespace BackEnd.db
{
    public class CarsRepository
    {
        private DatabaseContext<Car> dbContext = new DatabaseContext<Car>();
        public List<Car> getCars()
        {
            return dbContext.Entities.ToList();
        }

        public List<String> getCarsBrands()
        {
            return  getCars().Select(car => car.Make + " " + car.Model).ToList();
        }
    }
}