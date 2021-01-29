using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemySoldierPrefab;
    public GameObject machineGunnerPrefab;

    public List<GameObject> EnemiesInstance;

    public GameObject player;
    public GameObject forrest1;

    private float _spawnRangeX = 10f;
    private float _spawnRangeY = 5f;

    private float _startDelay = 2f;
    private float _spawnInterval = 1f;

    private int enemyCount; 

    Vector2 spawnPosition;

    float neglimitX;
    float posLimitX;
    float neglimitY;
    float posLimitY;

    // Start is called before the first frame update
    void Start()
    {
                      
        InvokeRepeating("SpawnEnemySoldier", _startDelay, _spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        neglimitX = player.transform.position.x - _spawnRangeX;
        posLimitX = player.transform.position.x + _spawnRangeX;
        neglimitY = player.transform.position.y - _spawnRangeY;
        posLimitY = player.transform.position.y + _spawnRangeY;

        spawnPosition = new Vector2(Random.Range(neglimitX, posLimitX),Random.Range(neglimitY, posLimitY));

        enemyCount = EnemiesInstance.Count;
    }

    void SpawnEnemySoldier()
    {
        if (enemyCount >= 10)
        {
            return;
        }
        else
        {
            GameObject soldier = new GameObject();
            soldier = Instantiate(enemySoldierPrefab, spawnPosition, enemySoldierPrefab.transform.rotation);
            EnemiesInstance.Add(soldier);   
        }
    }
}
