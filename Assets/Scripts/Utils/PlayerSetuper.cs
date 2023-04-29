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

            healthBarSlider.direction = inputType == PlayerInputType.FirstPlayer
                ? Slider.Direction.LeftToRight
                : Slider.Direction.RightToLeft;
            
            var healthBarRect = healthBarSlider.GetComponent<RectTransform>();
            if (healthBarRect == null) return;

            var anchors = new Vector2(inputType == PlayerInputType.FirstPlayer ? 0.0f : 1.0f, 1.0f);
            
            healthBarRect.pivot = anchors;
            healthBarRect.anchorMax = anchors;
            healthBarRect.anchorMin = anchors;
            
            healthBarRect.localPosition = new Vector3(
                healthCanvasRect.sizeDelta.x / 2.0f * (inputType == PlayerInputType.FirstPlayer ? -1 : 1), 
                healthCanvasRect.sizeDelta.y / 2.0f, 0.0f);
        }
    }
}