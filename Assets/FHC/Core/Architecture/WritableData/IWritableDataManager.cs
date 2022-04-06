using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.WritableData
{
    public interface IWritableDataManager
    {
        bool ContainsKey(string key);
        T LoadData<T>(string key, FileFormat fileFormat, bool useEncryption, string password);
        void SaveData<T>(string key, T data, FileFormat fileFormat, bool useEncryption, string password);
        void DeleteData(string key);
        void DeleteAllData();
    }

}