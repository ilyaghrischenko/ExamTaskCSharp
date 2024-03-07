using Quiz.Enums;
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
        public QuizSection Section { get; set; } = QuizSection.Mathematics;
        public uint Grade { get; set; } = 0;
        public uint Wrong { get; set; } = 0;
        public uint Correct { get; set; } = 0;

        public QuizResult() { }
        public QuizResult(QuizSection section, uint grade, uint wrong, uint correct)
        {
            Section = section;
            Grade = grade;
            Wrong = wrong;
            Correct = correct;
        }

        public override string ToString()
        {
            return $"Section: {Section}, Grade: {Grade}, Wrong: {Wrong}, Correct: {Correct}";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var other = (QuizResult)obj;
            return ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
