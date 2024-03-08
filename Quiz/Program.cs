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
                User user = AuthorisationRegistration.Authorisation("SimpleLogin123", "SimplePassword123");
                user.Show();

                QuizGame quiz = new(QuizSection.Mathematics);
                quiz.StartQuiz(user);
                user.Show();
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