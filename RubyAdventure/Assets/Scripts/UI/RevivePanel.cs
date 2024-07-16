using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RevivePanel : UIPanel
{
    private Action<object> _onRubyDeathRef;
    private int dealthCount = 1;
    private bool isFirstDeath = true;
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
        PlayerPrefHelper.SubtractCoins(dealthCount * Constant.CoinNeedToRevive);
        //Update coin
        EventDispatcher.Instance.PostEvent(EventID.OnCoinReceive);
        //Revive Ruby
        EventDispatcher.Instance.PostEvent(EventID.OnRubyRevive);
        OnClose();
    }

    public override void OnOpen(bool isStopTime = true)
    {
        if (PlayerPrefHelper.GetCurrentCoin() < dealthCount * Constant.CoinNeedToRevive) return;
        if (isFirstDeath)
        {
            dealthCount++;
            isFirstDeath = false;
        }

        displayMessageText.text = "Use "+dealthCount * Constant.CoinNeedToRevive+" Coins to \nREVIVE?";
        base.OnOpen(isStopTime);
    }

    public void OnNoClick()
    {
        base.OnClose();
        GameManager.Instance.UIController.OpenPanel(UIPanelID.Lose);
    }
}
