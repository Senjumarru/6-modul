using System;
using System.Collections.Generic;
using System.IO;

namespace WeatherReportBuilder
{
    public enum Format
    {
        Text, Html, Json
    }

    public interface IReportBuilder
    {
        void SetHeader(string header);
        void SetContent(string content);
        void AddSection(string sectionName, string sectionContent);
        Report GetReport();
    }

    public class TextReportBuilder : IReportBuilder
    {
        private Report report = new Report();

        public void SetHeader(string header)
        {
            report.Header = header + "\n";
        }
        public void SetContent(string content)
        {
            report.Content = content + "\n";
        }
        public void AddSection(string sectionName, string sectionContent)
        {
            report.Sections.Add($"{sectionName}: {sectionContent}\n");
        }
        public Report GetReport()
        {
            return report;
        }
    }

    public class HtmlReportBuilder : IReportBuilder
    {
        private Report report = new Report();

        public void SetHeader(string header)
        {
            report.Header = $"<h1>{header}</h1>\n";
        }
        public void SetContent(string content)
        {
            report.Content = $"<p>{content}</p>\n";
        }
        public void AddSection(string sectionName, string sectionContent)
        {
            report.Sections.Add($"<h3>{sectionName}</h3><p>{sectionContent}</p>\n");
        }
        public Report GetReport()
        {
            return report;
        }
    }

    public class JsonReportBuilder : IReportBuilder
    {
        private Report report = new Report();

        public void SetHeader(string header)
        {
            report.Header = $"\"header\": \"{header}\"";
        }

        public void SetContent(string content)
        {
            report.Content = $"\"content\": \"{content}\"";
        }

        public void AddSection(string sectionName, string sectionContent)
        {
            report.Sections.Add($"\"{sectionName}\": \"{sectionContent}\"");
        }

        public Report GetReport()
        {
            return report;
        }
    }

    public class Report
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public List<string> Sections { get; set; } = new List<string>();

        public void Export(string fileName, Format format)
        {
            string output = $"{Header}\n{Content}\n{string.Join("\n", Sections)}";

            File.WriteAllText(fileName, output);
        }
    }

    public class ReportDirector
    {
        private IReportBuilder _reportBuilder;

        public ReportDirector(IReportBuilder reportBuilder)
        {
            _reportBuilder = reportBuilder;
        }

        public void BuildReport(string header, string content, List<(string sectionName, string sectionContent)> sections)
        {
            _reportBuilder.SetHeader(header);
            _reportBuilder.SetContent(content);

            foreach (var section in sections)
            {
                _reportBuilder.AddSection(section.sectionName, section.sectionContent);
            }
        }

        public Report GetReport()
        {
            return _reportBuilder.GetReport();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sections = new List<(string sectionName, string sectionContent)>
            {
                ("Температура", "15°C"),
                ("Влажность", "60%"),
                ("Ветер", "5 м/с")
            };

            // Пример текстового отчета
            IReportBuilder textBuilder = new TextReportBuilder();
            ReportDirector director = new ReportDirector(textBuilder);
            director.BuildReport("Отчет о погоде", "Сегодня солнечно", sections);
            Report textReport = director.GetReport();
            textReport.Export("weather_report.txt", Format.Text);

            // Пример HTML отчета
            IReportBuilder htmlBuilder = new HtmlReportBuilder();
            director = new ReportDirector(htmlBuilder);
            director.BuildReport("Отчет о погоде", "Сегодня солнечно", sections);
            Report htmlReport = director.GetReport();
            htmlReport.Export("weather_report.html", Format.Html);

            // Пример JSON отчета
            IReportBuilder jsonBuilder = new JsonReportBuilder();
            director = new ReportDirector(jsonBuilder);
            director.BuildReport("Отчет о погоде", "Сегодня солнечно", sections);
            Report jsonReport = director.GetReport();
            jsonReport.Export("weather_report.json", Format.Json);
        }
    }
}
