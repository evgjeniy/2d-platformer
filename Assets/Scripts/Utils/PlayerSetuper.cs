using System.Collections.Generic;
using Entities.Player;
using InputScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public static class PlayerCustomizer
    {
        public static void SetupPlayer(this PlayerEntity playerEntity, PlayerInputType inputType)
        {
            playerEntity.Controller.InputType = inputType;

            var healthCanvasRect = playerEntity.GetComponentInChildren<Canvas>()?.GetComponent<RectTransform>();
            if (healthCanvasRect == null) return;

            var healthBarSlider = healthCanvasRect.GetComponentInChildren<Slider>();
            if (healthBarSlider == null) return;

            var isFirstPlayer = inputType == PlayerInputType.FirstPlayer;
            healthBarSlider.direction = isFirstPlayer
                ? Slider.Direction.LeftToRight
                : Slider.Direction.RightToLeft;
            
            var healthBarRect = healthBarSlider.GetComponent<RectTransform>();
            if (healthBarRect == null) return;

            var anchors = new Vector2(isFirstPlayer ? 0.0f : 1.0f, 1.0f);
            
            healthBarRect.pivot = anchors;
            healthBarRect.anchorMax = anchors;
            healthBarRect.anchorMin = anchors;

            var healthBarLocalPosition = healthCanvasRect.sizeDelta / 2.0f - Vector2.one * 5;
            healthBarLocalPosition.x *= isFirstPlayer ? -1 : 1;
            healthBarRect.localPosition = healthBarLocalPosition;
        }
        
        public static PlayerEntity GetNearestPlayer(this List<PlayerEntity> players, Transform target)
        {
            if (players == null || players.Count == 0) return null;

            var nearest = players[0];
            for (var i = 1; i < players.Count; i++)
                if (Vector3.Distance(players[i].position, target.position) <
                    Vector3.Distance(nearest.position, target.position)) nearest = players[i];

            return nearest;
        }
    }
}