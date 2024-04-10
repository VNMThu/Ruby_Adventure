using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected float rangeToDetectEnemies;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float damageEachShot;
    [SerializeField] protected string detectionTag;
    [SerializeField] protected GameObject bullet;
    protected Transform currentTarget;
    protected virtual void FireBullet()
    {
        //Fire off this bullet - Passing in target here
        Instantiate(bullet);
    }

    protected virtual IEnumerator C_FiringShots()
    {
        //Call fire
        FireBullet();
        yield return new WaitForSeconds(1f / fireRate);
    }
    protected virtual void OnDetectEnemies(){}

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(detectionTag)) return;
        
        //Fire
        currentTarget = other.transform;
        StartCoroutine(C_FiringShots());
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(detectionTag)) return;
        
        //Fire
        StopCoroutine(C_FiringShots());
        currentTarget = null;
    }
}
