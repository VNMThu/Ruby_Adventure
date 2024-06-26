using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject AskSave;
    [SerializeField] GameObject SaveSuccesfully;

    [SerializeField] GameObject PauseGameObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseGameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseGameObject.SetActive(true);
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.txt");
        if (!File.Exists(path))
        {
            JustSave();
        }
        else
        {
            AskSave.SetActive(true);
        }
    }

    public void JustSave()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.txt");
        WriteToFile(path);
    }

    void WriteToFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        //Create SaveData
        FileStream file = File.Create(path);
        //Create binary 
        file.Close();
        Debug.Log("Game Saved:" + path);
        SaveSuccesfully.SetActive(true);
    }
}