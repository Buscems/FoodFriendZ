using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemySpawner : MonoBehaviour
{

    public GameObject SpawnerObject;
    public GameObject SpawnEnemy;
    private GameObject[] BasicEnemy;

    public int SpawnAmount;
    public int CurrentAmount;

    List<GameObject> EnemyCount;


    void Start()
    {
        Spawn();
    }

    


    void Update()
    {

        BasicEnemy = GameObject.FindGameObjectsWithTag("BasicEnemy");
        CurrentAmount = BasicEnemy.Length;

        if (CurrentAmount == 0)
        {
            SpawnAmount += 1;
            Instantiate(SpawnEnemy, transform.position, Quaternion.identity);
        }
    }


    void Spawn()
    {
        if (CurrentAmount >= SpawnAmount)
        {
            Instantiate(SpawnEnemy, transform.position, Quaternion.identity);
            CurrentAmount += 1;
        }
    }
}
