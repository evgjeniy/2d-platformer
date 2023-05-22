using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using InputScripts;
using UnityEngine;

namespace Assets.HeroEditor.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Abstract item container. It can be inventory bag, player equipment or trader goods.
    /// </summary>
    public abstract class ItemContainer : MonoBehaviour
    {
        /// <summary>
        /// List of items.
        /// </summary>
        public List<Item> Items { get; protected set; } = new List<Item>();

        /// <summary>
        /// Either all items are expanded (i.e. item count = 1, so two equal items will be stored as two list elements).
        /// </summary>
        public bool Expanded;
        public bool SelectOnRefresh = true;
        public Character Character;
        public PlayerInputType playerType;

        public Action<Item> OnLeftClick;
        public Action<Item> OnRightClick;
        public Action<Item> OnDoubleClick;
        public Action<Item> OnMouseEnter;
        public Action<Item> OnMouseExit;

        public abstract void Refresh(Item selected);

        public void Initialize(ref List<Item> items, Item selected = null)
        {
            Items = items;
            Refresh(selected);

            TryLoadJson();
        }

        private void TryLoadJson()
        {
            var playerSaveKey = playerType.GetSaveKey();

            Character.FromJson(PlayerPrefs.HasKey(playerSaveKey)
                ? PlayerPrefs.GetString(playerSaveKey)
                : "{\"Head\":\"Basic/Head#FFBE78FF\",\"Body\":\"Basic/Human#FFBE78FF\",\"Ears\":\"Basic/HumanEar#FFBE78FF\",\"Hair\":\"Thrones/Hair5#FB5000FF\",\"Beard\":null,\"Helmet\":\"Basic/AgileHat\",\"Glasses\":null,\"Mask\":null,\"Cape\":null,\"Back\":null,\"Shield\":null,\"WeaponType\":\"Melee1H\",\"Expression\":\"Default\",\"BodyScaleX\":\"1\",\"BodyScaleY\":\"1\",\"PrimaryMeleeWeapon\":\"Basic/WoodenSpikedClub\",\"SecondaryMeleeWeapon\":null,\"Armor[0]\":\"Basic/Robe\",\"Armor[1]\":\"Basic/Robe\",\"Armor[2]\":\"Basic/Robe\",\"Armor[3]\":\"Basic/Robe\",\"Armor[4]\":\"Basic/Robe\",\"Armor[5]\":\"Basic/Robe\",\"Armor[6]\":\"Basic/Robe\",\"Armor[7]\":\"Basic/Robe\",\"Armor[8]\":\"Basic/Robe\",\"Armor[9]\":\"Basic/Robe\",\"Armor[10]\":\"Basic/Robe\",\"Armor[11]\":\"Basic/Robe\",\"Armor[12]\":\"Basic/Robe\",\"Armor[13]\":\"Basic/Robe\",\"Armor[14]\":\"Basic/Robe\",\"Armor[15]\":\"Basic/Robe\",\"Armor[16]\":\"Basic/Robe\",\"Armor[17]\":\"Basic/Robe\",\"Armor[18]\":\"Basic/Robe\",\"Armor[19]\":\"Basic/Robe\",\"Armor[20]\":\"Basic/Robe\",\"Armor[21]\":\"Basic/Robe\",\"Armor[22]\":\"Basic/Robe\",\"Armor[23]\":\"Basic/Robe\",\"Armor[24]\":\"Basic/Robe\",\"Armor[25]\":\"Basic/Robe\",\"Expression.Default.Eyebrows\":\"Emoji/=3=\",\"Expression.Default.Eyes\":\"Expressions/Bored#00C8FFFF\",\"Expression.Default.Mouth\":\"Bonus/1\",\"Expression.Angry.Eyebrows\":\"Basic/Angry\",\"Expression.Angry.Eyes\":null,\"Expression.Angry.Mouth\":\"Basic/Angry\",\"Expression.Dead.Eyebrows\":null,\"Expression.Dead.Eyes\":\"Basic/Dead#00C8FFFF\",\"Expression.Dead.Mouth\":\"Emoji/Dead\"}");
            
            SaveJson();
        }

        public void SaveJson()
        {
            PlayerPrefs.SetString(playerType.GetSaveKey(), Character.ToJson());
            PlayerPrefs.Save();
        }
    }
}