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
        public QuizGame ResultQuiz { }
        private uint _grade;
        private uint _wrong;
        private uint _correct;

        public QuizResult(QuizGame quiz)
        {
            _quiz = quiz;
            _grade = _quiz.Grade;
            _correct=_grade
        }
    }
}
