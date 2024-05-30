using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributeManagers : MonoBehaviour
{
    [SerializeField] private WeaponAttributeSuit[] allWeaponAttributes ;

    private Action<object> _onStartLevel;
    
    private Dictionary<WeaponType,int> _currentLevelOfWeapons = new();
    private Dictionary<WeaponType,bool> _currentUnlockWeapons = new();
    
    [Header("Weapons in here will appear on the start of the level")]
    [SerializeField] private WeaponType[] OriginalWeapon;
    
    //Get call by buttons here
    public void OnWeaponUpgrade(WeaponType typeSelect)
    {
        //Find Suit with that type
        WeaponAttributeSuit tempSuit = null;

        foreach (var VARIABLE in allWeaponAttributes)
        {
            if (VARIABLE.Type == typeSelect)
            {
                tempSuit = VARIABLE;
            }
        }
       

        //Send event to chosen weapon
        if (tempSuit != null)
        {
            //Send Upgrade event
            EventDispatcher.Instance.PostEvent(EventID.OnWeaponUpgrade, tempSuit.FindAttributeWithLevel(_currentLevelOfWeapons[typeSelect]));
            
            //Level up
            _currentLevelOfWeapons[typeSelect] += 1;
        }
        else
        {
            Debug.LogError("Weapon Type not exist in weapon manager");
        }
    }

    private void OnEnable()
    {
        _onStartLevel = _ => InitAllWeaponsOnScene();
        EventDispatcher.Instance.RegisterListener(EventID.OnStartLevel,_onStartLevel);
    }

    private void InitAllWeaponsOnScene()
    {
        //Init weapons for ruby
        
        foreach (var VARIABLE in allWeaponAttributes)
        {
            //Init both dictionary
            //All level 0
            _currentLevelOfWeapons.Add(VARIABLE.Type, 0);
            //All not unlock
            _currentUnlockWeapons.Add(VARIABLE.Type, false);
        }

        foreach (var VARIABLE in OriginalWeapon)
        {
            //Active weapon in original for Ruby
            EventDispatcher.Instance.PostEvent(EventID.OnWeaponUpgrade,);
        }
        
    }
}
