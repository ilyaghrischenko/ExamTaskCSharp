using Quiz.Classes;
using Quiz.Enums;
using static System.Collections.Specialized.BitVector32;
using static System.Console;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                User user = AuthorisationRegistration.Authorisation("SimpleLogin123", "SimplePassword123");
                user.Show();

                var section = QuizSection.Mathematics;

                QuizGame quiz = new(section);
                quiz.StartQuiz(user);

                QuizStatistics statistics = new(section);
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