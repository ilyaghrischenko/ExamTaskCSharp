using Quiz.Classes;
using Quiz.Enums;
using static System.Console;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1. Після завершення вікторини користувач отримує
                //своє місце у таблиці результатів гравців вікторини.
                //2. Змінити зберігання результатів у файли statistics.json

                User user = AuthorisationRegistration.Authorisation("SimpleLogin123", "SimplePassword123");
                user.Show();

                var section = QuizSection.Mathematics;

                QuizGame quiz = new(section);
                quiz.StartQuiz(user);

                ShowStatistics statistics = new(section);
                statistics.Show();
            }
            catch (FileLoadException ex)
            {
                WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            WriteLine();
        }
    }
}