using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(WaveManager))]
public class WaveManagerEditor : Editor
{
    List<Wave> waves;
    public override void OnInspectorGUI()
    {
        waves = ((WaveManager)target).waves;

        IndexWavesAndInstructions();
        
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("waves"), true);
        serializedObject.ApplyModifiedProperties();
    }

    void AddWave()
    {
        waves.Add(new Wave());
    }
    void RemoveWave()
    {
        if (waves.Count > 0)
            waves.RemoveAt(waves.Count - 1);
    }

    void IndexWavesAndInstructions()
    {
        for(int i=0; i<waves.Count; i++)
        {
            waves[i].name = "Wave " + (i+1);

            for(int j=0; j<waves[i].instructions.Count; j++)
            {
                waves[i].instructions[j].name = "Instruction " + (j+1);
            }
        }
    }
}
