using System;

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
    }
}