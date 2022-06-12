using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonoTest : MonoBehaviour
{
    void Start()
    {
        AstarData data = AstarPath.active.data;
        NavGraph graph = AstarPath.active.graphs.First();
        Debug.Log(true);
    }
}
