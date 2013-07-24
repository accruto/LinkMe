using System.Text;

namespace LinkMe.Utility.Security
{
    public static class RC4
    {
        private const int DefaultLen = 256;

        private static int ord(char ch)
        {
            return (int)(Encoding.GetEncoding(1252).GetBytes(ch + "")[0]);
        }

        // Get character representation of ASCII Code
        //	private static char chr(int i)
        //	{
        //		byte[] bytes = new byte[1];
        //		bytes[0] = (byte)i;
        //		return Encoding.GetEncoding(1252).GetString(bytes)[0];
        //	}

        /// <summary>
        ///  Convert Hex to Binary (hex2bin)
        /// </summary>
        /// <param name="packtype"></param>
        /// <param name="datastring">Hex to be packed into Binary</param>
        public static string hex2bin(string packtype, string datastring)
        {
            int i, j, datalength, packsize;
            byte[] bytes;
            char[] hex;
            string tmp;

            datalength = datastring.Length;
            packsize = (datalength / 2) + (datalength % 2);
            bytes = new byte[packsize];
            hex = new char[2];

            for (i = j = 0; i < datalength; i += 2)
            {
                hex[0] = datastring[i];
                if (datalength - i == 1)
                    hex[1] = '0';
                else
                    hex[1] = datastring[i + 1];
                tmp = new string(hex, 0, 2);
                bytes[j++] = byte.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
            }
            return Encoding.GetEncoding(1252).GetString(bytes);
        }

        /// <summary>
        /// Convert Binary to Hex (bin2hex)
        /// </summary>
        /// <param name="bindata">Binary data</param>
        public static string bin2hex(string bindata)
        {
            int i;
            byte[] bytes = Encoding.GetEncoding(1252).GetBytes(bindata);
            string hexString = string.Empty;
            for (i = 0; i < bytes.Length; i++)
                hexString += bytes[i].ToString("x2");

            return hexString;
        }

        /// <summary>
        /// The symmetric encryption function
        /// </summary>
        /// <param name="pwd">Key to encrypt with (can be binary of hex)</param>
        /// <param name="data">Content to be encrypted</param>
        /// <param name="ispwdHex"> Key passed is in hexadecimal or not</param>
        public static string Encrypt(string pwd, string data, bool ispwdHex)
        {
            int a, i, j, k, tmp, pwd_length, data_length;
            int[] key, box;
            byte[] cipher;
            //string cipher;

            if (ispwdHex)
                pwd = hex2bin("H*", pwd);

            pwd_length = pwd.Length;
            data_length = data.Length;
            key = new int[DefaultLen];
            box = new int[DefaultLen];
            cipher = new byte[data.Length];
            //cipher = "";

            for (i = 0; i < DefaultLen; i++)
            {
                key[i] = ord(pwd[i % pwd_length]);
                box[i] = i;
            }
            for (j = i = 0; i < DefaultLen; i++)
            {
                j = (j + box[i] + key[i]) % DefaultLen;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data_length; i++)
            {
                a = (a + 1) % DefaultLen;
                j = (j + box[a]) % DefaultLen;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[((box[a] + box[j]) % DefaultLen)];
                cipher[i] = (byte)(ord(data[i]) ^ k);
                //cipher += chr(ord(data[i]) ^ k);
            }
            return Encoding.GetEncoding(1252).GetString(cipher);
            //return cipher;
        }

        /// <summary>
        /// Decryption, recall encryption
        /// </summary>
        /// <param name="pwd">Key to decrypt with (can be binary of hex)</param>
        /// <param name="data">Content to be decrypted</param>
        /// <param name="ispwdHex">Key passed is in hexadecimal or not</param>

        public static string Decrypt(string pwd, string data, bool ispwdHex)
        {
            return Encrypt(pwd, data, ispwdHex);
        }
    }
}