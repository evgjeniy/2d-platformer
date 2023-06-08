using System.Collections.Generic;
using Agava.YandexGames;
using Newtonsoft.Json;
using UnityEngine;

namespace Utils
{
    public static class YandexCloudSaveData
    {
        private static Dictionary<string, string> _saveDictionary = null;

        public static void Save(string key, string value)
        {
            CheckSaveDictionary();

            _saveDictionary[key] = value;

            SaveJsonToCloud();
        }

        public static string Get(string key, string defaultValue = null)
        {
            CheckSaveDictionary();

            return _saveDictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static void Delete(string key) => _saveDictionary.Remove(key);

        private static void SaveJsonToCloud()
        {
            var jsonString = JsonConvert.SerializeObject(_saveDictionary);

#if !UNITY_WEBGL || UNITY_EDITOR
            PlayerPrefs.SetString("SaveDataDictionary", jsonString);
            PlayerPrefs.Save();
#else
            PlayerAccount.SetCloudSaveData(jsonString);
#endif
        }

        private static void CheckSaveDictionary()
        {
            if (_saveDictionary != null) return;

#if !UNITY_WEBGL || UNITY_EDITOR
            var jsonSaveDictionary = PlayerPrefs.GetString("SaveDataDictionary", null);
            _saveDictionary = jsonSaveDictionary == null
                ? new Dictionary<string, string>()
                : JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSaveDictionary);
#else
            PlayerAccount.GetCloudSaveData(jsonCloudData =>
            {
                _saveDictionary = jsonCloudData == default 
                    ? new Dictionary<string, string>() 
                    : JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCloudData);
            }, onErrorCallback: _ => _saveDictionary = new Dictionary<string, string>());
#endif
        }

#if UNITY_EDITOR
        public static void DeleteAll()
        {
            if (_saveDictionary != null)
            {
                _saveDictionary.Clear();
                _saveDictionary = null;
            }

            Debug.Log(_saveDictionary);
        }
#endif
    }
}