using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{     

    [SerializeField] Transform _plankPrototype;
    public bool isActive=true;

    [SerializeField] MeshRenderer meshRenderer=null;
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

    public void Drop()
    {
        var _p=Instantiate(_plankPrototype);
        _p.transform.position=transform.position;
        _p.gameObject.AddComponent<BoxCollider>().isTrigger=true;
        _p.gameObject.AddComponent<Rigidbody>();

    }
}
