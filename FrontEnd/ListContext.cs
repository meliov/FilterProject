using System;
using System.Collections.Generic;
using BackEnd.db;

namespace FrontEnd
{
    public class ListContext
    {
        private List<String> cars;

        public List<string> Cars
        {
            get => cars;
            set => cars = value;
        }

        public ListContext()
        {
            CarsRepository carsRepository = new CarsRepository();
            Cars = carsRepository.getCarsBrands();
        }
    }
}