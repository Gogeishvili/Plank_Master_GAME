using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{

    [SerializeField] Transform _plankPrototype;
    public bool isActive = true;

    [SerializeField] MeshRenderer meshRenderer = null;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void SetActiveAndDeactive(bool flag,Color color)
    {
        if (flag == true)
        {
            meshRenderer.enabled = true;
            meshRenderer.material.SetColor("_Color",color);
        }
        meshRenderer.enabled = flag;
    }

    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }

    public void Drop(Color color)
    {
        var _p = Instantiate(_plankPrototype);
        _p.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        _p.transform.position = transform.position;
        _p.gameObject.AddComponent<BoxCollider>().isTrigger = true;
        _p.gameObject.AddComponent<Rigidbody>();

    }
}
