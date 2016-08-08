using System;
using System.Text.RegularExpressions;

namespace MessageBird.Utilities
{
    public class ParameterValidator
    {
        public static void IsNotNullOrWhiteSpace(string param, string paramName)
        {
            if (String.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentException("Invalid string parameter, cannot be null, empty, or contain only whitespace", paramName);
            }
        }

        public static void IsNotNull(object param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ContainsAtLeast<T>(T [] param, int n, string paramName)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n");
            }

            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < n)
            {
                throw new ArgumentException(String.Format("The array contains {0} elements, but at least {1} were expected", param.Length, n), paramName);  
            }
        }

        public static void IsValidOriginator(string originator)
        {
            if (!string.IsNullOrEmpty(originator))
            {
                var numeric = new Regex("^\\+?[0-9]+$");
                var alphanumericWithWhitespace = new Regex("^[A-Za-z0-9]+(?:\\s[A-Za-z0-9]+)*$");
                var isNumeric = numeric.IsMatch(originator);
                var isAlphanumericWithWhitespace = alphanumericWithWhitespace.IsMatch(originator);

                if (!isNumeric && !isAlphanumericWithWhitespace) 
                {
                    throw new ArgumentException("Originator can only contain numeric or whitespace separated alphanumeric characters.");
                } else if (isAlphanumericWithWhitespace && originator.Length > 11)
                {
                    throw new ArgumentException("Alphanumeric originator is limited to 11 characters.");
                }
            }
        }
    }
}