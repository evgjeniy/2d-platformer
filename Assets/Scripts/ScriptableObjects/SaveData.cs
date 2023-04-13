using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Save Data", fileName = "SaveDataConfig")]
    public class SaveDataConfig : ScriptableObject
    {
        private const string SaveDataKey = "SaveDataKey";

        private SaveData _saveData;

        public SaveData SaveData
        {
            get
            {
                if (_saveData != null) return _saveData;
                if (!PlayerPrefs.HasKey(SaveDataKey)) return _saveData = new SaveData();

                var canBeNull = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveDataKey));
                return _saveData = (canBeNull == null) ? new SaveData() : _saveData;
            }
        }

        public void Save() => PlayerPrefs.SetString(SaveDataKey, JsonUtility.ToJson(SaveData));
    }

    [System.Serializable]
    public class SaveData
    {
        public float moneyAmount;
        public float gemsAmount;
    }
}