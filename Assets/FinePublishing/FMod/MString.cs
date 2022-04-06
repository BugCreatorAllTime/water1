using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace FMod
{
    [System.Serializable]
    public class MString
    {
        [SerializeField]
        string iOS;
        [SerializeField]
        string android;
        [SerializeField]
        string defaultValue = "";

        [Header("Encryption")]
        [SerializeField]
        bool useEncryption = false;
        [SerializeField]
        string originalIOS;
        [SerializeField]
        string originalAndroid;
        [SerializeField]
        string originalDefaultValue = "";

        private string decryptedString = "";
        bool isDecrypted = false;
        static string encryptionKey = null;

        public static implicit operator string(MString ms)
        {
            return ms.GetString();
        }

        static string GetEncryptionKey()
        {
            int k = 543;

            if (encryptionKey == null)
            {
                encryptionKey = (4236).ToString();
                encryptionKey += "iOS@";
                encryptionKey += 6543;
                encryptionKey += "spin";
                encryptionKey += (k + 5285).ToString();
                encryptionKey += "mine";
            }

            ///
            return encryptionKey;
        }

        public void TrimAllValues()
        {
            if (!useEncryption)
            {
                iOS = iOS.Trim();
                android = android.Trim();
                defaultValue = defaultValue.Trim();
            }
        }

        public string GetString()
        {
            // Decrypted
            if (useEncryption && isDecrypted)
            {
                return decryptedString;
            }

            ///
            string rs;
#if UNITY_IOS
            rs = iOS;
#elif UNITY_ANDROID
            rs = android;
#else
			rs = defaultValue;
#endif

            ///
            if (!useEncryption)
            {
                return rs;
            }

            ///
            string originalRs;
#if UNITY_IOS
            originalRs = originalIOS;
#elif UNITY_ANDROID
            originalRs = originalAndroid;
#else
			originalRs = originalDefaultValue;
#endif
            ///
            decryptedString = Decrypt(rs);
            if (decryptedString == originalRs)
            {
                isDecrypted = true;
                return decryptedString;
            }
            else
            {
                throw new System.Exception("Invalid string");
            }
        }

        public string Decrypt(string encryptedString)
        {
            byte[] inputArray = System.Convert.FromBase64String(encryptedString);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(GetEncryptionKey());
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string Encrypt(string originalString)
        {
#if UNITY_EDITOR
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(originalString);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(GetEncryptionKey());
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return System.Convert.ToBase64String(resultArray, 0, resultArray.Length);
#else
            throw new System.NotImplementedException();
#endif
        }

        public void TryFillEncryptionFields()
        {
            if (useEncryption)
            {
                iOS = Encrypt(originalIOS);
                android = Encrypt(originalAndroid);
                defaultValue = Encrypt(originalDefaultValue);
            }
        }

        public bool VerifyEncryption()
        {
            ///
            if (!useEncryption)
            {
                return true;
            }

            ///
            if (!VerifyEncryption(iOS, originalIOS))
            {
                Debug.Log("iOS encryption failed");
                return false;
            }

            ///
            if (!VerifyEncryption(android, originalAndroid))
            {
                Debug.Log("Android encryption failed");
                return false;
            }

            ///
            if (!VerifyEncryption(defaultValue, originalDefaultValue))
            {
                Debug.Log("DefaultValue encryption failed");
                return false;
            }

            ///
            return true;
        }

        bool VerifyEncryption(string encryptedString, string originalString)
        {
            try
            {
                return Decrypt(encryptedString) == originalString;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }

}