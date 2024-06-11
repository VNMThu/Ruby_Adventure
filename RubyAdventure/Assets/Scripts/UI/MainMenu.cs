using System;
using System.Collections;
using System.Collections.Generic;
using JSAM;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnButtonPlayClick()
    {
        TitleSceneManager.Instance.UIController.OpenPanel(UIPanelID.Loading);
    }

    private void Start()
    {
        AudioManager.PlayMusic(AudioLibraryMusic.TitleMusic);
    }

    public void OnTutorialClick()
    {
        TitleSceneManager.Instance.UIController.OpenPanel(UIPanelID.Tutorial);
    }
}
