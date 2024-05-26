using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SureQuit : UIPanel
{
    public void OnYesClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void OnNoClick()
    {
        OnClose();
    }
}
