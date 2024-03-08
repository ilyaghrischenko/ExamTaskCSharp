using Quiz.Enums;
using System.Text.Json;
using static System.Console;

namespace Quiz.Classes
{
    public class QuizStatistics
    {
        private QuizSection Section { get; set; } = QuizSection.Mathematics;
        private string Path { get; set; } = string.Empty;
        private Dictionary<string, List<QuizResult>>? Results { get; set; } = new();

        public QuizStatistics() { }
        public QuizStatistics(QuizSection section)
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

        private Dictionary<string, List<QuizResult>>? GetStatisticsFromFile()
        {
            if (!File.Exists(Path) || new FileInfo(Path).Length == 0) return new();
            return JsonSerializer.Deserialize<Dictionary<string, List<QuizResult>>>(File.ReadAllText(Path));
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
                text += $"{item.Key}:\n";
                foreach (var value in item.Value)
                {
                    text += $"{value}\n";
                }
                text += "\n";
            }
            return text;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var other = (QuizStatistics)obj;
            return ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
