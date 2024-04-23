using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectsPoolManager : MonoBehaviour
{
    //List of different pool
    public static List<PoolObjectInfo> ObjectPools = new();
    private static GameObject _particleSystemParent;
    private static GameObject _bulletSystemParent;
    private static GameObject _othersSystemParent;


    public enum PoolType
    {
        ParticleSystem,
        Projectile,
        Others
    }

    private void Awake()
    {
        SetupEmpties();
    }

    //Create parent object to help organize pool objects
    private void SetupEmpties()
    {
        _particleSystemParent = new GameObject("Particle Effect Pool");
        _bulletSystemParent = new GameObject("Bullets Pool");
        _othersSystemParent = new GameObject("Others GameObjects Pool");
        
        _particleSystemParent.transform.SetParent(gameObject.transform);
        _bulletSystemParent.transform.SetParent(gameObject.transform);
        _othersSystemParent.transform.SetParent(gameObject.transform);
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemParent;
            case PoolType.Projectile:
                return _bulletSystemParent;
            default:
                return _othersSystemParent;
        }
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.Others )
    {
        //Find that pool 
        PoolObjectInfo pool = null;
        foreach (var VARIABLE in ObjectPools)
        {
            //Found it
            if (objectToSpawn.name == VARIABLE.LookUpString)
            {
                pool = VARIABLE;
                break;
            }
        }
        
        //Pool not found => Create it
        if (pool == null)
        {
            //Create it
            pool = new PoolObjectInfo
            {
                LookUpString = objectToSpawn.name
            };
            
            //Add it to pool manager
            ObjectPools.Add(pool);
            
        }
        
        //Check any inactive object can be used
        GameObject spawnedObject = null;
        foreach (var VARIABLE in pool.InActiveObjects)
        {
            //Found one
            if (VARIABLE != null)
            {
                spawnedObject = VARIABLE;
                break;
            }
        }

        //Nothing is inactive to use so create complete new
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            
            //Set parent to look nice :)
            spawnedObject.transform.SetParent(SetParentObject(poolType).transform);
        }
        //Actually find inactive object in pool to use
        else
        {
            //Init it
            spawnedObject.transform.position = spawnPosition;
            spawnedObject.transform.rotation = spawnRotation;
            //Remove it from inactive list
            pool.InActiveObjects.Remove(spawnedObject);
            //Turn it on
            spawnedObject.SetActive(true);
        }

        return spawnedObject;
    }
    
    //Overload the first spawn => This is for if want to have specific parent
    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform )
    {
        //Find that pool 
        PoolObjectInfo pool = null;
        foreach (var VARIABLE in ObjectPools)
        {
            //Found it
            if (objectToSpawn.name == VARIABLE.LookUpString)
            {
                pool = VARIABLE;
                break;
            }
        }
        
        //Pool not found => Create it
        if (pool == null)
        {
            //Create it
            pool = new PoolObjectInfo
            {
                LookUpString = objectToSpawn.name
            };
            
            //Add it to pool manager
            ObjectPools.Add(pool);
            
        }
        
        //Check any inactive object can be used
        GameObject spawnedObject = null;
        foreach (var VARIABLE in pool.InActiveObjects)
        {
            //Found one
            if (VARIABLE != null)
            {
                spawnedObject = VARIABLE;
                break;
            }
        }

        //Nothing is inactive to use so create complete new
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(objectToSpawn, parentTransform);
            
        }
        //Actually find inactive object in pool to use
        else
        {
            //Remove it from inactive list
            pool.InActiveObjects.Remove(spawnedObject);
            //Turn it on
            spawnedObject.SetActive(true);
        }

        return spawnedObject;
    }
    
    
    
    //Call this instead of just delete the damn thing
    public static void ReturnObjectToPool(GameObject obj)
    {
        //Kinda bad here but ...
        string goName = obj.name.Substring(0, obj.name.Length - 7); //Remove the "(clone)" part of game object name
        
        //Find that pool 
        PoolObjectInfo pool = null;
        foreach (var VARIABLE in ObjectPools)
        {
            //Found it
            if (goName == VARIABLE.LookUpString)
            {
                pool = VARIABLE;
                break;
            }
        }

        if (pool == null)
        {
            Debug.Log("Try to Delete Object not created in pool yet");
            return;
        }
        
        //Turn it off
        obj.SetActive(false);
        
        //Add it to inactive list for reused
        pool.InActiveObjects.Add(obj);
        
    }
}



//Pool of this single object
public class PoolObjectInfo
{
    public string LookUpString;
    public List<GameObject> InActiveObjects = new();
}