using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadingController : MonoBehaviour
{
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private GameObject loadingScene;


    // Start is called before the first frame update
    public void LoadGameplay()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        loadingScene.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Gameplay");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

    public void PlayGame()
    {
        TitleController.instance.btnChoice = EventSystem.current.currentSelectedGameObject.name;
        LoadGameplay();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
