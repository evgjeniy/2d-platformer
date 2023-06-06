using System.Collections.Generic;
using System.Linq;
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

            var jsonString = JsonConvert.SerializeObject(_saveDictionary);
            
#if !UNITY_WEBGL || UNITY_EDITOR
            var jsonSaveDictionary = JsonConvert.SerializeObject(_saveDictionary);
            PlayerPrefs.SetString("SaveDataDictionary", jsonSaveDictionary);
            PlayerPrefs.Save();
#else
            PlayerAccount.SetCloudSaveData(jsonString);
#endif

            Debug.Log(jsonString + "\n\n" + _saveDictionary.Aggregate("{", (current, pair) => current + $"\"{pair.Key}\" : \"{pair.Value}\"") + "}");
        }

        public static string Get(string key, string defaultValue = null)
        {
            CheckSaveDictionary();

            return _saveDictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static void Delete(string key) => _saveDictionary.Remove(key);

        private static void CheckSaveDictionary()
        {
            if (_saveDictionary != null) return;
            
#if !UNITY_WEBGL || UNITY_EDITOR
            var jsonSaveDictionary = PlayerPrefs.GetString("SaveDataDictionary", "{}");
            _saveDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSaveDictionary);
#else
            PlayerAccount.GetCloudSaveData(jsonCloudData =>
            {
                _saveDictionary = jsonCloudData == default 
                    ? new Dictionary<string, string>() 
                    : JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCloudData);
            }, onErrorCallback: _ => _saveDictionary = new Dictionary<string, string>());

#endif
        }
    }
}