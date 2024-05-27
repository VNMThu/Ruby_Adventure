using System;
using UnityEngine;

public class LevelUpPanel : UIPanel
{
    private Action<object> _onLevelUpRef;
    private void OnEnable()
    {
        //Open when level up
        _onLevelUpRef = _ => OnOpen();
        EventDispatcher.Instance.RegisterListener(EventID.OnLevelUp,_onLevelUpRef);
    }

    public override void OnOpen()
    {
        Time.timeScale = 0f;
        base.OnOpen();
    }

    public override void OnClose()
    {
        Time.timeScale = 1f;
        base.OnClose();
    }
}
