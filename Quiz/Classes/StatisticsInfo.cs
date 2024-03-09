using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Quiz.Classes
{
    public class StatisticsInfo
    {
        public uint Position { get; set; } = 0;
        public string Login { get; set; } = string.Empty;
        public QuizResult Result { get; set; } = new();

        public StatisticsInfo() { }
        public StatisticsInfo(uint position, string login, QuizResult result)
        {
            Position = position;
            Login = login;
            Result = result;
        }

        public void Show()
        {
            WriteLine(this);
        }

        public override string ToString()
        {
            return $"Position: {Position}, Login: {Login}, Result: {Result}";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;

            var other = (StatisticsInfo)obj;
            return ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
