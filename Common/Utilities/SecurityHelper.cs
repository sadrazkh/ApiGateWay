using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace Common.Utilities
{
    public static class SecurityHelper
    {
        public static string GetSha256Hash(string input)
        {
            //using (var sha256 = new SHA256CryptoServiceProvider())
            using var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = sha256.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
            //return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
        }

        public static string Encrypt(string input)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("P6.}][NAgN,9jV@&");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("P6.}][NAgN,9jV@&");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// checks that format of phone number is correct or not
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrEmpty(phoneNumber))
                return false;

            if (Regex.IsMatch(phoneNumber, "09\\d{9}") && phoneNumber.Length == 11)
                phoneNumber = "98" + phoneNumber.Substring(1, 10);
            else if (!Regex.IsMatch(phoneNumber, "989\\d{9}"))
                return false;

            return phoneNumber.Length == 12;
        }

        /// <summary>
        /// checks that input email is a valid email address or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// convert email to formats like this : javad*******2000@gmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string MakeEmailUnReadable(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                return email;

            var a = email.Substring(0, email.IndexOf('@'));
            var b = email.Substring(email.IndexOf('@'), email.Length - a.Length);

            var x = a.Length / 4;
            var y = a.Length / 2;


            string temp = null;


            for (int i = 0; i < x; i++)
            {
                temp += a[i];
            }
            for (int i = x; i < x + y; i++)
            {
                temp += '*';
            }
            for (int i = x + y; i < a.Length; i++)
            {
                temp += a[i];
            }

            return temp + b;
        }

        /// <summary>
        /// convert phone number to formats like this : 0919****338
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string MakePhoneNumberUnReadable(string phoneNumber)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(phoneNumber))
                return phoneNumber;

            if (Regex.IsMatch(phoneNumber, "09\\d{9}") && phoneNumber.Length == 11)
            {

                for (int i = 0; i < 4; i++)
                    result += phoneNumber[i];

                for (int i = 4; i < 8; i++)
                    result += "*";

                for (int i = 8; i < 11; i++)
                    result += phoneNumber[i];
            }

            else if (Regex.IsMatch(phoneNumber, "989\\d{9}") && phoneNumber.Length == 12)
            {
                for (int i = 0; i < 5; i++)
                    result += phoneNumber[i];

                for (int i = 4; i < 9; i++)
                    result += "*";

                for (int i = 9; i < 12; i++)
                    result += phoneNumber[i];
            }

            return result;
        }
    }
}
