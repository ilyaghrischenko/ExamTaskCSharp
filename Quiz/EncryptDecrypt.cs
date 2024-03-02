namespace Quiz
{
    public static class EncryptDecrypt
    {
        public static string Encrypt(string text, int key)
        {
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (char)(text[i] ^ key);
            }
            return new string(result);
        }
        public static string Decrypt(string text, int key)
        {
            return Encrypt(text, key);
        }
    }
}
