using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BackEnd.db;
using BackEnd.Entity;

namespace BackEnd
{
    internal class Program
    {
        private static List<Car> cars = new List<Car>()
        {
            new Car { Make = "Toyota", Model = "Camry", Year = 2018, Color = "Red", Mileage = 25000, Price = 22000.00 },
            new Car
            {
                Make = "Honda", Model = "Accord", Year = 2019, Color = "White", Mileage = 18000, Price = 24000.00
            },
            new Car
            {
                Make = "Ford", Model = "Mustang", Year = 2020, Color = "Black", Mileage = 15000, Price = 35000.00
            },
            new Car
            {
                Make = "Chevrolet", Model = "Camaro", Year = 2017, Color = "Yellow", Mileage = 30000, Price = 28000.00
            },
            new Car
            {
                Make = "Nissan", Model = "Altima", Year = 2016, Color = "Blue", Mileage = 40000, Price = 18000.00
            },
            new Car
            {
                Make = "Tesla", Model = "Model S", Year = 2022, Color = "Gray", Mileage = 1000, Price = 75000.00
            },
            new Car { Make = "BMW", Model = "X5", Year = 2019, Color = "Silver", Mileage = 12000, Price = 45000.00 },
            new Car
            {
                Make = "Mercedes-Benz", Model = "C-Class", Year = 2020, Color = "White", Mileage = 8000,
                Price = 52000.00
            },
            new Car { Make = "Audi", Model = "A4", Year = 2021, Color = "Red", Mileage = 5000, Price = 42000.00 },
            new Car
            {
                Make = "Lamborghini", Model = "Huracan", Year = 2018, Color = "Orange", Mileage = 5000,
                Price = 220000.00
            }
        };

        private static List<Laptop> laptops = new List<Laptop>()
        {
            new Laptop
            {
                Brand = "Dell",
                Model = "XPS 13",
                Processor = "Intel Core i5",
                RAM = 8,
                Storage = 256,
                Price = 999.99,
                ReleaseDate = new DateTime(2021, 1, 15)
            },
            new Laptop
            {
                Brand = "HP",
                Model = "Spectre x360",
                Processor = "Intel Core i7",
                RAM = 16,
                Storage = 512,
                Price = 1499.99,
                ReleaseDate = new DateTime(2020, 9, 30)
            },
            new Laptop
            {
                Brand = "Apple",
                Model = "MacBook Air",
                Processor = "Apple M1",
                RAM = 8,
                Storage = 256,
                Price = 999.99,
                ReleaseDate = new DateTime(2020, 11, 17)
            },
            new Laptop
            {
                Brand = "Lenovo",
                Model = "ThinkPad X1 Carbon",
                Processor = "Intel Core i5",
                RAM = 16,
                Storage = 512,
                Price = 1299.99,
                ReleaseDate = new DateTime(2021, 3, 1)
            },
            new Laptop
            {
                Brand = "ASUS",
                Model = "ROG Zephyrus G14",
                Processor = "AMD Ryzen 9",
                RAM = 16,
                Storage = 1_000,
                Price = 1499.99,
                ReleaseDate = new DateTime(2020, 4, 1)
            },
            new Laptop
            {
                Brand = "Acer",
                Model = "Swift 3",
                Processor = "AMD Ryzen 7",
                RAM = 8,
                Storage = 512,
                Price = 799.99,
                ReleaseDate = new DateTime(2021, 2, 15)
            },
            new Laptop
            {
                Brand = "Microsoft",
                Model = "Surface Laptop 4",
                Processor = "AMD Ryzen 7",
                RAM = 16,
                Storage = 512,
                Price = 1699.99,
                ReleaseDate = new DateTime(2021, 4, 15)
            },
            new Laptop
            {
                Brand = "Razer",
                Model = "Blade Stealth 13",
                Processor = "Intel Core i7",
                RAM = 16,
                Storage = 512,
                Price = 1599.99,
                ReleaseDate = new DateTime(2020, 10, 1)
            },
            new Laptop
            {
                Brand = "Samsung",
                Model = "Galaxy Book Pro",
                Processor = "Intel Core i7",
                RAM = 16,
                Storage = 512,
                Price = 1299.99,
                ReleaseDate = new DateTime(2021, 5, 14)
            },
            new Laptop
            {
                Brand = "LG",
                Model = "Gram 17",
                Processor = "Intel Core i7",
                RAM = 16,
                Storage = 1_000,
                Price = 1499.99,
                ReleaseDate = new DateTime(2021, 6, 1)
            }
        };

