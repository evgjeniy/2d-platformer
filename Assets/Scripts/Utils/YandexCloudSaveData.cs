using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Newtonsoft.Json;
using UnityEngine;

namespace Utils
{
    public static class YandexCloudSaveData
    {
        private static Dictionary<string, string> _saveDictionary = null;
        private static MonoBehaviour _context;

        public static void Save(string key, string value)
        {
            CheckContext();
            _context.StartCoroutine(CheckSaveDictionary());
            
            _saveDictionary[key] = value;

            _context.StartCoroutine(SaveJsonToCloud());
        }

        public static string Get(string key, string defaultValue = null)
        {
            CheckContext();
            _context.StartCoroutine(CheckSaveDictionary());

            return _saveDictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static void Delete(string key)
        {
            _saveDictionary.Remove(key);
            
            CheckContext();
            _context.StartCoroutine(SaveJsonToCloud());
        }

        private static void CheckContext()
        {
            if (_context != null) return;
            
            var gameObject = new GameObject("YandexInitializeContext");
            _context = gameObject.AddComponent<EmptyBehaviour>();
            Object.DontDestroyOnLoad(gameObject);
        }

        private static IEnumerator SaveJsonToCloud()
        {
            var jsonString = JsonConvert.SerializeObject(_saveDictionary);

#if !UNITY_WEBGL || UNITY_EDITOR
            PlayerPrefs.SetString("SaveDataDictionary", jsonString);
            PlayerPrefs.Save();
#else
            if (!YandexGamesSdk.IsInitialized) 
                yield return YandexGamesSdk.Initialize(); 
            
            PlayerAccount.SetCloudSaveData(jsonString);
#endif
            yield break;
        }
        
        private static IEnumerator CheckSaveDictionary()
        {
            if (_saveDictionary != null) yield break;
            
#if !UNITY_WEBGL || UNITY_EDITOR
            var jsonSaveDictionary = PlayerPrefs.GetString("SaveDataDictionary", "");
            _saveDictionary = jsonSaveDictionary == "" 
                ? new Dictionary<string, string>() 
                : JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSaveDictionary);
#else
            if (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize();

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