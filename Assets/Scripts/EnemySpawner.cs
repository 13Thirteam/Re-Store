using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public List<int> spawnTimes;
    [SerializeField] Vector2 spawnLocation;
    int index;


    private void Start()
    {
        spawnTimes.Sort();
    }

    void Update()
    {
        for(int i = index; i < spawnTimes.Count; i++)
        {
            if (spawnTimes[i] == (int)Time.timeSinceLevelLoad)
            {
                Instantiate(enemy, spawnLocation, Quaternion.identity);
                index++;
            }
            else break;
        }
    }
}