        private static List<Phone> phones = new List<Phone>
        {
            new Phone
            {
                Brand = "Apple", Model = "iPhone 13", Storage = 128, RAM = 6, Price = 999.99,
                OperatingSystem = "iOS 15", ReleaseDate = new DateTime(2021, 9, 14)
            },
            new Phone
            {
                Brand = "Samsung", Model = "Galaxy S21", Storage = 256, RAM = 8, Price = 799.99,
                OperatingSystem = "Android 11", ReleaseDate = new DateTime(2021, 1, 29)
            },
            new Phone
            {
                Brand = "Google", Model = "Pixel 5", Storage = 128, RAM = 8, Price = 699.99,
                OperatingSystem = "Android 11", ReleaseDate = new DateTime(2020, 10, 29)
            },
            new Phone
            {
                Brand = "OnePlus", Model = "9 Pro", Storage = 256, RAM = 12, Price = 969.00,
                OperatingSystem = "OxygenOS 11", ReleaseDate = new DateTime(2021, 3, 23)
            },
            new Phone
            {
                Brand = "Xiaomi", Model = "Mi 11", Storage = 256, RAM = 8, Price = 749.00, OperatingSystem = "MIUI 12",
                ReleaseDate = new DateTime(2021, 2, 1)
            },
            new Phone
            {
                Brand = "Motorola", Model = "Edge 20 Pro", Storage = 256, RAM = 12, Price = 699.99,
                OperatingSystem = "Android 11", ReleaseDate = new DateTime(2021, 8, 17)
            },
            new Phone
            {
                Brand = "Huawei", Model = "P40 Pro", Storage = 256, RAM = 8, Price = 799.00,
                OperatingSystem = "EMUI 10.1", ReleaseDate = new DateTime(2020, 4, 7)
            },
            new Phone
            {
                Brand = "Sony", Model = "Xperia 1 III", Storage = 256, RAM = 12, Price = 1299.99,
                OperatingSystem = "Android 11", ReleaseDate = new DateTime(2021, 8, 19)
            },
            new Phone
            {
                Brand = "LG", Model = "Wing 5G", Storage = 256, RAM = 8, Price = 999.99, OperatingSystem = "Android 10",
                ReleaseDate = new DateTime(2020, 10, 15)
            },
            new Phone
            {
                Brand = "Nokia", Model = "8.3 5G", Storage = 128, RAM = 8, Price = 499.99,
                OperatingSystem = "Android 10", ReleaseDate = new DateTime(2020, 9, 22)
            }
        };

        private static List<VideoGame> videoGames = new List<VideoGame>()
        {
            new VideoGame()
            {
                Title = "The Legend of Zelda: Breath of the Wild", Genre = "Action-adventure",
                Platform = "Nintendo Switch", Publisher = "Nintendo", ReleaseDate = new DateTime(2017, 3, 3),
                Rating = 97, Price = 59.99
            },
            new VideoGame()
            {
                Title = "Red Dead Redemption 2", Genre = "Action-adventure", Platform = "PlayStation 4",
                Publisher = "Rockstar Games", ReleaseDate = new DateTime(2018, 10, 26), Rating = 97, Price = 59.99
            },
            new VideoGame()
            {
                Title = "The Witcher 3: Wild Hunt", Genre = "Action role-playing", Platform = "Xbox One",
                Publisher = "CD Projekt", ReleaseDate = new DateTime(2015, 5, 19), Rating = 94, Price = 29.99
            },
            new VideoGame()
            {
                Title = "Uncharted 4: A Thief's End", Genre = "Action-adventure", Platform = "PlayStation 4",
                Publisher = "Sony Computer Entertainment", ReleaseDate = new DateTime(2016, 5, 10), Rating = 93,
                Price = 19.99
            },
            new VideoGame()
            {
                Title = "Horizon Zero Dawn", Genre = "Action role-playing", Platform = "PlayStation 4",
                Publisher = "Sony Interactive Entertainment", ReleaseDate = new DateTime(2017, 2, 28), Rating = 89,
                Price = 19.99
            },
            new VideoGame()
            {
                Title = "Overwatch", Genre = "First-person shooter", Platform = "PC",
                Publisher = "Blizzard Entertainment", ReleaseDate = new DateTime(2016, 5, 24), Rating = 90,
                Price = 39.99
            },
            new VideoGame()
            {
                Title = "Super Smash Bros. Ultimate", Genre = "Fighting", Platform = "Nintendo Switch",
                Publisher = "Nintendo", ReleaseDate = new DateTime(2018, 12, 7), Rating = 93, Price = 59.99
            },
            new VideoGame()
            {
                Title = "Minecraft", Genre = "Sandbox", Platform = "PC", Publisher = "Mojang Studios",
                ReleaseDate = new DateTime(2011, 11, 18), Rating = 93, Price = 26.95
            },
            new VideoGame()
            {
                Title = "Resident Evil Village", Genre = "Survival horror", Platform = "PlayStation 5",
                Publisher = "Capcom", ReleaseDate = new DateTime(2021, 5, 7), Rating = 84, Price = 59.99
            },
            new VideoGame()
            {
                Title = "Assassin's Creed Valhalla", Genre = "Action role-playing", Platform = "Xbox Series X/S",
                Publisher = "Ubisoft", ReleaseDate = new DateTime(2020, 11, 10), Rating = 83, Price = 59.99
            },
        };

        public static void Main(string[] args)
        {
            Service service = new Service();
           // service.getAllClassesNames();
           //service.getSelectedClassFields("Laptop");
        }
        
        private static void populateDb()
        {
            Console.Write("in populateDb");

            foreach (var car in cars)
            {
                DatabaseContext.SingletonDbContext.Cars.Add(car);
            }
            foreach (var laptop in laptops)
            {
                DatabaseContext.SingletonDbContext.Laptops.Add(laptop);
            }
            foreach (var phone in phones)
            {
                DatabaseContext.SingletonDbContext.Phones.Add(phone);
            }
            foreach (var videoGame in videoGames)
            {
                DatabaseContext.SingletonDbContext.VideoGames.Add(videoGame);
            }

            DatabaseContext.SingletonDbContext.SaveChanges();
        }
    }
}