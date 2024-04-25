using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnSpots;
    [SerializeField] private float spawnInterval; // how many seconds between 2 enemy
    [SerializeField] private MeleeSoldier meleeSoldierPrefab;

    private bool _isSpawning;
    // Start is called before the first frame update
    private void Start()
    {
        _isSpawning = true;
        StartCoroutine(C_Spawn());
    }

    private void OnDisable()
    {
        _isSpawning = false;
    }

    private IEnumerator C_Spawn()
    {
        while (_isSpawning)
        {
            int randomIndex = Random.Range(0, spawnSpots.Length);
            ObjectsPoolManager.SpawnObject(meleeSoldierPrefab.gameObject, spawnSpots[randomIndex].position,
                Quaternion.identity, ObjectsPoolManager.PoolType.Enemy);
            yield return new WaitForSeconds(spawnInterval) ;
        }
    } 
}
