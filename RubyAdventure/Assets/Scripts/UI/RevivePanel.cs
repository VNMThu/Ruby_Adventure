using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RevivePanel : UIPanel
{
    private Action<object> _onRubyDeathRef;
    private int _deathCount = 1;
    private bool _isFirstDeath = true;
    [SerializeField] private TextMeshProUGUI displayMessageText;
    private void OnEnable()
    {
        //Open when level up
        _onRubyDeathRef = _ => OnOpen();
        EventDispatcher.Instance.RegisterListener(EventID.OnRubyDeath,_onRubyDeathRef);
    }
    public void OnReviveClick()
    {
        //Use coin
        PlayerPrefHelper.SubtractCoins(_deathCount * Constant.CoinNeedToRevive);
        //Update coin
        EventDispatcher.Instance.PostEvent(EventID.OnCoinReceive);
        OnClose(() =>
        {
            //Revive Ruby
            EventDispatcher.Instance.PostEvent(EventID.OnRubyRevive);
        });

   
    }

    public override void OnOpen(bool isStopTime = true)
    {
        if (_isFirstDeath)
        {
            _isFirstDeath = false;
        }
        else
        {
            _deathCount++;
        }
        if (PlayerPrefHelper.GetCurrentCoin() < _deathCount * Constant.CoinNeedToRevive) return;

        displayMessageText.text = "Use "+_deathCount * Constant.CoinNeedToRevive+" Coins to \nREVIVE?";
        base.OnOpen(isStopTime);
    }

    public void OnNoClick()
    {
        base.OnClose();
        GameManager.Instance.UIController.OpenPanel(UIPanelID.Lose);
    }
}
