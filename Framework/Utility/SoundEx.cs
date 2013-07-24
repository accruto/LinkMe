using System;

namespace LinkMe.Framework.Utility
{
    /// <summary>
    /// SoundEx algorithm implementation consistent with SQL Server. SQL Server only ignores adjacent
    /// duplicated phonetic sounds and it doesn't ignore a character if it is duplicated with the leading
    /// chararacter. For example, SQL Server will encode "PPPP" as "P100", whereas Miracode will
    /// encode it as "P000".
    /// 
    /// Based on http://www.codeproject.com/cs/algorithms/soundex.asp but corrected and optimised.
    /// </summary>
    public static class SoundEx
    {
        public const string NoSoundEx = "0000";

        private const char SkipChar = '\0';

        /// SQL Server ignores wowels and characters 'h','u','w' following first
        /// character in the word.
        /// It replaces non-handled symbols with zeros ('0'),
        /// it also returns '0000' for the empty string and empty string for the
        /// Null.
        /// Everyting that follows first unhandled character is replaced by '0'
        /// PLEASE NOTE: Nulls are not handled by this algorithm
        ///
        /// SQL Server only ignores adjacent duplicated phonetic sounds
        /// Plus, it doesn't ignore a character if it is duplicated with the leading
        /// char
        ///
        /// For example, SQL Server will encode "PPPP" as "P100", whereas Miracode
        /// will
        /// encode it as "P000".
        public static string GenerateSoundEx(string s)
        {
            const int totalLength = 4;

            if (s == null)
                throw new ArgumentNullException("s");

            if (s.Length == 0 || !char.IsLetter(s[0]))
                return NoSoundEx;

            char[] soundEx = new char[totalLength];
            soundEx[0] = char.ToUpper(s[0]);

            // Stop at a maximum of 4 characters

            char last = SkipChar;
            int length = s.Length;
            int isx = 1;

            for (int i = 1; i < length && isx < totalLength; i++)
            {
                char c = EncodeChar(s[i]);

                // Ignore duplicated chars, except a duplication with the first char

                if (c != SkipChar && c != last)
                {
                    soundEx[isx++] = c;
                }

                last = c;

                // Ignore all chars after first unhandled.

                if (c == '0')
                    break;
            }

            // Pad with zeros.

            while (isx < totalLength)
            {
                soundEx[isx++] = '0';
            }

            return new string(soundEx);
        }

        private static char EncodeChar(char c)
        {
            switch (c)
            {
                case 'b':
                case 'B':
                case 'f':
                case 'F':
                case 'p':
                case 'P':
                case 'v':
                case 'V':
                    return '1';

                case 'c':
                case 'C':
                case 'g':
                case 'G':
                case 'j':
                case 'J':
                case 'k':
                case 'K':
                case 'q':
                case 'Q':
                case 's':
                case 'S':
                case 'x':
                case 'X':
                case 'z':
                case 'Z':
                    return '2';

                case 'd':
                case 'D':
                case 't':
                case 'T':
                    return '3';

                case 'l':
                case 'L':
                    return '4';

                case 'm':
                case 'M':
                case 'n':
                case 'N':
                    return '5';

                case 'r':
                case 'R':
                    return '6';

                case 'a':
                case 'A':
                case 'e':
                case 'E':
                case 'i':
                case 'I':
                case 'o':
                case 'O':
                case 'u':
                case 'U':
                case 'h':
                case 'H':
                case 'y':
                case 'Y':
                case 'w':
                case 'W':
                    return SkipChar; // SQL Sever ignores those chars

                default:
                    return '0'; // SQL Server replaces all non-handled chars with '0'
            }
        }
    }
}
