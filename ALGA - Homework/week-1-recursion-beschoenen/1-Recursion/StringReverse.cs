using System;

namespace ALGA
{
    public class StringReverse
    {
        public static String string_reverse(String s)
        {
            if (s.Length == 0) return s;

            var last = s.Substring(s.Length - 1, 1);

            if (s.Length == 1)
            {
                return last;
            }

            return last + string_reverse(s.Substring(0, s.Length - 1));
        }

        public static bool is_palindrome(String s)
        {
            if (s.Length <= 1) return true;

            if (s.Substring(0, 1) == s.Substring(s.Length - 1, 1))
            {
                return is_palindrome(s.Substring(1, s.Length - 2));
            }

            return false;
        }
    }
}
