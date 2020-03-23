using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
public class PathManager : MonoBehaviour
{
    public static event NewPathCalculated onNewPathCalculated;
    public static PathManager instance;
    public NavMeshSurface surfaceMesh;
    public NavMeshAgent realTimeAgent;
    public NavMeshAgent preBuildAgent;
    public PathFollower follower;
    NavMeshPath realTimePath;
    NavMeshPath preBuildPath;
    //public bool PrebuiltPathIsPossible { get; private set; }

    [SerializeField]Transform destination;

    IEnumerator generateNewNavMeshPath;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            surfaceMesh.BuildNavMesh();
            realTimePath = new NavMeshPath();
            preBuildPath = new NavMeshPath();
            generateNewNavMeshPath = CalculatePaths();
            NavMesh.CalculatePath(transform.position, destination.position, preBuildAgent.areaMask, preBuildPath);
            NavMesh.CalculatePath(transform.position, destination.position, realTimeAgent.areaMask, realTimePath);
        }
    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.C))
        {
            StopCoroutine(generateNewNavMeshPath);
            generateNewNavMeshPath = CalculatePaths();
            StartCoroutine(generateNewNavMeshPath);
        }
        for (int i = 0; i < preBuildPath.corners.Length - 1; i++)
            Debug.DrawLine(preBuildPath.corners[i], preBuildPath.corners[i + 1], Color.red);

        for (int i = 0; i < realTimePath.corners.Length - 1; i++)
            Debug.DrawLine(realTimePath.corners[i], realTimePath.corners[i + 1], Color.green);
    }
    public void CalculateNewPaths()
    {
        StopCoroutine(generateNewNavMeshPath);
        generateNewNavMeshPath = CalculatePaths();
        StartCoroutine(generateNewNavMeshPath);
    }
    IEnumerator CalculatePaths()
    {
        bool isNewPathViable;
        surfaceMesh.BuildNavMesh();
        NavMesh.CalculatePath(transform.position, destination.position, preBuildAgent.areaMask, preBuildPath);
        NavMesh.CalculatePath(transform.position, destination.position, realTimeAgent.areaMask, realTimePath);



        if (preBuildPath.status == NavMeshPathStatus.PathComplete)
        {
            Debug.LogWarning("New Path Available");
            isNewPathViable = true;
        }
        else
        {
            isNewPathViable = false;
            Debug.LogWarning("Path Impossible");
        }
        
       if(onNewPathCalculated!=null)
        {
            onNewPathCalculated(isNewPathViable);
        }

        yield return null;
    }
}
