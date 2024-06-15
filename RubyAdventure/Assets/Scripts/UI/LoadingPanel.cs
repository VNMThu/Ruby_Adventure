using System;
using System.Collections;
using DG.Tweening;
using JSAM;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : UIPanel
{
    [SerializeField] private Slider loadingSlider;

    // Start is called before the first frame update
    private void LoadGameplay()
    {
        StartCoroutine(LoadSceneAsync());
        AudioManager.StopMusic(AudioLibraryMusic.TitleMusic);
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Gameplay");
        while (operation != null && !operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
    public override void OnOpen(bool isFromGameplay = true)
    {
        base.OnOpen(false);
        loadingSlider.value = 0f;
        DOVirtual.DelayedCall(openAnimationTime, LoadGameplay);
    }
    
    public override void OnClose(bool isFromGameplay = true)
    {
        base.OnClose(false);
    }
    
}
