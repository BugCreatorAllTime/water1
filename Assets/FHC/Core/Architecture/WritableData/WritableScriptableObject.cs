using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.WritableData
{
    [ExecuteInEditMode]
    public class WritableScriptableObject<T> : ScriptableObject, IWritableData<T>, IWritableScriptableObjectHelper, IInspectorCommandObject where T : new()
    {
        [SerializeField, ReadOnly]
        string key;
        [SerializeField, ReadOnly]
        string standAloneKey;
        [SerializeField, ReadOnly]
        FileFormat fileFormat = FileFormat.Binary;
        [SerializeField, ReadOnly]
        string password = "";
        [SerializeField, ReadOnly]
        bool encryptOnStandalone = false;
        [SerializeField, ReadOnly]
        bool encryptOnMobile = false;
        [SerializeField, ReadOnly]
        bool encryptOnEditor = false;

        [Space]
        [SerializeField]
        protected T currentData;
        [SerializeField, InspectorCommand()]
        int saveCurrentData;

        [Space]
        [SerializeField]
        protected T defaultData = new T();

        [NonSerialized]
        bool loadedData = false;

        public bool UseEncryption
        {
            get
            {
#if UNITY_STANDALONE

#if UNITY_EDITOR
                return encryptOnStandalone || encryptOnEditor;
#else
                return encryptOnStandalone;
#endif

#else

#if UNITY_EDITOR
                return encryptOnMobile || encryptOnEditor;
#else
                return encryptOnMobile;
#endif

#endif
            }
        }

        #region IWritableData<T>
        public T Data
        {
            get
            {
                if (!loadedData)
                {
                    LoadData();
                    loadedData = true;
                }

                return currentData;
            }

            set
            {
#if UNITY_EDITOR
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
#endif
                currentData = value;
            }
        }
        public virtual void SaveData()
        {
            WritableDataManagerProvider.GetManager().SaveData(Key, currentData, fileFormat, UseEncryption, password);
        }
        #endregion

        public T DefaultData
        {
            get
            {
                return defaultData;
            }
        }

        public string Key
        {
            get
            {
#if UNITY_IOS || UNITY_ANDROID
                return key;
#else
                return standAloneKey;
#endif
            }
        }
        
        protected void LoadData()
        {
            IWritableDataManager manager = WritableDataManagerProvider.GetManager();
            if (manager.ContainsKey(Key))
            { 
                
                currentData = manager.LoadData<T>(Key, fileFormat, UseEncryption, password);
            }
            else
            {
                currentData = BinarySerializationHelper.Clone(defaultData);
            }
            
            OnDataLoaded(currentData);
        }

        string GetAutoKey()
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.AssetPathToGUID(UnityEditor.AssetDatabase.GetAssetPath(this));
#else
            throw new System.NotImplementedException();
#endif
        }

        [ContextMenu("SetAutoKey")]
        protected void SetAutoKey()
        {
#if UNITY_EDITOR
            key = GetAutoKey();
            standAloneKey = key + "_PC";
#endif
        }

        protected void CopyDefaultDataToCurrentData()
        {
            currentData = BinarySerializationHelper.Clone(defaultData);
        }

        protected virtual void OnDataLoaded(T data) { }

        #region MonoB

        public virtual void OnValidate()
        {
            ///
            SetAutoKey();

            ///
            key = key.Trim();
            standAloneKey = standAloneKey.Trim();
            Assert.IsFalse(string.IsNullOrEmpty(Key));
            Assert.IsFalse(string.IsNullOrEmpty(standAloneKey));
        }

        public void Reset()
        {
            SetAutoKey();
        }
        #endregion

        #region Context menu

        [ContextMenu("LoadCurrentData")]
        public void Editor_LoadCurrentData()
        {
            LoadData();
            FHLog.Log("Loaded from " + Key);
        }

        [ContextMenu("SaveCurrentData")]
        protected void Editor_SaveCurrentData()
        {
            SaveData();
            FHLog.Log("Saved to " + Key);
        }

        [ContextMenu("CopyDefaultDataToCurrentData")]
        protected void Editor_CopyDefaultDataToCurrentData()
        {
            currentData = BinarySerializationHelper.Clone(defaultData);
        }

        #endregion

        #region IInspectorCommandObject
        void IInspectorCommandObject.ExcuteCommand(int intPara, string stringPara)
        {
            Editor_SaveCurrentData();
        }
        #endregion


    }

}