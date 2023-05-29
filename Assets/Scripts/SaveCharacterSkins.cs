using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

public class SaveCharacterSkins : MonoBehaviour
{
    private List<string> _boughtSkins = new();

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(StringConstants.ItemParamsContainerSaveKey))
        {
            PlayerPrefs.SetString(StringConstants.ItemParamsContainerSaveKey, StringConstants.BaseShopSet);
            PlayerPrefs.Save();
        }
        
        var savedJson = PlayerPrefs.GetString(StringConstants.ItemParamsContainerSaveKey);
        _boughtSkins = JsonConvert.DeserializeObject<List<string>>(savedJson);
    }

    public bool Contains(string key) => _boughtSkins.Contains(key);
    
    public void Add(string key)
    {
        if (!_boughtSkins.Contains(key)) _boughtSkins.Add(key);
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(StringConstants.ItemParamsContainerSaveKey, JsonConvert.SerializeObject(_boughtSkins));
        PlayerPrefs.Save();
    }

#if UNITY_EDITOR

    [ContextMenu("Clear Saved Skins")]
    private void ClearPrefs()
    {
        PlayerPrefs.DeleteKey(StringConstants.ItemParamsContainerSaveKey);
        PlayerPrefs.Save();
    }
    
#endif
}