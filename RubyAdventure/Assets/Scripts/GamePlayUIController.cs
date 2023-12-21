using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUIController : MonoBehaviour
{
    [Header("Win/Lose")]
    [SerializeField] private GameObject loseScene;
    [SerializeField] private GameObject winScene;
    private void OnEnable()
    {
        GameManager.Instance.OnGameWin += ShowWinScene;
        GameManager.Instance.OnGameOver += ShowLoseScene;
        
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameWin -= ShowWinScene;
        GameManager.Instance.OnGameOver -= ShowLoseScene;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Turn off UI
        SetActiveWinLose(false);
    }

    
    //Win/Lose UI
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(Constant.TitleSceneIndex);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Constant.PlaySceneIndex);
    }

    private void ShowWinScene()
    {
        loseScene.SetActive(false);
        winScene.SetActive(true);
    }    
    private void ShowLoseScene()
    {
        loseScene.SetActive(true);
        winScene.SetActive(false);
    }

    private void SetActiveWinLose(bool toggle)
    {
        loseScene.gameObject.SetActive(toggle);
        winScene.gameObject.SetActive(toggle);
    }

    
}
