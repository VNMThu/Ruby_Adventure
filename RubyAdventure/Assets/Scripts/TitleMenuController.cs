using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuController : MonoBehaviour
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
