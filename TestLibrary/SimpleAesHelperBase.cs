using System.Security.Cryptography;
using System.Text;

namespace TestLibrary
{
    public static class SimpleAesHelperBase
    {

        public static string Decrypt(string cipherText, string secretKey)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                byte[] iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;
                using (var ms = new MemoryStream(fullCipher, 16, fullCipher.Length - 16))
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs)) return sr.ReadToEnd();
            }
        }
        public static string Encrypt(string plainText, string secretKey)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs)) sw.Write(plainText);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}