using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshManager : Singleton<NavMeshManager>
{
    [SerializeField] NavMeshSurface NavMesh;
    public void BakenavMesh()
    {
        NavMesh.BuildNavMesh();
    }
}
