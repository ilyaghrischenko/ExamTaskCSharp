using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Console;

namespace Quiz
{
    public static class AuthorisationRegistration
    {
        private static string _path = "_registeredUsers.json";
        private static List<User>? _registeredUsers = GetRegisteredUsers();

        public static bool IsValid(string value, string message)
        {
            if (value == string.Empty) throw new ArgumentException($"Error: Empty value for {message}");

            int kilkLetters = 0;
            int kilkNumbers = 0;
            foreach (var item in value)
            {
                if (char.IsLetter(item)) ++kilkLetters;
                else if (char.IsDigit(item)) ++kilkNumbers;
                else throw new ArgumentException($"Error: {message} can`t have punctuation or other symbols");
            }

            if (kilkLetters < 8 || kilkNumbers < 3) throw new ArgumentException($"Error: {message} must have at least 8 letters and 3 digits");
            return true;
        }
        private static List<User>? GetRegisteredUsers()
        {
            if (!File.Exists(_path)) return null;
            List<User>? _registeredUsers = JsonSerializer.Deserialize<List<User>>(_path);
            return _registeredUsers;
        }
        private static void Save()
        {
            string jsonString = JsonSerializer.Serialize(_registeredUsers);
            File.WriteAllText(_path, jsonString);
        }

        public static User Authorisation(string login, string password)
        {
            if (login == string.Empty || password == string.Empty) throw new ArgumentException("Error: Empty value for login or password");
            if (login == password) throw new ArgumentException("Error: Login and password can`t be equal");
            try
            {
                if (!IsValid(login, "login") || !IsValid(password, "password")) throw new ArgumentException("Error: Invalid value for login or password");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }

            if (_registeredUsers == null)
            {
                return Registration(login, password);
            }
            foreach (var item in _registeredUsers)
            {
                if (item.Login == login && item.Password == password)
                {
                    Write("Birth date: ");
                    DateOnly birthDate;
                    if (!DateOnly.TryParse(ReadLine(), out birthDate))
                    {
                        throw new ArgumentException("Error: Invalid value for birth date");
                    }
                    return new User(login, password, birthDate);
                }
            }
            return Registration(login, password);
        }
        private static User Registration(string login, string password)
        {
            try
            {
                Write("Birth date: ");
                DateOnly birthDate;
                if (!DateOnly.TryParse(ReadLine(), out birthDate))
                {
                    throw new ArgumentException("Error: Invalid value for birth date");
                }
                Save();
                return new User(login, password, birthDate);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
