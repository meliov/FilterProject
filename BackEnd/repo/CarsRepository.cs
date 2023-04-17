using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.db
{
    public class CarsRepository
    {
        public List<Car> getCars()
        {
            return DatabaseContext.SingletonDbContext.Cars.ToList();
        }

        public List<String> getCarsBrands()
        {
            return DatabaseContext.SingletonDbContext.Cars.ToList().Select(car => car.Make + " " + car.Model).ToList();
        }
    }
}