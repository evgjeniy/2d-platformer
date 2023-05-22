using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SaveCharacterSkins : MonoBehaviour
{
    private const string ItemParamsContainerSaveKey = nameof(ItemParamsContainerSaveKey);
    private List<string> _boughtSkins = new();

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(ItemParamsContainerSaveKey))
        {
            PlayerPrefs.SetString(ItemParamsContainerSaveKey, "[null,\"Equipment/Helmet/Basic/AgileHat\",\"Equipment/Armor/Basic/Robe\",\"Equipment/MeleeWeapon1H/Basic/Hammer/WoodenSpikedClub\",\"BodyParts/Body/Basic/Human\",\"BodyParts/Head/Basic/Head\",\"BodyParts/Hair/Thrones/Hair5\",\"BodyParts/Hair/Bonus/VillageGirl\",\"BodyParts/Beard/Basic/Beard\",\"BodyParts/Eyebrows/Emoji/=3=\",\"BodyParts/Eyebrows/Basic/Angry\",\"BodyParts/Eyes/Expressions/Bored\",\"BodyParts/Ears/Basic/HumanEar\",\"BodyParts/Mouth/Bonus/1\"]");
            PlayerPrefs.Save();
        }
        
        var savedJson = PlayerPrefs.GetString(ItemParamsContainerSaveKey);
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
        PlayerPrefs.SetString(ItemParamsContainerSaveKey, JsonConvert.SerializeObject(_boughtSkins));
        PlayerPrefs.Save();
    }

#if UNITY_EDITOR

    [ContextMenu("ClearSavedSkins")]
    private void ClearPrefs()
    {
        PlayerPrefs.DeleteKey(ItemParamsContainerSaveKey);
        PlayerPrefs.Save();
    }
    
#endif
}