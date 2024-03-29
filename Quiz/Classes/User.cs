﻿using static System.Console;

namespace Quiz.Classes
{
    public class User
    {
        private string _login;
        private string _password;
        private DateOnly _birthDate;

        public User() { }
        public User(string login, string password, DateOnly birthDate)
        {
            Login = login;
            Password = password;
            BirthDate = birthDate;
        }
        public User(string login, string password, DateOnly birthDate, List<QuizResult> results)
        {
            Login = login;
            Password = password;
            BirthDate = birthDate;
            Results = results;
        }

        public string Login
        {
            get => _login;
            set
            {
                if (value == Password) throw new ArgumentException("Error: Login and password can`t be equal");
                if (AuthorisationRegistration.IsValid(value, "login")) _login = value;
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value == Login) throw new ArgumentException("Error: Password and login can`t be equal");
                if (AuthorisationRegistration.IsValid(value, "password")) _password = value;
            }
        }
        public DateOnly BirthDate
        {
            get => _birthDate;
            set
            {
                if (value == new DateOnly()) throw new ArgumentException("Error: Empty value for birth date");
                if (value.Year < 1934) throw new ArgumentException("Error: Invalid year for birth date");
                _birthDate = value;
            }
        }
        public List<QuizResult> Results { get; set; } = new(); 

        public void Show()
        {
            WriteLine($"\n{this}");
        }
        public void Input()
        {
            try
            {
                Write("Login: ");
                string? value = ReadLine();
                if (AuthorisationRegistration.IsValid(value, "login"))
                {
                    Login = value;
                }

                Write("Password: ");
                value = ReadLine();
                if (AuthorisationRegistration.IsValid(value, "password"))
                {
                    Password = value;
                }

                Write("Birth date: ");
                if (!DateOnly.TryParse(ReadLine(), out _birthDate))
                {
                    throw new Exception("Error: Invalid value for birth date");
                }

                for (int i = 0; i < AuthorisationRegistration.RegisteredUsers.Count; ++i)
                {
                    if (AuthorisationRegistration.RegisteredUsers[i].Equals(this))
                    {
                        AuthorisationRegistration.RegisteredUsers[i] = new(Login, Password, BirthDate);
                    }
                }
                AuthorisationRegistration.Save();
            }
            catch (ArgumentException ex)
            {
                WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            string result = $"Login: {Login}, Birth date: {BirthDate}\nResults: ";

            if (Results.Count == 0) result += "No completed quizes";
            else
            {
                result += "\n";
                uint number = 1;
                foreach (var item in Results)
                {
                    result += $"{number++}. Section: {item.Section}, Grade: {item.Grade}, Correct: {item.Correct}, Wrong: {item.Wrong}\n";
                }
            }
            return result;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var other = (User)obj;
            return ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}