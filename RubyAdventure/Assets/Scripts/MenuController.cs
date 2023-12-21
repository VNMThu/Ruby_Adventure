using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void LoadSceneGameplay()
    {
        SceneManager.LoadScene(Constant.PlaySceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
