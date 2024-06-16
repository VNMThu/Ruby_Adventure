using System;

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
        GameManager.Instance.ReturnToHome();

    }
    
    public void OnRestartClick()
    {
        GameManager.Instance.RestartGameplay();
    }
    
}
