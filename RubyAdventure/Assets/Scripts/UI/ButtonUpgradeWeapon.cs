using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI levelOrUnlock;
    [SerializeField] private Image weaponIcon;
    [Header("Level")]
    [SerializeField] private TextMeshProUGUI previousLevel;
    [SerializeField] private Image levelArrowIcon;
    [SerializeField] private TextMeshProUGUI AfterLevel;
    [Header("Speed")]
    [SerializeField] private TextMeshProUGUI previousSpeed;
    [SerializeField] private Image speedArrowIcon;
    [SerializeField] private TextMeshProUGUI AfterSpeed;
    [Header("Power")]
    [SerializeField] private TextMeshProUGUI previousPower;
    [SerializeField] private Image powerArrowIcon;
    [SerializeField] private TextMeshProUGUI AfterPower;
    [Header("Sprites For Weapon")] [SerializeField]
    private Sprite pistolSprite;
    [SerializeField]
    private Sprite rifleSprite;
    [SerializeField]
    private Sprite spearSprite;

    [Header("Button")] [SerializeField] private Button button;
    public void InitUI(KeyValuePair<WeaponAttributeSuit,int> pairValue)
    {
        //Name
        weaponName.text = pairValue.Key.Type.ToString();
        
        //Set button
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            GameManager.Instance.WeaponAttributeManagers.OnWeaponUpgrade(pairValue.Key.Type);
        });
        
        if (pairValue.Value == 0)
        {
            //Unlock State
            levelOrUnlock.text = "New!";
            
            //Set text
            WeaponAttribute attribute = pairValue.Key.FindAttributeWithLevel(pairValue.Value);
            AfterLevel.text = attribute.Level.ToString();
            AfterSpeed.text = attribute.FireRate.ToString("0.0");
            AfterPower.text = attribute.DamagePerAttack.ToString();
            
            //Turn off previous and icon
            previousLevel.gameObject.SetActive(false);
            previousSpeed.gameObject.SetActive(false);
            previousPower.gameObject.SetActive(false);
            levelArrowIcon.gameObject.SetActive(false);
            speedArrowIcon.gameObject.SetActive(false);
            powerArrowIcon.gameObject.SetActive(false);
        }
        else
        {
            //Unlock State
            levelOrUnlock.text = "+1 Level";
            
            //Turn on UI
            previousLevel.gameObject.SetActive(true);
            previousSpeed.gameObject.SetActive(true);
            previousPower.gameObject.SetActive(true);
            levelArrowIcon.gameObject.SetActive(true);
            speedArrowIcon.gameObject.SetActive(true);
            powerArrowIcon.gameObject.SetActive(true);
            
            WeaponAttribute attributeBefore = pairValue.Key.FindAttributeWithLevel(pairValue.Value);

            //Set text for before level
            previousLevel.text = attributeBefore.Level.ToString();
            previousSpeed.text = attributeBefore.FireRate.ToString("0.0");
            previousPower.text = attributeBefore.DamagePerAttack.ToString();
            
            //Set text for after level
            WeaponAttribute attributeAfter = pairValue.Key.FindAttributeWithLevel(pairValue.Value+1);
            AfterLevel.text = attributeAfter.Level.ToString();
            AfterSpeed.text = attributeAfter.FireRate.ToString("0.0");
            AfterPower.text = attributeAfter.DamagePerAttack.ToString();
            

            
        }
        
        
    }

}
