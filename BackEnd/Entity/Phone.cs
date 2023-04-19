using System;

namespace BackEnd.Entity
{
    public class Phone : Entity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Storage { get; set; }
        public int RAM { get; set; }
        public double Price { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime ReleaseDate { get; set; }
        
        public override string ToString()
        {
            return Brand + " " + Model + " - " + Price;
        }
    }
    
}