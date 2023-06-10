using UnityEngine;

namespace Utils
{
    public static class StringConstants
    {
        public const string StartCharacterSkin = "{\"Head\":\"Basic/Head#FFBE78FF\",\"Body\":\"Basic/Human#FFBE78FF\",\"Ears\":\"Basic/HumanEar#FFBE78FF\",\"Hair\":\"Thrones/Hair5#FB5000FF\",\"Beard\":null,\"Helmet\":\"Basic/AgileHat\",\"Glasses\":null,\"Mask\":null,\"Cape\":null,\"Back\":null,\"Shield\":null,\"WeaponType\":\"Melee1H\",\"Expression\":\"Default\",\"BodyScaleX\":\"1\",\"BodyScaleY\":\"1\",\"PrimaryMeleeWeapon\":\"Basic/WoodenSpikedClub\",\"SecondaryMeleeWeapon\":null,\"Armor[0]\":\"Basic/Robe\",\"Armor[1]\":\"Basic/Robe\",\"Armor[2]\":\"Basic/Robe\",\"Armor[3]\":\"Basic/Robe\",\"Armor[4]\":\"Basic/Robe\",\"Armor[5]\":\"Basic/Robe\",\"Armor[6]\":\"Basic/Robe\",\"Armor[7]\":\"Basic/Robe\",\"Armor[8]\":\"Basic/Robe\",\"Armor[9]\":\"Basic/Robe\",\"Armor[10]\":\"Basic/Robe\",\"Armor[11]\":\"Basic/Robe\",\"Armor[12]\":\"Basic/Robe\",\"Armor[13]\":\"Basic/Robe\",\"Armor[14]\":\"Basic/Robe\",\"Armor[15]\":\"Basic/Robe\",\"Armor[16]\":\"Basic/Robe\",\"Armor[17]\":\"Basic/Robe\",\"Armor[18]\":\"Basic/Robe\",\"Armor[19]\":\"Basic/Robe\",\"Armor[20]\":\"Basic/Robe\",\"Armor[21]\":\"Basic/Robe\",\"Armor[22]\":\"Basic/Robe\",\"Armor[23]\":\"Basic/Robe\",\"Armor[24]\":\"Basic/Robe\",\"Armor[25]\":\"Basic/Robe\",\"Expression.Default.Eyebrows\":\"Emoji/=3=\",\"Expression.Default.Eyes\":\"Expressions/Bored#00C8FFFF\",\"Expression.Default.Mouth\":\"Bonus/1\",\"Expression.Angry.Eyebrows\":\"Basic/Angry\",\"Expression.Angry.Eyes\":null,\"Expression.Angry.Mouth\":\"Basic/Angry\",\"Expression.Dead.Eyebrows\":null,\"Expression.Dead.Eyes\":\"Basic/Dead#00C8FFFF\",\"Expression.Dead.Mouth\":\"Emoji/Dead\"}";
        public const string BaseShopSet = "[null,\"Equipment/Helmet/Basic/AgileHat\",\"Equipment/Armor/Basic/Robe\",\"Equipment/MeleeWeapon1H/Basic/Hammer/WoodenSpikedClub\",\"BodyParts/Body/Basic/Human\",\"BodyParts/Head/Basic/Head\",\"BodyParts/Hair/Thrones/Hair5\",\"BodyParts/Hair/Bonus/VillageGirl\",\"BodyParts/Beard/Basic/Beard\",\"BodyParts/Eyebrows/Emoji/=3=\",\"BodyParts/Eyebrows/Basic/Angry\",\"BodyParts/Eyes/Expressions/Bored\",\"BodyParts/Ears/Basic/HumanEar\",\"BodyParts/Mouth/Bonus/1\"]";
        public const string ItemParamsContainerSaveKey = nameof(ItemParamsContainerSaveKey);
        public const string LastLevelSceneSaveKey = nameof(LastLevelSceneSaveKey);
        public const string SoundStateKey = nameof(SoundStateKey);
        public const string CoinsSaveKey = nameof(CoinsSaveKey);
    }

    public static class Sound
    {
        public static bool State
        {
            get
            {
                if (!PlayerPrefs.HasKey(StringConstants.SoundStateKey)) State = true;
                return PlayerPrefs.GetString(StringConstants.SoundStateKey) == "True";
            }
            set
            {
                PlayerPrefs.SetString(StringConstants.SoundStateKey, value.ToString());
                PlayerPrefs.Save();
				
				var backgroundAudio = Object.FindObjectOfType<BackgroundAudio>();
                if (backgroundAudio != null) backgroundAudio.enabled = value;
            }
        }
    } 
}