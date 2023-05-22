using UnityEngine;
using UnityEngine.Events;

public class MoneyCollector : MonoTransform
{
    private const string CoinsSaveKey = nameof(CoinsSaveKey);

    public UnityEvent<int> onCoinsChanged;
    public UnityEvent<string> onCoinsChangedString;

    public int CoinsAmount
    {
        get => PlayerPrefs.GetInt(CoinsSaveKey, 0);
        set
        {
            if (value < 0) return;
            PlayerPrefs.SetInt(CoinsSaveKey, value);
            
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