
namespace Org.Joey.Common
{
    using System;
    public static class Guard
    {
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(string.Format(Strings.InputValueCantBeNull, argumentName));
        }
        public static void ArgementNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentNullException(string.Format(Strings.InputValueCantBeNull, argumentName));
        }

    }
}
