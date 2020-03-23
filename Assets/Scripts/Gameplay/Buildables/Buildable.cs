using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    [SerializeField] bool locked = false;
    [SerializeField] float buildCost = 20;
    [SerializeField] float upgradeCost = 100;
    [SerializeField] Buildable upgradedBuildable;
}
