using System.Text.Json;
using static System.Console;

namespace Quiz.Classes
{
    public static class AuthorisationRegistration
    {
        private const string Path = "registeredUsers.json";
        public static List<User>? RegisteredUsers { get; set; } = GetRegisteredUsers();

        public static bool IsValid(string value, string message)
        {
            if (value == string.Empty) throw new ArgumentException($"Error: Empty value for {message}");

            int kilkLetters = 0;
            int kilkNumbers = 0;
            int kilkUpper = 0;
            foreach (var item in value)
            {
                if (char.IsUpper(item)) ++kilkUpper;
                if (char.IsLetter(item)) ++kilkLetters;
                else if (char.IsDigit(item)) ++kilkNumbers;
            }

            if (kilkLetters < 8 || kilkNumbers < 3 || kilkUpper < 1) throw new ArgumentException($"Error: {message} must have at least 8 letters, 1 capital letter and 3 digits");
            return true;
        }
        private static List<User>? GetRegisteredUsers()
        {
            if (!File.Exists(Path)) return new();

            string jsonContent = File.ReadAllText(Path);
            if (jsonContent == string.Empty) return new();

            List<User>? RegisteredUsers = JsonSerializer.Deserialize<List<User>>(jsonContent);
            return RegisteredUsers;
        }
        public static void Save()
        {
            string jsonString = JsonSerializer.Serialize(RegisteredUsers);
            File.WriteAllText(Path, jsonString);
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

            if (RegisteredUsers.Count == 0) return Registration(login, password);
            foreach (var item in RegisteredUsers)
            {
                if (item.Login == login && item.Password == password)
                {
                    if (item.Results.Count == 0) return new User(login, password, item.BirthDate);
                    return new User(login, password, item.BirthDate, item.Results);
                }
            }
            return Registration(login, password);
        }
        private static User Registration(string login, string password)
        {
            try
            {
                foreach (var item in RegisteredUsers)
                {
                    if (item.Login == login) throw new ArgumentException("Error: This login already exists");
                }

                Write("Birth date: ");
                DateOnly birthDate;
                if (!DateOnly.TryParse(ReadLine(), out birthDate))
                {
                    throw new ArgumentException("Error: Invalid value for birth date");
                }

                User user = new(login, password, birthDate);
                RegisteredUsers.Add(user);
                Save();
                return user;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}