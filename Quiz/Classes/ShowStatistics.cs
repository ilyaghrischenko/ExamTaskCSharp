using Quiz.Enums;
using System.Text.Json;
using static System.Console;

namespace Quiz.Classes
{
    public class ShowStatistics
    {
        private QuizSection Section { get; set; } = QuizSection.Mathematics;
        private string Path { get; set; } = string.Empty;
        public List<StatisticsInfo>? Results { get; set; } = new();

        public ShowStatistics() { }
        public ShowStatistics(QuizSection section)
        {
            Section = section;
            if (Section == QuizSection.Mathematics)
            {
                Path = "tests/mathematics/statistics.json";
            }
            else if (Section == QuizSection.Biology)
            {
                Path = "tests/biology/statistics.json";
            }
            else if (Section == QuizSection.Geography)
            {
                Path = "tests/geography/statistics.json";
            }
            else Path = "tests/mixed/statistics.json";

            Results = GetStatisticsFromFile();
        }

        private List<StatisticsInfo>? GetStatisticsFromFile()
        {
            if (!File.Exists(Path) || new FileInfo(Path).Length == 0) return new();
            return JsonSerializer.Deserialize<List<StatisticsInfo>>(File.ReadAllText(Path));
        }

        public void Show()
        {
            if (Results.Count == 0) WriteLine("No statistics");
            WriteLine(this);
        }

        public override string ToString()
        {
            string text = "";
            foreach (var item in Results)
            {
                text += $"{item.Key}:\n{item.Value}\n";
            }
            return text;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var other = (ShowStatistics)obj;
            return ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
