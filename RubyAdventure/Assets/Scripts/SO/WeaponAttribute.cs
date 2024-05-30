using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponAttribute
{
    [SerializeField]
    private int level;
    public int Level => level;
    [SerializeField]
    private float fireRate;
    public float FireRate => fireRate;
    [SerializeField]
    private int damagePerAttack;
    public int DamagePerAttack => damagePerAttack;
}
