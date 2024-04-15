using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to particle
public class ReturnParticleToPool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectsPoolManager.ReturnObjectToPool(gameObject);
    }
}
