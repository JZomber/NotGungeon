using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    private EnemyScript[] enemyScripts;
    private ModularRooms[] modularRooms;
    private EnemyMage[] enemyMages;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private int maxSpawnersAmount;
    
    [SerializeField] private List<EnemyPoolClass> enemyClass;
    private List<GameObject> enemyPool;
    private List<GameObject> spawnerPool;
    private int activeEnemyAmount;

    public event Action<GameObject> OnMageCalled;
    public static event Action OnRoomCompleted;
    public event Action OnEnemyDespawn;
    
    private void Awake()
    {
        enemyPool = new List<GameObject>();
        spawnerPool = new List<GameObject>();
        
        foreach (var poolItem in enemyClass)
        {
            for (int i = 0; i < poolItem.initialAmount; i++)
            {
                GameObject newEnemy = Instantiate(poolItem.enemyPrefab);
                newEnemy.SetActive(false);
                enemyPool.Add(newEnemy);

                EnemyScript enemyScript = newEnemy.GetComponent<EnemyScript>();
                EnemyMage enemyMage = newEnemy.GetComponent<EnemyMage>();

                if (enemyScript != null)
                {
                    enemyScript.OnEnemyKilled += HandlerEnemyKilled;
                    enemyScript.OnEnemyRevived += HandlerEnemyRevived;
                    //Debug.Log($"Subscripto al enemigo {enemyScript.GameObject()}");
                }
                else if (enemyMage != null)
                {
                    enemyMage.OnMageKilled += HandlerEnemyKilled;
                    //Debug.Log("Subscripto al mago");
                }
            }
        }

        for (int i = 0; i < maxSpawnersAmount; i++)
        {
            GameObject newSpawner = Instantiate(enemySpawner);
            newSpawner.SetActive(false);
            spawnerPool.Add(newSpawner);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        modularRooms = FindObjectsOfType<ModularRooms>();

        foreach (ModularRooms rooms in modularRooms)
        {
            rooms.OnSpawnEnemiesRequest += HandleSpawnEnemies;
            //Debug.Log("Subscripto a SpawnRequest");
        }
    }

    private void HandleSpawnEnemies(List<GameObject> enemies, List<Transform> spawns)
    {
        SpawnersInitialize(spawns);
        StartCoroutine(EnemySpawner(enemies, spawns, 1.25f));
    }

    private void SpawnersInitialize(List<Transform> spawns)
    {
        for (int i = 0; i < spawns.Count; i++)
        {
            GameObject spawner = GetSpawnerFromPool();
            spawner.transform.position = new Vector3(spawns[i].position.x, spawns[i].position.y + 0.5f);
            spawner.SetActive(true);
        }
    }

    private IEnumerator EnemySpawner(List<GameObject> enemies, List<Transform> spawns, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = GetEnemyFromPool(enemies[i]);
            enemy.transform.position = spawns[i].position;
            enemy.SetActive(true);
            //enemy.GetComponent<EnemyScript>().OnEnemyKilled += HandlerEnemyDeath;
        }

        activeEnemyAmount = enemies.Count;
        //Debug.Log($"Objetivo de enemigos {activeEnemyAmount}");
    }

    private GameObject GetSpawnerFromPool()
    {
        for (int i = 0; i < spawnerPool.Count; i++)
        {
            if (!spawnerPool[i].activeInHierarchy)
            {
                return spawnerPool[i];
            }
        }

        GameObject newSpawner = Instantiate(enemySpawner);
        spawnerPool.Add(newSpawner);
        return newSpawner;
    }

    private GameObject GetEnemyFromPool(GameObject prefab)
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy && enemyPool[i].name.Contains(prefab.name))
            {
                return enemyPool[i];
            }
        }

        GameObject newEnemy = Instantiate(prefab);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }

    private IEnumerator DeactivateEnemies(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        for (int i = 0; i < enemyPool.Count; i++)
        {
            GameObject enemy = enemyPool[i].GameObject();
            
            if (enemy.activeInHierarchy)
            {
                enemy.SetActive(false);
            }
        }
    }

    private IEnumerator DespawnEnemies(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnEnemyDespawn?.Invoke();
    }
    
    private void HandlerEnemyKilled(GameObject obj)
    {
        //Debug.Log($"ENEMIGO {obj} ELIMINADO");

        OnMageCalled?.Invoke(obj);
        activeEnemyAmount--;

        if (activeEnemyAmount == 0)
        {
            OnRoomCompleted?.Invoke();
            StartCoroutine(DespawnEnemies(1f));
            StartCoroutine(DeactivateEnemies(2f));
        }
    }

    private void HandlerEnemyRevived()
    {
        activeEnemyAmount++;
    }

    private void UnsubscribeFromEvents()
    {
        //OnSpawnEnemies -= HandleSpawnEnemies;
        if (modularRooms != null)
        {
            foreach (ModularRooms rooms in modularRooms)
            {
                rooms.OnSpawnEnemiesRequest -= HandleSpawnEnemies;
            }
        }

        if (enemyPool != null)
        {
            foreach (GameObject enemyObject in enemyPool)
            {
                if (enemyObject != null)
                {
                    EnemyScript enemyScript = enemyObject.GetComponent<EnemyScript>();
                    EnemyMage enemyMage = enemyObject.GetComponent<EnemyMage>();

                    if (enemyScript != null)
                    {
                        enemyScript.OnEnemyKilled -= HandlerEnemyKilled;
                        enemyScript.OnEnemyRevived -= HandlerEnemyRevived;
                        //Debug.Log($"Desubscripto al enemigo {enemyScript.GameObject()}");
                    }
                    else if (enemyMage != null)
                    {
                        enemyMage.OnMageKilled -= HandlerEnemyKilled;
                        //Debug.Log("Desubscripto al mago");
                    }
                    
                }
            }
        }
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void OnApplicationQuit()
    {
        UnsubscribeFromEvents();
    }

    [Serializable]
    public class EnemyPoolClass
    {
        public GameObject enemyPrefab;
        public int initialAmount;
    }
}
