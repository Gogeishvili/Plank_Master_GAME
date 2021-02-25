using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetActiveAndDeactive(bool flag)
    {
        meshRenderer.enabled = flag;

    }

    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }
}
