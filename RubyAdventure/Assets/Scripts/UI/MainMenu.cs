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
        //Update sound status
        AudioManager.MusicMuted = !PlayerPrefHelper.GetMusicStatus();
        AudioManager.SoundMuted = !PlayerPrefHelper.GetSoundStatus();

        //Play title music
        AudioManager.PlayMusic(AudioLibraryMusic.TitleMusic);
    }
    

    public void OnTutorialClick()
    {
        TitleSceneManager.Instance.UIController.OpenPanel(UIPanelID.Tutorial);
    }
}
