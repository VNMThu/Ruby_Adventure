using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [Header("Weapon Icon")]
    [SerializeField] private GameObject levelTitleText;
    [SerializeField] private GameObject newWeaponText;
    [Header("Weapon Icon")]
    [SerializeField] private Image weaponIcon;
    [Header("Level")]
    [SerializeField] private TextMeshProUGUI previousLevel;
    [SerializeField] private Image levelArrowIcon;
    [SerializeField] private TextMeshProUGUI afterLevel;
    [Header("Speed")]
    [SerializeField] private TextMeshProUGUI previousSpeed;
    [SerializeField] private Image speedArrowIcon;
    [SerializeField] private TextMeshProUGUI afterSpeed;
    [Header("Power")]
    [SerializeField] private TextMeshProUGUI previousPower;
    [SerializeField] private Image powerArrowIcon;
    [SerializeField] private TextMeshProUGUI afterPower;
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
        Debug.Log("Weapon Upgrade UI:"+weaponName.text);
        
        //Weapon icon
        weaponIcon.sprite = pairValue.Key.Type switch
        {
            WeaponType.Pistol => pistolSprite,
            WeaponType.Spear => spearSprite,
            WeaponType.Rifle => rifleSprite,
            _ => throw new ArgumentOutOfRangeException()
        };

        //Set button
        button.onClick.RemoveAllListeners();

        
        if (pairValue.Value == 0)
        {
            //Unlock State
            levelTitleText.gameObject.SetActive(false);
            newWeaponText.gameObject.SetActive(true);
            
            //Set text
            WeaponAttribute attribute = pairValue.Key.FindAttributeWithLevel(pairValue.Value);
            
            afterPower.gameObject.SetActive(true);
            afterLevel.gameObject.SetActive(true);
            afterSpeed.gameObject.SetActive(true);
            
            afterLevel.text = (attribute.Level+1).ToString();
            afterSpeed.text = Mathf.RoundToInt(attribute.FireRate).ToString();
            afterPower.text = attribute.DamagePerAttack.ToString();
            
            
            Debug.Log("Weapon Upgrade UI 1:"+attribute.Level);
            Debug.Log("Weapon Upgrade UI 1:"+attribute.FireRate);
            Debug.Log("Weapon Upgrade UI 1:"+attribute.DamagePerAttack);

            
            //Turn off previous and icon
            previousLevel.gameObject.SetActive(false);
            previousSpeed.gameObject.SetActive(false);
            previousPower.gameObject.SetActive(false);
            levelArrowIcon.gameObject.SetActive(false);
            speedArrowIcon.gameObject.SetActive(false);
            powerArrowIcon.gameObject.SetActive(false);
            
            //Tick weapon unlock
            button.onClick.AddListener(() =>
            {
                EventDispatcher.Instance.PostEvent(EventID.OnWeaponUnlock,pairValue.Key.Type);
            });
        }
        else
        {
            //Unlock State
            levelTitleText.gameObject.SetActive(true);        
            newWeaponText.gameObject.SetActive(false);
            
            //Turn on UI
            previousLevel.gameObject.SetActive(true);
            previousSpeed.gameObject.SetActive(true);
            previousPower.gameObject.SetActive(true);
            
            WeaponAttribute attributeBefore = pairValue.Key.FindAttributeWithLevel(pairValue.Value);

            Debug.Log("Weapon Upgrade UI 2:"+attributeBefore.Level);
            Debug.Log("Weapon Upgrade UI 2:"+attributeBefore.FireRate);
            Debug.Log("Weapon Upgrade UI 2:"+attributeBefore.DamagePerAttack);
            // previousLevel.text = attributeBefore.Level.ToString();
            // previousSpeed.text = Mathf.RoundToInt(attributeBefore.FireRate).ToString();
            // previousPower.text = attributeBefore.DamagePerAttack.ToString();

            //Set text for after level
            WeaponAttribute attributeAfter = pairValue.Key.FindAttributeWithLevel(pairValue.Value+1);
            // AfterLevel.text = attributeAfter.Level.ToString();
            // AfterSpeed.text = Mathf.RoundToInt(attributeAfter.FireRate).ToString();
            // AfterPower.text = attributeAfter.DamagePerAttack.ToString();

            //Set text for before level
            UpdateAttributeText(previousLevel,afterLevel,levelArrowIcon.gameObject,attributeBefore.Level,attributeAfter.Level);
            UpdateAttributeText(previousSpeed,afterSpeed,speedArrowIcon.gameObject,Mathf.RoundToInt( attributeBefore.FireRate),Mathf.RoundToInt(attributeAfter.FireRate));
            UpdateAttributeText(previousPower,afterPower,powerArrowIcon.gameObject, attributeBefore.DamagePerAttack,attributeBefore.DamagePerAttack);

            //Tick weapon upgrade
            button.onClick.AddListener(() =>
            {
                GameManager.Instance.WeaponAttributeManagers.OnWeaponUpgrade(pairValue.Key.Type);
            });
        }

        return;

        void UpdateAttributeText(TMP_Text previousText,TMP_Text afterText,GameObject iconImage,int before,int after)
        {
            if (after > before)
            {
                previousText.text = before.ToString();
                iconImage.SetActive(true);
                afterText.gameObject.SetActive(true);
                afterText.text = after.ToString();
            }
            else
            {
                previousText.text = before.ToString();
                iconImage.SetActive(false);
                afterText.gameObject.SetActive(false);
            }
        }
        
        
    }

}
