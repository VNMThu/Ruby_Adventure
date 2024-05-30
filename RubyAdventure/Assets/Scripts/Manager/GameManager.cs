using System;
using JSAM;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public RubyController Ruby => ruby;
    [SerializeField] private RubyController ruby;
    public UIController UIController => uiController;
    [SerializeField] private UIController uiController;
    
    public WeaponAttributeManagers WeaponAttributeManagers => weaponAttributeManagers;
    [SerializeField] private WeaponAttributeManagers weaponAttributeManagers;
    
    public Vector3 RubyPosition => new(ruby.transform.position.x, ruby.transform.position.y + 0.5f, ruby.transform.position.z);

    private void OnEnable()
    {
        //Turn on music
        AudioManager.PlayMusic(AudioLibraryMusic.GameplayMusic);
    }
    
}