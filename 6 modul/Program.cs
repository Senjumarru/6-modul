using System;
using System.Collections.Generic;
using System.Linq;
using WeatherReportBuilder;

namespace CarPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------------------------
            // Тестирование шаблона прототипа
            // --------------------------
            // Создание оригинальной машины
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

            // Изменение клонированной машины
            clonedCar.Model = "Tesla Model 3";  // Изменяем модель
            clonedCar.Engine.HorsePower = 480;   // Изменяем мощность двигателя
            clonedCar.Features.Add(new Feature("Камера заднего вида"));  // Добавляем новую особенность

            // Вывод данных клонированной машины после изменений
            Console.WriteLine("\nКлонированная машина (после изменений):");
            clonedCar.ShowCarDetails();

            // Проверка оригинальной машины на неизменность
            Console.WriteLine("\nОригинальная машина после изменения клона:");
            originalCar.ShowCarDetails();

            // --------------------------
            // Тестирование шаблона строителя
            // --------------------------
            var reportBuilder = new TextReportBuilder();
            var reportDirector = new ReportDirector(reportBuilder);

            var sections = new List<(string sectionName, string sectionContent)>
            {
                ("Введение", "Это пример отчета о машинах."),
                ("Заключение", "Все машины являются уникальными.")
            };

            reportDirector.BuildReport("Отчет о машинах", "Этот отчет содержит информацию о различных автомобилях.", "Конец отчета", new ReportStyle { BackgroundColor = "white", FontColor = "black", FontSize = 12 }, sections);

            // Получение отчета
            var report = reportDirector.GetReport();
            Console.WriteLine("\nСформированный отчет:");
            Console.WriteLine(report.Header);
            Console.WriteLine(report.Content);
            foreach (var section in report.Sections)
            {
                Console.WriteLine(section);
            }
            Console.WriteLine(report.Footer);

            Console.ReadLine();
        }
    }
}
