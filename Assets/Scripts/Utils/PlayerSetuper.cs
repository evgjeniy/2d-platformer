using System.Collections.Generic;
using Agava.YandexGames;
using DG.Tweening;
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
            playerEntity.Character.FromJson(PlayerPrefs.GetString(inputType.GetSaveKey(), StringConstants.StartCharacterSkin));

            var canvasRect = playerEntity.GetComponentInChildren<Canvas>()?.GetComponent<RectTransform>();
            SetupPlayerHealthBar(canvasRect, inputType);
            SetupPlayerJoystick(canvasRect, inputType);
            SetupPlayerAttackArea(canvasRect, inputType);
        }

        private static void SetupPlayerHealthBar(RectTransform canvasRect, PlayerInputType inputType)
        {
            if (canvasRect == null) return;
            
            var healthBarSlider = canvasRect.GetComponentInChildren<Slider>();
            if (healthBarSlider == null) return;

            var isFirstPlayer = inputType == PlayerInputType.FirstPlayer;
            healthBarSlider.direction = isFirstPlayer
                ? Slider.Direction.LeftToRight
                : Slider.Direction.RightToLeft;

            var healthBarRect = healthBarSlider.GetComponent<RectTransform>();
            if (healthBarRect == null) return;

            var anchors = new Vector2(isFirstPlayer ? 0.0f : 1.0f, 1.0f);

            healthBarRect.pivot = new Vector2(isFirstPlayer ? 0.0f : 1.0f, 1.0f);
            healthBarRect.anchorMin = new Vector2(isFirstPlayer ? 0.0f : 0.5f, 1.0f);
            healthBarRect.anchorMax = new Vector2(isFirstPlayer ? 0.5f : 1.0f, 1.0f);

            var healthBarLocalPosition = canvasRect.sizeDelta / 2.0f - Vector2.one * 10;
            healthBarLocalPosition.x *= isFirstPlayer ? -1 : 1;
            healthBarRect.localPosition = healthBarLocalPosition;
        }

        private static void SetupPlayerJoystick(RectTransform canvasRect, PlayerInputType inputType)
        {
            if (canvasRect == null) return;

            var joystick = canvasRect.GetComponentInChildren<JoystickController>();
            if (joystick == null) return;
            
            var isFirstPlayer = inputType == PlayerInputType.FirstPlayer;
            joystick.controlPath = $"<Gamepad>/{(isFirstPlayer ? "left" : "right")}Stick";

            var joystickRect = joystick.GetComponent<RectTransform>();
            if (joystickRect == null) return;

            joystickRect.pivot = new Vector2(isFirstPlayer ? 0.0f : 1.0f, 0.0f);
            joystickRect.anchorMin = new Vector2(isFirstPlayer ? 0.0f : 0.5f, 0.0f);
            joystickRect.anchorMax = new Vector2(isFirstPlayer ? 0.5f : 1.0f, 0.5f);

            joystickRect.DOAnchorPos(Vector2.zero, 0.1f);
        }

        private static void SetupPlayerAttackArea(RectTransform canvasRect, PlayerInputType inputType)
        {
            if (canvasRect == null) return;

            var attackArea = canvasRect.GetComponentInChildren<AttackMobileArea>();
            if (attackArea == null) return;
            
            var isFirstPlayer = inputType == PlayerInputType.FirstPlayer;
            attackArea.controlPath = $"<Gamepad>/{(isFirstPlayer ? "left" : "right")}Stick/down";

            var joystickRect = attackArea.GetComponent<RectTransform>();
            if (joystickRect == null) return;

            joystickRect.pivot = new Vector2(isFirstPlayer ? 0.0f : 1.0f, 1.0f);
            joystickRect.anchorMin = new Vector2(isFirstPlayer ? 0.0f : 0.5f, 0.5f);
            joystickRect.anchorMax = new Vector2(isFirstPlayer ? 0.5f : 1.0f, 1.0f);

            joystickRect.DOAnchorPos(Vector2.zero, 0.1f);
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