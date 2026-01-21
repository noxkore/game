using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class BakeNavMesh2D : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void Bake()
    {
        navMeshSurface.BuildNavMesh();
    }
}
