using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{    
    public sealed class ScriptableSingleton : ScriptableObject
    {
        #region Singleton
        const string assetName = "ScriptableSingleton_ASSET";
        static ScriptableSingleton instance = null;
        public static ScriptableSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = LoadAsset();
                    if (instance == null)
                    {
                        instance = CreateAsset();
                    }

                    (instance as ScriptableSingleton).Initialize();
                }

                return instance;
            }
        }

        private void Initialize()
        {
            throw new NotImplementedException();
        }

        static ScriptableSingleton CreateAsset()
        {
#if UNITY_EDITOR
            ScriptableSingleton scriptableSingleton = CreateInstance<ScriptableSingleton>();
            string assetPath = ApplicationConstances.ApplicationDataAssetResourcePath + "/" + assetName + ".asset";
            UnityEditor.AssetDatabase.CreateAsset(scriptableSingleton, assetPath);
            return scriptableSingleton as ScriptableSingleton;
#else
            return null;
#endif
        }

        static ScriptableSingleton LoadAsset()
        {
            string assetPath = ApplicationConstances.ApplicationDataResourcePath + "/" + assetName;
            return Resources.Load<ScriptableSingleton>(assetPath) as ScriptableSingleton;
        }
        #endregion

        #region Editor menu
#if UNITY_EDITOR
        //[UnityEditor.MenuItem("FH/Application data/ScriptableSingleton_ASSET")]
        static void ShowInInspector()
        {
            UnityEditor.Selection.activeObject = Instance as ScriptableSingleton;
        }
#endif
        #endregion

    }

}