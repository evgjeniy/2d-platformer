using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

public class SaveCharacterSkins : MonoBehaviour
{
    private List<string> _boughtSkins = new();

    private void Awake()
    {
        if (YandexCloudSaveData.Get(StringConstants.ItemParamsContainerSaveKey) is null)
            YandexCloudSaveData.Save(StringConstants.ItemParamsContainerSaveKey, StringConstants.BaseShopSet);

        var savedJson = YandexCloudSaveData.Get(StringConstants.ItemParamsContainerSaveKey);
        _boughtSkins = JsonConvert.DeserializeObject<List<string>>(savedJson);
    }

    public bool Contains(string key) => _boughtSkins.Contains(key);
    
    public void Add(string key)
    {
        if (!_boughtSkins.Contains(key)) _boughtSkins.Add(key);
        SaveData();
    }

    private void SaveData() =>
        YandexCloudSaveData.Save(StringConstants.ItemParamsContainerSaveKey, JsonConvert.SerializeObject(_boughtSkins));

#if UNITY_EDITOR

    [ContextMenu("Clear Saved Skins")]
    private void ClearPrefs() => YandexCloudSaveData.Delete(StringConstants.ItemParamsContainerSaveKey);
    
#endif
}