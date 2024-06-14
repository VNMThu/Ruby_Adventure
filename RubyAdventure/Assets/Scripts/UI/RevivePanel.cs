using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePanel : UIPanel
{
    private Action<object> _onRubyDeathRef;
    private void OnEnable()
    {
        //Open when level up
        _onRubyDeathRef = _ => OnOpen();
        EventDispatcher.Instance.RegisterListener(EventID.OnRubyDeath,_onRubyDeathRef);
    }
    public void OnReviveClick()
    {
        //Use coin
        PlayerPrefHelper.SubtractCoins(Constant.CoinNeedToRevive);
        //Update coin
        EventDispatcher.Instance.PostEvent(EventID.OnCoinReceive);
        //Revive Ruby
        EventDispatcher.Instance.PostEvent(EventID.OnRubyRevive);
        OnClose();
    }

    public override void OnOpen(bool isFromGameplay = true)
    {
        if (PlayerPrefHelper.GetCurrentCoin()>=Constant.CoinNeedToRevive)
        {
            base.OnOpen(isFromGameplay);
        }
    }

    public void OnNoClick()
    {
        GameManager.Instance.UIController.OpenPanel(UIPanelID.Lose);
    }
}
