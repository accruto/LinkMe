using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Utility.Utilities;

namespace LinkMe.InstallUtil
{
    internal static class HashPasswordsUtil
    {
        public static void HashPasswords(string[] args)
        {
            if (args.Length < 2)
            {
                Program.Usage();
                return;
            }

            Console.WriteLine("Plaintext\tHash (base64)\t\t\tHash (hex)");
            Console.WriteLine();

            for (int i = 1; i < args.Length; i++)
            {
                string plainText = args[i];
                string base64 = args[i];
                byte[] bytes;

                if (plainText.EndsWith("=="))
                {
                    // The input is actually a base64 hash, not plaintext.
                    bytes = Convert.FromBase64String(args[i]);
                    plainText = "????????";
                }
                else
                {
                    bytes = LoginCredentials.HashToBytes(args[i]);
                    base64 = LoginCredentials.HashToString(args[i]);
                }

                Console.WriteLine("{0}\t{1}\t{2}", plainText, base64, StringUtils.ByteArrayToHexString(bytes));
            }

            Console.WriteLine();
            Console.WriteLine("You can check the strength by entering the hex value at http://passcracking.com/");
        }
    }
}
