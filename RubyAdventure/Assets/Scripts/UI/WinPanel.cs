using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : UIPanel
{
    private Action<object> _onWinRef;
    private void OnEnable()
    {
        //Open when level up
        _onWinRef = _ => OnOpen();
        EventDispatcher.Instance.RegisterListener(EventID.OnWinLevel,_onWinRef);
    }

    public override void OnOpen()
    {
        Time.timeScale = 0f;
        base.OnOpen();
    }

    public void OnHomeClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
    
}
