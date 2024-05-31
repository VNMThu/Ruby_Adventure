using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAttribute", menuName = "Weapon/WeaponAttributeSuit", order = 1)]
public class WeaponAttributeSuit : ScriptableObject
{
    [SerializeField] private WeaponType type;
    public WeaponType Type => type;
    [SerializeField]
    private WeaponAttribute[] _list;
    public WeaponAttribute[] WeaponAttributePerLevel=>_list;

    public WeaponAttribute FindAttributeWithLevel(int weaponLevel)
    {
        return _list.FirstOrDefault(VARIABLE => VARIABLE.Level == weaponLevel);
    }
}

