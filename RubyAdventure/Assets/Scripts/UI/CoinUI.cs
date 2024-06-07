using System;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private void Start()
    {
        UpdateCoin();
    }
    private Action<object> _onCoinCollectRef;
    private void OnEnable()
    {
        //Open when level up
        _onCoinCollectRef = _ => UpdateCoin();
        EventDispatcher.Instance.RegisterListener(EventID.OnCoinReceive,_onCoinCollectRef);
    }

    private void UpdateCoin()
    {
        coinText.text = PlayerPrefsHelper.GetCurrentCoin().ToString();
    }
}
