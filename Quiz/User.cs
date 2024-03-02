using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Quiz
{
    public class User
    {
        private string _login;
        private string _password;
        private DateOnly _birthDate;
        private int _encodeKey = new Random().Next(1, 10);

        public User() { }
        public User(string login, string password, DateOnly birthDate)
        {
            Login = login;
            Password = password;
            BirthDate = birthDate;
        }

        public string Login
        {
            get => _login;
            private set
            {
                try
                {
                    if (value == Password) throw new ArgumentException("Error: Login and password can`t be equal");
                    if (AuthorisationRegistration.IsValid(value, "login")) _login = value;
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }
        public string Password
        {
            get => _password;
            private set
            {
                try
                {
                    if (value == Login) throw new ArgumentException("Error: Password and login can`t be equal");
                    if (AuthorisationRegistration.IsValid(value, "password")) _password = value;
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
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

    }
}
