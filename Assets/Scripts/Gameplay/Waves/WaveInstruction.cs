using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveInstruction
{
    [HideInInspector]
    public string name;
    public GameObject enemyPrefab;
 
    public int amountToSpawn;
    [Tooltip("Time to wait between individual enemy spawns")]
    public float spawnIntervalTime;
    [Tooltip("Time to wait before launching this instruction")]
    public float executionDelayTime;
    
    
}
