using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Quiz.Classes
{
    public class QuizResult
    {
        public QuizGame CompletedQuiz { get; set; } = new();
        public uint Grade { get; set; } = 0;
        public uint Wrong { get; set; } = 0;
        public uint Correct { get; set; } = 0;

        public QuizResult() { }
        public QuizResult(QuizGame quiz)
        {
            CompletedQuiz = quiz;
        }
    }
}
