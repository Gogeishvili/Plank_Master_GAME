using System.Collections.Generic;
using UnityEngine;


public class Triangle3D
{
    public Vector3 p0 { get; private set; }
    public Vector3 p1 { get; private set; }
    public Vector3 p2 { get; private set; }
    Vector3 _v0, _v1;
    float _dotv00, _dotv01, _dotv11, _invDenom;
    public Vector3 position { get; private set; }
    public Vector3 normal { get; private set; }
    public Triangle3D(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        this.p0 = p0;
        this.p1 = p1;
        this.p2 = p2;

        _v0 = p2 - p0;
        _v1 = p1 - p0;

        _dotv00 = Vector3.Dot(_v0, _v0);
        _dotv01 = Vector3.Dot(_v0, _v1);
        _dotv11 = Vector3.Dot(_v1, _v1);
        _invDenom = 1f / (_dotv00 * _dotv11 - _dotv01 * _dotv01);

        position = (p0 + p1 + p2) / 3;

        normal = Vector3.Cross(p1 - p0, p2 - p1).normalized;
    }

    
    public bool Contains(Vector3 point)
    {
        Vector3 v2 = point - p0;
        float dotv02 = Vector3.Dot(_v0, v2);
        float dotv12 = Vector3.Dot(_v1, v2);

        float u = (_dotv11 * dotv02 - _dotv01 * dotv12) * _invDenom;
        float v = (_dotv00 * dotv12 - _dotv01 * dotv02) * _invDenom;

        return u >= 0 && v >= 0 && u + v < 1;
    }

}

