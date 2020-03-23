using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PathFollower : MonoBehaviour
{
    NavMeshAgent NMAgent;

    private void Start()
    {
        NMAgent = GetComponent<NavMeshAgent>();
    }
    public void StartMove(NavMeshPath path)
    {
        NMAgent.SetPath(path);
        NMAgent.isStopped = false;
    }
}
