using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponAttributeManagers : MonoBehaviour
{
    [SerializeField] private WeaponAttributeSuit[] allWeaponAttributes ;

    private Action<object> _onStartLevel;
    
    
    //0: Not Unlock 
    //1: Base Level
    private Dictionary<WeaponType,int> _currentLevelOfWeapons = new();
    
    
    [Header("Weapons in here will appear on the start of the level")]
    [SerializeField] private WeaponType[] OriginalWeapon;
    
    //Get call by buttons here
    public void OnWeaponUpgrade(WeaponType typeSelect)
    {
        //Find Suit with that type
        WeaponAttributeSuit tempSuit = 
            allWeaponAttributes.FirstOrDefault(VARIABLE => VARIABLE.Type == typeSelect);


        //Send event to chosen weapon
        if (tempSuit != null)
        {
            //Level up
            _currentLevelOfWeapons[typeSelect] += 1;   
            
            //Send Upgrade event
            EventDispatcher.Instance.PostEvent(EventID.OnWeaponUpgrade, tempSuit.FindAttributeWithLevel(_currentLevelOfWeapons[typeSelect]));
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
            allWeaponAttributes.FirstOrDefault(VARIABLE => VARIABLE.Type == typeSelect);
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
        
        foreach (var VARIABLE in allWeaponAttributes)
        {
            //Init both dictionary
            //All level 0
            bool isActive = false;
            foreach (var activeWeaponNow in OriginalWeapon)
            {
                //If in this list then active it
                if (VARIABLE.Type != activeWeaponNow) continue;
                
                //Weapon need to be in ruby hand NOW
                _currentLevelOfWeapons.Add(VARIABLE.Type, 1);
                EventDispatcher.Instance.PostEvent(EventID.OnWeaponUnlock,activeWeaponNow);
                isActive = true;
            }

            if (!isActive)
            {
                //This weapon type has not been unlocked
                _currentLevelOfWeapons.Add(VARIABLE.Type, 0);
            }
                
        }
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
        return allWeaponAttributes.FirstOrDefault(VARIABLE => selectType == VARIABLE.Type);
    }
}
