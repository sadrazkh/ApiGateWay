using System;
using System.Linq;

namespace Common.Utilities
{
    public static class RandomHelper
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Generate a random string 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="useNumbers"></param>
        /// <param name="useLowerCaseAlphabeticalChars"></param>
        /// <param name="useUpperCaseAlphabeticalChars"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int length, bool useNumbers = true, bool useLowerCaseAlphabeticalChars = false, bool useUpperCaseAlphabeticalChars = false)
        {
            string chars = null;

            if (useNumbers)
                chars = "0123456789";

            if (useLowerCaseAlphabeticalChars)
                chars += "abcdefghijklmnopqrstuvwxyz";

            if (useUpperCaseAlphabeticalChars)
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (chars == null || length <= 0)
                return string.Empty;

            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}