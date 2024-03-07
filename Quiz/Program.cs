using Quiz.Classes;
using static System.Console;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                User user = AuthorisationRegistration.Authorisation("IllyaHryshchenko711626", "IlyaHRYSH711626");
                QuizGame quiz = new(QuizSection.Mathematics);
                quiz.StartQuiz(user);
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
            ReadKey();
        }
    }
}
