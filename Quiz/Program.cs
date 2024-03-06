using Quiz.Classes;
using System.Net.Security;
using static System.Console;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
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
