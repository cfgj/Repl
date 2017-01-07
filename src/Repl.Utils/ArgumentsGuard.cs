using System;
using System.Diagnostics;

namespace Repl.Utils
{
    [DebuggerStepThrough]
    public static class ArgumentsGuard
    {
        public static void ThrowIfNull(object value, string parameterName)
        {
            ThrowIfNull(value, parameterName, null);
        }

        public static void ThrowIfNull(object value, string parameterName, string message)
        {
            if (value == null)
            {
                if (!string.IsNullOrEmpty(message))
                    throw new ArgumentNullException(message, parameterName);
                else
                    throw new ArgumentNullException(parameterName);
            }
        }
    }
}
