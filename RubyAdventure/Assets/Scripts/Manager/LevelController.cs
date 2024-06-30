using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private MeleeSoldier meleeSoldierPrefab;
    [SerializeField] private RangeSoldier rangeSoldierPrefab;
    [Header("Boss")]
    [SerializeField] private BossBug bossBug;
    [SerializeField] private ParticleSystem bossPreAppearEffect;
    [SerializeField] private ParticleSystem bossAppearEffect;
    [SerializeField] private Transform bossSpawnPosition;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timeCountDownUI;
    [SerializeField] private int timeInLevel;

    [Header("Spawn Config")] [SerializeField]
    private Vector2 spawnInterval; // how many seconds between 2 enemy
    [SerializeField] private float percentageMelee = 0.7f;
    [SerializeField] private float percentageRange = 0.3f;


    [SerializeField] private Transform centerSpawnPoint;
    [SerializeField] private Vector2 spawnAreaSize;

    private Coroutine _coroutineSpawnEnemy;
    private Coroutine _coroutineWinLevel;
    private bool _isSpawning;

    private Action<object> _onStartLevelRef;

    
    // Start is called before the first frame update
    private void OnDisable()
    {
        _isSpawning = false;
    }

    private void OnEnable()
    {
        timeCountDownUI.text = "Preparing";
        
        //Event
        _onStartLevelRef = _ => StartLevel();
        EventDispatcher.Instance.RegisterListener(EventID.OnStartLevel,_onStartLevelRef);
    }


    private IEnumerator C_Spawn()
    {
        yield return new WaitForSeconds(1f);
        while (_isSpawning)
        {
            float random = Random.Range(0.1f, 1f);
            //Get Game Object base on random
            var resultEnemy = random <= percentageMelee ? meleeSoldierPrefab.gameObject : rangeSoldierPrefab.gameObject;

            //Spawn it out
            ObjectsPoolManager.SpawnObject(resultEnemy,
                RandomPointInArea(centerSpawnPoint.position, spawnAreaSize.x, spawnAreaSize.y),
                Quaternion.identity, ObjectsPoolManager.PoolType.Enemy);
            yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y + 1));
        }
    }

    private IEnumerator C_CountDownToBoss()
    {
        // TimeSpan timeLeft = new TimeSpan(0, 0, timeInLevel, 0); //minutes
        //Cheat
        TimeSpan timeLeft = new TimeSpan(0, 0, timeInLevel, 0); //minutes

        while (timeLeft.Seconds >= 0)
        {
            string formattedTime = $"{timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}";
            timeCountDownUI.text = formattedTime;

            timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 0, 1));
            yield return new WaitForSeconds(1f);
        }

        //Stop spawning
        _isSpawning = false;

        StartCoroutine(C_SpawnBoss());

        Debug.Log("Level ended");
    }

    private void StartLevel()
    {
        Debug.Log("@Enemy spawner: Start Level Here");

        //Spawn enemy
        _isSpawning = true;
        _coroutineSpawnEnemy = StartCoroutine(C_Spawn());

        //Finish level when spawn done
        _coroutineWinLevel = StartCoroutine(C_CountDownToBoss());
    }

    private Vector2 RandomPointInArea(Vector3 center, float sizeX, float sizeY)
    {
        return center + new Vector3(
            (Random.value - 0.5f) * sizeX,
            (Random.value - 0.5f) * sizeY
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(centerSpawnPoint.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y));
    }

    private IEnumerator C_SpawnBoss()
    {
        //Spawn Effects
        ParticleSystem preAppearEffect = ObjectsPoolManager.SpawnObject(bossPreAppearEffect.gameObject, bossSpawnPosition.position, Quaternion.identity,
            ObjectsPoolManager.PoolType.ParticleSystem).GetComponent<ParticleSystem>();

        yield return new WaitForSeconds(2f);
        preAppearEffect.Stop();
        
        ObjectsPoolManager.SpawnObject(bossAppearEffect.gameObject, bossSpawnPosition.position, Quaternion.identity,
            ObjectsPoolManager.PoolType.ParticleSystem).GetComponent<ParticleSystem>();
        
        yield return new WaitForSeconds(0.2f);
        
        //Spawn it out
        ObjectsPoolManager.SpawnObject(bossBug.gameObject,
            bossSpawnPosition.position, 
            Quaternion.identity, ObjectsPoolManager.PoolType.Enemy);
    }
}