﻿using System.Collections.Generic;
using DG.Tweening;
using Entities.Player;
using Interactable.Base;
using Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactable
{
    public class FinishComponent : InteractableBehaviour<Finish>
    {
        private void OnTriggerExit2D(Collider2D other) => interactable.Interact(this, other);
    }

    [System.Serializable]
    public class Finish : IInteractable
    {
        [SerializeField] private string sceneName = "Level _";
        
        private List<PlayerEntity> _enteredEntities = new();
        
        public void Interact(MonoCashed<Collider2D> finish, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            if (finish.First.IsTouching(other)) _enteredEntities.Add(player);
            else _enteredEntities.Remove(player);

            if (_enteredEntities.Count != (PlayerSpawner.IsTwoPlayers ? 2 : 1)) return;
            
            _enteredEntities.ForEach(entity => entity.DOKill());
            SceneManager.LoadScene(sceneName);
        }
    }
}