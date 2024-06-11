using System;
using System.Collections;
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
        LoadGameplay();
    }
}
