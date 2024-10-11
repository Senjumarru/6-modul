using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPrototype
{
    // Обобщённый интерфейс для клонирования
    public interface ICloneable<T>
    {
        T Clone();
    }

    // Класс машины
    public class Car : ICloneable<Car>
    {
        public string Model { get; set; }        // Модель
        public Engine Engine { get; set; }       // Двигатель
        public List<Wheel> Wheels { get; set; }  // Колеса
        public List<Feature> Features { get; set; } // Особенности

        public Car() { }

        public Car(string model, Engine engine, List<Wheel> wheels, List<Feature> features)
        {
            Model = model;
            Engine = engine;
            Wheels = wheels;
            Features = features;
        }

        // Метод глубокого копирования машины
        public Car Clone()
        {
            return new Car(
                this.Model,
                this.Engine.Clone(),
                this.Wheels.Select(wheel => wheel.Clone()).ToList(),
                this.Features.Select(feature => feature.Clone()).ToList()
            );
        }

        public void ShowCarDetails()
        {
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Двигатель: {Engine.Type} с мощностью {Engine.HorsePower} л.с.");
            Console.WriteLine("Колеса:");
            foreach (var wheel in Wheels)
            {
                Console.WriteLine($"  {wheel.Type} - Размер: {wheel.Size}");
            }
            Console.WriteLine("Особенности:");
            foreach (var feature in Features)
            {
                Console.WriteLine($"  {feature.Description}");
            }
        }
    }

    // Класс двигателя
    public class Engine : ICloneable<Engine>
    {
        public string Type { get; set; }         // Тип двигателя
        public int HorsePower { get; set; }      // Мощность в лошадиных силах

        public Engine() { }

        public Engine(string type, int horsePower)
        {
            Type = type;
            HorsePower = horsePower;
        }

        // Метод глубокого копирования двигателя
        public Engine Clone()
        {
            return new Engine(this.Type, this.HorsePower);
        }
    }

    // Класс колеса
    public class Wheel : ICloneable<Wheel>
    {
        public string Type { get; set; }         // Тип колеса
        public int Size { get; set; }            // Размер колеса

        public Wheel() { }

        public Wheel(string type, int size)
        {
            Type = type;
            Size = size;
        }

        // Метод глубокого копирования колеса
        public Wheel Clone()
        {
            return new Wheel(this.Type, this.Size);
        }
    }

    // Класс особенностей автомобиля
    public class Feature : ICloneable<Feature>
    {
        public string Description { get; set; }  // Описание особенности

        public Feature() { }

        public Feature(string description)
        {
            Description = description;
        }

        // Метод глубокого копирования особенностей
        public Feature Clone()
        {
            return new Feature(this.Description);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Оригинальная машина
            Car originalCar = new Car(
                "Tesla Model S",
                new Engine("Электрический", 670),
                new List<Wheel>
                {
                    new Wheel("Спортивные", 19),
                    new Wheel("Спортивные", 19),
                    new Wheel("Спортивные", 19),
                    new Wheel("Спортивные", 19)
                },
                new List<Feature>
                {
                    new Feature("Автопилот"),
                    new Feature("Панорамная крыша")
                }
            );

            // Клонирование машины
            Car clonedCar = originalCar.Clone();

            // Вывод данных оригинальной машины
            Console.WriteLine("Оригинальная машина:");
            originalCar.ShowCarDetails();

            // Вывод данных клонированной машины
            Console.WriteLine("\nКлонированная машина:");
            clonedCar.ShowCarDetails();

            Console.ReadLine();
        }
    }
}
