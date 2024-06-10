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
        SetUpUpgradeSlot();
        base.OnOpen();
    }

    private void SetUpUpgradeSlot()
    {
        //Get all 
        Dictionary<WeaponAttributeSuit, int> allData = GameManager.Instance.WeaponAttributeManagers.GetAllWeaponDataInfo();
        List<KeyValuePair<WeaponAttributeSuit, int>> allDataList = allData.ToList();
        List<int> keysToKeep = new List<int>();

        for (int i = 0; i <allDataList.Count; i++)
        {
            if (allDataList[i].Key.WeaponAttributePerLevel.Any(weaponAttribute => allDataList[i].Value + 1 == weaponAttribute.Level))
            {
                keysToKeep.Add(i);
            }
        }
        
        //Find 3 random element
        int amountNeed = Mathf.Min(buttonUpgradeWeapons.Length,keysToKeep.Count) ;
        
        //Show money mode cause no more upgrade available
        if (amountNeed == 0)
        {
            foreach (var buttonUpgrade in buttonUpgradeWeapons)
            {
                buttonUpgrade.gameObject.SetActive(true);
                buttonUpgrade.InitMoneyState(Constant.MoneyReceiveInUpgrade);
            }
        }
        //Show weapon upgrade
        else
        {
            
            // Shuffle the array indices
            Shuffle(keysToKeep);

            // Select the first three shuffled indices
            int[] randomIndex  = keysToKeep.Take(amountNeed).ToArray();
        
            //Turn it into UI

            int differentBetweenUIAndData = Mathf.Abs(randomIndex.Length - buttonUpgradeWeapons.Length) ;

            for (int i = 0; i < differentBetweenUIAndData; i++)
            {
                buttonUpgradeWeapons.Last().gameObject.SetActive(false);
            }

            int indexOfRandom = 0;
            foreach (var buttonUpgrade in buttonUpgradeWeapons)
            {
                if (!buttonUpgrade.gameObject.activeInHierarchy) continue;
            
                buttonUpgrade.InitUI(allDataList[randomIndex[indexOfRandom]]);
                indexOfRandom++;
            }

        }
        
        return;

        void Shuffle(List<int> array)
        {
            for (int i = array.Count - 1; i > 0; i--)
            {
                int randomI = Random.Range(0, i + 1);
                (array[i], array[randomI]) = (array[randomI], array[i]);
            }
        }
    }


    
}
