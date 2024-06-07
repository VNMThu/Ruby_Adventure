using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : UIPanel   
{
    public void PauseGame()
    {
        OnOpen();
    }

    public void ResumeGame()
    {
        OnClose();
    }
    
    public void MainMenuClick()
    {
        GameManager.Instance.UIController.OpenPanel(UIPanelID.SureQuit);
    }
    
    
}
