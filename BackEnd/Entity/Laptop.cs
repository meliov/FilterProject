using System;

namespace BackEnd.Entity
{
    public class Laptop
    {
        public int Id { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Processor { get; set; }
            public int RAM { get; set; }
            public int Storage { get; set; }
            public double Price { get; set; }
            public DateTime ReleaseDate { get; set; }
    }
}