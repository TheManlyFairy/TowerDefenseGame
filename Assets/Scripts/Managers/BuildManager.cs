using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] Grid grid;
    [SerializeField] List<Buildable> buildables;
    Dictionary<Buildable, Vector3> builtObjects;
    private void Start()
    {
        instance = this;
        buildables = new List<Buildable>();
        buildables.AddRange(FindObjectsOfType<Buildable>());
    }
    void BuildDictionary()
    {
        builtObjects = new Dictionary<Buildable, Vector3>();
        foreach (Buildable builtObject in buildables)
        {
            Vector3 gridPosition = grid.WorldToCell(builtObject.transform.position);
            builtObjects.Add(builtObject, gridPosition);
        }
    }

    public static void MoveObjectToGridPoint(Vector3 worldPoint)
    {
        Vector3 XZYWorldPosition = new Vector3(worldPoint.x, worldPoint.z, worldPoint.y);
        Vector3 gridPoint = instance.grid.WorldToCell(XZYWorldPosition);
        instance.buildables[0].transform.position = new Vector3(gridPoint.x, 0.5f, gridPoint.z);
    }
    Vector3Int RoundUpXYZVector3(Vector3 unroundedVector)
    {
        Vector3Int RoundedXYZVector3 = Vector3Int.zero;
        float x = unroundedVector.x;
        float y = unroundedVector.y;
        float z = unroundedVector.z;

        return RoundedXYZVector3;
    }
    public static Vector3 GetGridPoint(Vector3 worldPoint)
    {
        Vector3Int XZYWorldPosition = new Vector3Int((int)worldPoint.x, (int)worldPoint.z, (int)worldPoint.y);
        Vector3Int gridCellPoint = instance.grid.WorldToCell(XZYWorldPosition);
        Vector3 gridCellCenter = instance.grid.GetCellCenterLocal(XZYWorldPosition);
        /*Debug.LogWarning("raycast hit point " + worldPoint); 
        Debug.Log("XZYWorldPosition " + XZYWorldPosition);
        Debug.Log("gridCellPoint " + gridCellPoint);
        Debug.Log("gridCellCenter " + gridCellCenter);*/

        return new Vector3(gridCellCenter.x,0.5f, gridCellCenter.z);
        return new Vector3(gridCellPoint.x, 0.5f, gridCellPoint.z);
    }
}
