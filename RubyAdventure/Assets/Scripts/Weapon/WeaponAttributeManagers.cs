using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponAttributeManagers : MonoBehaviour
{
    [SerializeField] private WeaponAttributeSuit[] allWeaponAttributes ;

    private Action<object> _onStartLevel;
    private Action<object> _onWeaponUnlockPref;

    
    //0: Not Unlock 
    //1: Base Level
    private readonly Dictionary<WeaponType,int> _currentLevelOfWeapons = new();
    
    
    [Header("Weapons in here will appear on the start of the level")]
    [SerializeField] private WeaponType[] originalWeapon;
    
    //Get call by buttons here
    public void OnWeaponUpgrade(WeaponType typeSelect)
    {
        //Find Suit with that type
        WeaponAttributeSuit tempSuit = 
            allWeaponAttributes.FirstOrDefault(variable => variable.Type == typeSelect);


        //Send event to chosen weapon
        if (tempSuit != null)
        {
            //Level up
            _currentLevelOfWeapons[typeSelect] += 1;   
            Debug.Log("Tick event through button");
            
            //Give it first Level
            WeaponData weaponData = new WeaponData(typeSelect,
                tempSuit.FindAttributeWithLevel(_currentLevelOfWeapons[typeSelect]));

            //Send Upgrade event
            EventDispatcher.Instance.PostEvent(EventID.OnWeaponUpgrade, weaponData);
        }
        else
        {
            Debug.LogError("Weapon Type not exist in weapon manager");
        }
    }

    public WeaponAttribute AttributeOfFirstLevel(WeaponType typeSelect)
    {
        //Find Suit with that type
        WeaponAttributeSuit tempSuit = 
            allWeaponAttributes.FirstOrDefault(variable => variable.Type == typeSelect);
        return tempSuit!=null ? tempSuit.FindAttributeWithLevel(0) : null;
    }
    

    private void OnEnable()
    {
        _onStartLevel = _ => InitAllWeaponsOnScene();
        EventDispatcher.Instance.RegisterListener(EventID.OnStartLevel,_onStartLevel);


    }

    private void InitAllWeaponsOnScene()
    {
        //Init weapons for ruby
        
        foreach (var variable in allWeaponAttributes)
        {
            //Init both dictionary
            //All level 0
            bool isActive = false;
            foreach (var activeWeaponNow in originalWeapon)
            {
                //If in this list then active it
                if (variable.Type != activeWeaponNow) continue;
                
                //Weapon need to be in ruby hand NOW
                _currentLevelOfWeapons.Add(variable.Type, 1);
                EventDispatcher.Instance.PostEvent(EventID.OnWeaponUnlock,activeWeaponNow);
                isActive = true;
            }

            if (!isActive)
            {
                //This weapon type has not been unlocked
                _currentLevelOfWeapons.Add(variable.Type, 0);
            }
                
        }
        
        //This event only need to be active after initial base weapons
        _onWeaponUnlockPref = param => OnWeaponUnlock((WeaponType)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnWeaponUnlock,_onWeaponUnlockPref);
    }

    public Dictionary<WeaponAttributeSuit, int> GetAllWeaponDataInfo()
    {
        Dictionary<WeaponAttributeSuit, int> allData = new Dictionary<WeaponAttributeSuit, int>();

        foreach (var pairValue in _currentLevelOfWeapons)
        {
            allData.Add(FindSuitWithType(pairValue.Key),pairValue.Value);    
        }
        
        return allData;
    }

    private WeaponAttributeSuit FindSuitWithType(WeaponType selectType)
    {
        return allWeaponAttributes.FirstOrDefault(variable => selectType == variable.Type);
    }

    private void OnWeaponUnlock(WeaponType weaponType)
    {
        //Update weapon level when unlock
        _currentLevelOfWeapons[weaponType]++;
    }
}
