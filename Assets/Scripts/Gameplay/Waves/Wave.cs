using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [HideInInspector]
    public string name;
    public List<WaveInstruction> instructions;
}
