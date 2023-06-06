using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using InputScripts;
using UnityEngine;
using Utils;

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

            Character.FromJson(YandexCloudSaveData.Get(playerSaveKey, StringConstants.StartCharacterSkin));
            
            SaveJson();
        }

        public void SaveJson()
        {
            YandexCloudSaveData.Save(playerType.GetSaveKey(), Character.ToJson());
        }
    }
}