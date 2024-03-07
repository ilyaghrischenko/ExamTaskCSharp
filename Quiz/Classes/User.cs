using static System.Console;

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
            set
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
        public List<QuizResult> Results { get; set; } = new(); 

        public void Show()
        {
            WriteLine(this);
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
            return $"Login: {Login}, Password: {Password}, Birth date: {BirthDate}";
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
