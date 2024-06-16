using System;
using JSAM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public RubyController Ruby => ruby;
    [SerializeField] private RubyController ruby;
    public UIController UIController => uiController;
    [SerializeField] private UIController uiController;
    
    public WeaponAttributeManagers WeaponAttributeManagers => weaponAttributeManagers;
    [SerializeField] private WeaponAttributeManagers weaponAttributeManagers;
    
    public Vector3 RubyPosition => new(ruby.transform.position.x, ruby.transform.position.y + 0.7f, ruby.transform.position.z);

    private void OnEnable()
    {
        //Turn on music
        AudioManager.PlayMusic(AudioLibraryMusic.GameplayMusic);
    }

    public void ReturnToHome()
    {
        Time.timeScale = 1f;
        AudioManager.StopMusic(AudioLibraryMusic.GameplayMusic);
        SceneManager.LoadScene("Title");
    }
    
    public void RestartGameplay()
    {
        Time.timeScale = 1f;
        AudioManager.StopMusic(AudioLibraryMusic.GameplayMusic);
        SceneManager.LoadScene("Gameplay");
    }
    
}