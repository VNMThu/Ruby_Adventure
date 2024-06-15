using System.Collections;
using System.Collections.Generic;
using JSAM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SureQuit : UIPanel
{
    public void OnYesClick()
    {
        Time.timeScale = 1f;
        AudioManager.StopMusic(AudioLibraryMusic.GameplayMusic);
        SceneManager.LoadScene("Title");
    }

    public void OnNoClick()
    {
        OnClose();
    }
}
