using System.Text.Json;
using static System.Console;

namespace Quiz.Classes
{
    public enum QuizSection
    {
        Mathematics,
        Geography,
        Biology
    }

    public class QuizGame
    {
        public QuizSection Section { get; set; } = QuizSection.Mathematics;
        public Dictionary<string, string> QuestionAnswers { get; private set; } = new();
        private List<char> Answers { get; set; } = new();
        private string TestPath { get; set; } = "tests/mathematics/1/test.json";
        private string AnswersPath { get; set; } = "tests/mathematics/1/answers.json";

        public QuizGame() { }
        public QuizGame(QuizSection section)
        {
            Section = section;

            int random = new Random().Next(1, 2);
            if (Section == QuizSection.Mathematics)
            {
                TestPath = $"tests/mathematics/{random}/test.json";
                AnswersPath = $"tests/mathematics/{random}/answers.json";
            }
            else if (Section == QuizSection.Geography)
            {
                TestPath = $"tests/geography/{random}/test.json";
                AnswersPath = $"tests/geography/{random}/answers.json";
            }
            else
            {
                TestPath = $"tests/biology/{random}/test.json";
                AnswersPath = $"tests/biology/{random}/answers.json";
            }

            try
            {
                QuestionAnswers = GetQuestionsFromFile();
                Answers = GetAnswersFromFile();
            }
            catch (FileLoadException ex)
            {
                throw new FileLoadException(ex.Message);
            }
        }

        private Dictionary<string, string> GetQuestionsFromFile()
        {
            if (!File.Exists(TestPath) || new FileInfo(TestPath).Length == 0) throw new FileLoadException("Error: File does not exist or it is empty");
            return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(TestPath));
        }
        private List<char> GetAnswersFromFile()
        {
            if (!File.Exists(AnswersPath) || new FileInfo(AnswersPath).Length == 0) throw new FileLoadException("Error: File does not exist or it is empty");
            return JsonSerializer.Deserialize<List<char>>(File.ReadAllText(AnswersPath));
        }

        public void StartQuiz(User user)
        {
            WriteLine("Quiz(Max grade - 100):");

            char answer;
            uint number = 1;
            int answersIndex = 0;
            uint grade = 0;
            uint correct = 0;
            uint wrong = 0;
            foreach (var item in QuestionAnswers)
            {
                Write($"{number++}. {item.Key}\n{item.Value}\n:");
                if (!char.TryParse(ReadLine(), out answer))
                {
                    throw new Exception("Error: Invalid input");
                }
                if (answer == Answers[answersIndex++])
                {
                    grade += 5;
                    ++correct;
                    WriteLine("Correct answer!");
                }
                else
                {
                    WriteLine("Incorrect answer!");
                    ++wrong;
                }
                WriteLine($"Current grade: {grade}\n");
                ReadKey();
            }

            QuizResult quizResult = new(this);
            quizResult.Grade = grade;
            quizResult.Wrong = wrong;
            quizResult.Correct = correct;
            user.Results.Add(quizResult);
        }

        public override string ToString()
        {
            string text = $"{Section}:\n";
            foreach (var item in QuestionAnswers)
            {
                text += item.Key + "\n" + item.Value + "\n\n";
            }
            return text;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;

            var other = (QuizGame)obj;
            return ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
