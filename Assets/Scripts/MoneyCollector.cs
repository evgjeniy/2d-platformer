﻿using UnityEngine;
using UnityEngine.Events;
using Utils;

public class MoneyCollector : MonoCashed
{
    public UnityEvent<int> onCoinsChanged;
    public UnityEvent<string> onCoinsChangedString;

    public int CoinsAmount
    {
        get => int.Parse(YandexCloudSaveData.Get(StringConstants.CoinsSaveKey, "0"));
        set
        {
            if (value < 0) return;
            YandexCloudSaveData.Save(StringConstants.CoinsSaveKey, $"{value}");
            
            SendCoinsChanged();
        }
    }

    protected override void PostAwake() => SendCoinsChanged();
    
    public void Collect(int coinsAmount) => CoinsAmount += coinsAmount;

    private void SendCoinsChanged()
    {
        var coinsAmount = CoinsAmount;
        onCoinsChanged?.Invoke(coinsAmount);
        onCoinsChangedString?.Invoke($"{coinsAmount}");
    }

#if UNITY_EDITOR
    [ContextMenu("Reset Money Amount")]
    private void ResetMoneyAmount() => CoinsAmount = 0;
#endif
}