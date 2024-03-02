using static System.Console;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Write("Login: ");
                string? login = ReadLine();
                Write("Pass: ");
                string? pass = ReadLine();
                User user = AuthorisationRegistration.Authorisation(login, pass);
            }
            catch (ArgumentException ex)
            {
                WriteLine(ex.Message);
            }
            WriteLine();
            ReadKey();
        }
    }
}
