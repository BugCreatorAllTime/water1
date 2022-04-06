using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.WritableData
{
    public static partial class WritableDataManagerProvider
    {
        private class WritableDataManager : IWritableDataManager
        {
            const string KeysListKey = "DatakeyList";

            List<string> keys;

            #region IWritableDataManager
            public bool ContainsKey(string key)
            {
                return keys.Contains(key);
            }

            public T LoadData<T>(string key, FileFormat fileFormat, bool useEncryption, string password)
            {
                ///
                Assert.IsTrue(keys.Contains(key));

                ///
                byte[] bytesData = PersistentFileHelper.LoadFile(GetFileName(key));

                ///
                if (useEncryption)
                {
                    bytesData = SimpleEncryption.Decrypt(bytesData, password);
                }

                ///
                return SerializationHelper.Deserialize<T>(bytesData, fileFormat);
            }

            public void SaveData<T>(string key, T data, FileFormat fileFormat, bool useEncryption, string password)
            {
                ///
                byte[] bytesData = SerializationHelper.Serialize(data, fileFormat);

                ///
                if (useEncryption)
                {
                    bytesData = SimpleEncryption.Encrypt(bytesData, password);
                }

                ///
                PersistentFileHelper.SaveFile(bytesData, GetFileName(key));

                ///
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                    SavekeysList();
                }
            }

            public void DeleteData(string key)
            {
                ///
                string fileName = GetFileName(key);

                // Delete the data file
                if (PersistentFileHelper.FileExist(fileName))
                {
                    try
                    {
                        PersistentFileHelper.DeleteFile(fileName);
                    }
                    catch (Exception)
                    {

                    }
                }

                // Remove from list
                keys.Remove(key);
                SavekeysList();
            }

            public void DeleteAllData()
            {
                keys = new List<string>();
                SavekeysList();
            }
            #endregion

            public WritableDataManager()
            {
                LoadKeysList();
            }

            void LoadKeysList()
            {
                string fileName = GetFileName(KeysListKey);
                if (PersistentFileHelper.FileExist(fileName))
                {
                    byte[] keysData = PersistentFileHelper.LoadFile(fileName);
                    keys = BinarySerializationHelper.Deserialize<List<string>>(keysData);
                }
                else
                {
                    keys = new List<string>();
                }
            }

            void SavekeysList()
            {
                byte[] keysData = BinarySerializationHelper.Serialize(keys);
                PersistentFileHelper.SaveFile(keysData, GetFileName(KeysListKey));
            }

            string GetFileName(string key)
            {
                return string.Format("WritableData_{0}.FH", key);
            }
        }
    }
}