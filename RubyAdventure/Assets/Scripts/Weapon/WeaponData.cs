using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is use When weapon need to listen to event receive upgrade
/// </summary>
public class WeaponData
{
    public WeaponType Type { get; private set; }
    public WeaponAttribute Attribute{ get; private set; }

    public WeaponData(WeaponType type, WeaponAttribute attribute)
    {
        Type = type;
        Attribute = attribute;
    }
}
