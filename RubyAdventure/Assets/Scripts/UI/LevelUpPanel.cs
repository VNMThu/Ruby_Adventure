using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUpPanel : UIPanel
{
    private Action<object> _onLevelUpRef;
    [SerializeField] private ButtonUpgradeWeapon[] buttonUpgradeWeapons;
    private void OnEnable()
    {
        //Open when level up
        _onLevelUpRef = _ => OnOpen();
        EventDispatcher.Instance.RegisterListener(EventID.OnLevelUp,_onLevelUpRef);
    }
    
    

    public override void OnOpen()
    {
        Time.timeScale = 0f;
        SetUpUpgradeSlot();
        base.OnOpen();
    }

    private void SetUpUpgradeSlot()
    {
        //Get all 
        Dictionary<WeaponAttributeSuit, int> allData = GameManager.Instance.WeaponAttributeManagers.GetAllWeaponDataInfo();

        foreach (var VARIABLE in allData.ToList())
        {
            //Filter it
            // Already Reach max level
            if (VARIABLE.Value > VARIABLE.Key.WeaponAttributePerLevel.Length)
            {
                //Remove
                allData.Remove(VARIABLE.Key);
            }
        }
        
        //Find 3 random element
        List<int> randomIndex = new List<int>();
        int previousIndex = -1;
        while (randomIndex.Count < buttonUpgradeWeapons.Length || randomIndex.Count < allData.Count)
        {
            int randomInt = Random.Range(0, allData.Count);
            if (randomInt == previousIndex) continue;
            randomIndex.Add(randomInt);
            previousIndex = randomInt;
        }
        
        //Turn it into UI

        int differentBetweenUIAndData = allData.Count - buttonUpgradeWeapons.Length;

        for (int i = 0; i < differentBetweenUIAndData; i++)
        {
            buttonUpgradeWeapons.Last().gameObject.SetActive(false);
        }

        int indexOfRandom = 0;
        foreach (var buttonUpgrade in buttonUpgradeWeapons)
        {
            if (!buttonUpgrade.gameObject.activeInHierarchy) continue;
            
            buttonUpgrade.InitUI(allData.ToList()[randomIndex[indexOfRandom]]);
            indexOfRandom++;
        }
    }

    public override void OnClose()
    {
        Time.timeScale = 1f;
        base.OnClose();
    }
}
