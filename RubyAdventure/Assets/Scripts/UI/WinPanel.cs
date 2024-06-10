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

    public void OnHomeClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
    
    public void OnRestartClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
    
}
