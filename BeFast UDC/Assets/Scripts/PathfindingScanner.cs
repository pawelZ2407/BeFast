using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PathfindingScanner : MonoBehaviour
{
    [SerializeField] float scanTickTime;

    void Start()
    {
        StartCoroutine(Scanner());     
    }

    IEnumerator Scanner()
    {
        while (true)
        {
            yield return new WaitForSeconds(scanTickTime);
            AstarPath.active.Scan();
        }

    }
}
