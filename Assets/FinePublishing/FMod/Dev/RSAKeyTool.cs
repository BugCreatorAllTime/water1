#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace FMod
{
    public class RSAKeyTool : MonoBehaviour
    {
        [SerializeField]
        int keySize = 512;

        [ContextMenu("GenerateAndExport")]
        public void GenerateAndExport()
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(keySize))
            {
                //Export the key information to an RSAParameters object.
                //Pass false to export the public key information or pass
                //true to export public and private key information.
                Debug.LogFormat("Public key: {0}", RSA.ToXmlString(false));
                Debug.LogFormat("Public and private key: {0}", RSA.ToXmlString(true));
            }
        }

    }

}
#endif