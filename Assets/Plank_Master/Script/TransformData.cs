using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TransformData
{
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;

    public TransformData(Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
    {
        this.localPosition = localPosition;
        this.localRotation = localRotation;
        this.localScale = localScale;
    }
}
