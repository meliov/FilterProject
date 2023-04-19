namespace BackEnd.Entity
{
    public class Car : Entity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Make + " " + Model;
        }
    }
}