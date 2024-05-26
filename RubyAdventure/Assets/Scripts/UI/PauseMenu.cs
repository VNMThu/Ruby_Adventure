using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : UIPanel   
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnOpen();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        OnClose();
    }
    
    public void MainMenuClick()
    {
        GameManager.Instance.UIController.OpenPanel(UIPanelID.SureQuit);
    }
    
    
}
