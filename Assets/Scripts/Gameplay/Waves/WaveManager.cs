using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static int enemyCountForLevel = 0;
    public static int enemyCountForThisWave = 0;

    public float timeBetweenWaves;
    public Transform spawnPosition;
    public List<Wave> waves;

    private int waveIndex;
    private List<GameObject> enemyPool;

    private void Start()
    {
        waveIndex = 0;
        BuildEnemyPool();
        CountEnemiesForIncomingWave();
        StartCoroutine(LaunchWave());
    }

    void BuildEnemyPool()
    {
        enemyPool = new List<GameObject>();
        foreach(Wave wave in waves)
        {
            foreach(WaveInstruction instruction in wave.instructions)
            {
                for(int i=0; i<instruction.amountToSpawn; i++)
                {
                    if (instruction.enemyPrefab != null)
                    {
                        enemyPool.Add(Instantiate(instruction.enemyPrefab));
                        enemyCountForLevel++;
                    }
                }
            }
        }
    }
    void CountEnemiesForIncomingWave()
    {
        enemyCountForThisWave = 0;
        Wave currentWave = waves[waveIndex];
        foreach (WaveInstruction instruction in currentWave.instructions)
        {
            if(instruction.enemyPrefab!=null)
                enemyCountForThisWave += instruction.amountToSpawn;
        }

        //Debug.LogWarning("Expecting " + enemyCountForThisWave + " enemies");
    }

    IEnumerator LaunchWave()
    {
        Wave currentWave = waves[waveIndex];
        foreach(WaveInstruction instruction in currentWave.instructions)
        {
            yield return new WaitForSeconds(instruction.executionDelayTime);
            if(instruction.enemyPrefab!=null)
            {
                for(int i=0; i<instruction.amountToSpawn; i++)
                {
                    enemyPool[0].SetActive(true);
                    enemyPool.RemoveAt(0);
                    yield return new WaitForSeconds(instruction.spawnIntervalTime);
                }
            }
            yield return null;
        }
    }
}
