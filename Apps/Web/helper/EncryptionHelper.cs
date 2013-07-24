using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Utilities;

namespace LinkMe.Web.Helper
{
    /// <summary>
    /// Provides encryption/decryption and keyed hashing using the ASP.NET machine key algorithm and key,
    /// specified in Web.config. Encryption uses the decryption key and hashing uses the validation key.
    /// </summary>
    public static class EncryptionHelper
    {
        private static readonly Encoding encoding = new UTF8Encoding(false, true);

        private static SymmetricAlgorithm encryptionAlgorithm;
        private static HMAC hashAlgorithm;

        private static SymmetricAlgorithm EncryptionAlgorithm
        {
            get
            {
                if (encryptionAlgorithm == null)
                {
                    try
                    {
                        encryptionAlgorithm = GetEncryptionAlgorithm();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Failed to initialise encryption using web application path '"
                            + new ApplicationUrl("~/").Path + "'.", ex);
                    }
                }

                return encryptionAlgorithm;
            }
        }

        private static HMAC HashAlgorithm
        {
            get
            {
                if (hashAlgorithm == null)
                {
                    try
                    {
                        hashAlgorithm = GetHashAlgorithm();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Failed to initialise hash using web application path '"
                            + new ApplicationUrl("~/").Path + "'.", ex);
                    }
                }

                return hashAlgorithm;
            }
        }

        public static string EncryptToHexString(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            return StringUtils.ByteArrayToHexString(EncryptOrDecryptData(true, encoding.GetBytes(data)));
        }

        public static string DecryptFromHexString(string encryptedHex)
        {
            if (encryptedHex == null)
                throw new ArgumentNullException("encryptedHex");

            return encoding.GetString(EncryptOrDecryptData(false, StringUtils.HexStringToByteArray(encryptedHex)));
        }

        public static string GetKeyedHashAsHexString(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            return StringUtils.ByteArrayToHexString(GetKeyedHash(encoding.GetBytes(data)));
        }

        public static bool IsKeyedHashValid(string data, string hashAsHex)
        {
            return IsKeyedHashValid(encoding.GetBytes(data), StringUtils.HexStringToByteArray(hashAsHex));
        }

        public static bool IsKeyedHashValid(byte[] data, byte[] hash)
        {
            return MiscUtils.ByteArraysEqual(GetKeyedHash(data), hash);
        }

        public static byte[] GetKeyedHash(byte[] data)
        {
            return HashAlgorithm.ComputeHash(data);
        }

        public static string GetSha1HashAsHexString(string data)
        {
            return StringUtils.ByteArrayToHexString(GetSha1Hash(data));
        }

        public static byte[] GetSha1Hash(string data)
        {
            return GetSha1Hash(encoding.GetBytes(data));
        }

        public static byte[] GetSha1Hash(byte[] data)
        {
            return SHA1.Create().ComputeHash(data);
        }

        public static byte[] EncryptOrDecryptData(bool encrypt, byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            return EncryptOrDecryptData(encrypt, data, 0, data.Length);
        }

        public static byte[] EncryptOrDecryptData(bool encrypt, byte[] data, int start, int length)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            ICryptoTransform cryptoTransform = (encrypt ? EncryptionAlgorithm.CreateEncryptor() :
                EncryptionAlgorithm.CreateDecryptor());
            MemoryStream stream = new MemoryStream();

            using (CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, start, length);
                cryptoStream.FlushFinalBlock();

                return stream.ToArray();
            }
        }

        private static SymmetricAlgorithm GetEncryptionAlgorithm()
        {
            MachineKeySection machineKey = GetMachineKey();
            string key = machineKey.DecryptionKey;

            CheckKey(key, "decryption");

            SymmetricAlgorithm sa;
            switch (machineKey.Decryption)
            {
                case "3DES":
                    sa = new TripleDESCryptoServiceProvider();
                    break;

                case "DES":
                    sa = new DESCryptoServiceProvider();
                    break;

                case "AES":
                    sa = new RijndaelManaged();
                    break;

                case "Auto":
                    sa = (machineKey.DecryptionKey.Length == 16 ? (SymmetricAlgorithm)new DESCryptoServiceProvider()
                        : new RijndaelManaged());
                    break;

                default:
                    throw new ApplicationException("The machine decryption method specified in"
                        + " Web.config, '" + machineKey.Decryption + "', is invalid.");
            }

            sa.Key = StringUtils.HexStringToByteArray(key);
            sa.GenerateIV();
            sa.IV = new byte[sa.IV.Length];

            return sa;
        }

        private static MachineKeySection GetMachineKey()
        {
            string applicationPath = new ApplicationUrl("~/").Path;
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(applicationPath);
            return (MachineKeySection)config.GetSection("system.web/machineKey");
        }

        private static HMAC GetHashAlgorithm()
        {
            MachineKeySection machineKey = GetMachineKey();

            string key = machineKey.ValidationKey;
            CheckKey(key, "validation");

            return new HMACSHA1(StringUtils.HexStringToByteArray(key));
        }

        private static void CheckKey(string key, string description)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("The " + description + " key must be specified.", "key");
 
            if (key.StartsWith("AutoGenerate"))
            {
                throw new ApplicationException(string.Format("Unable to initialise EncryptionHelper,"
                    + " because the machine {0} key in web.config (under application path '{1}')"
                    + " is set to 'AutoGenerate'.", description, new ApplicationUrl("~/").Path));
            }
        }
    }
}
