using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{

    [SerializeField] Transform _plankPrototype;
    public bool isActive = true;
    public MeshRenderer meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (GameManager.instance.randomPlankeVersion)
        {
            meshRenderer.enabled = false;
        }
    }
    
    public void SetActiveAndDeactive(bool flag, Color color, Transform trans)
    {
        if (flag == true)
        {
            meshRenderer.enabled = true;
            meshRenderer.material.SetColor("_Color", color);
            if (GameManager.instance.randomPlankeVersion)
            {
                transform.forward = trans.forward;
            }
        }
        meshRenderer.enabled = flag;
    }

    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }

    public void Drop(Color color, Transform trans)
    {

        var _p = Instantiate(_plankPrototype);
        _p.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        _p.transform.position = transform.position;
        if (GameManager.instance.randomPlankeVersion)
        {
            _p.transform.forward = trans.forward;
        }
        _p.gameObject.AddComponent<BoxCollider>().isTrigger = true;
        _p.gameObject.AddComponent<Rigidbody>();

    }
}
