using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Common
{
    public static class StringHelper
    {
        public static string camelCase(string input)
        {
            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
