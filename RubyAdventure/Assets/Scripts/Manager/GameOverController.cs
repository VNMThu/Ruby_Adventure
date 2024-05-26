using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScene;

    [SerializeField] private GameObject gameVictoryScene;
    [SerializeField] private GameObject BackGround;
    [SerializeField] private AudioClip gameOverSound;
    private RubyController ruby;
    [SerializeField] private AudioClip gameVictorySound;

    [SerializeField] private TMP_Text TimeTxt;

    // Start is called before the first frame update
    private void Start()
    {
        //Đăng ký vào sự kiện
        ruby = FindObjectOfType<RubyController>();
        ruby.OnPlayerDeath += OnGameOver;
    }


    public void RestartGame()
    {
        TitleController.instance.btnChoice = "Play";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    private void OnGameOver()
    {
        //Bật GameOver lên
        gameOverScene.SetActive(true);
    }

    private void OnGameWin()
    {
        gameVictoryScene.SetActive(true);
        TimeTxt.text = "Time: " + Mathf.RoundToInt(Time.timeSinceLevelLoad) + "s";
    }
}