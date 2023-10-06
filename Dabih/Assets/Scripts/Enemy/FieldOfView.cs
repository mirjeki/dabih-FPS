using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldOfView
{
    [Range(0, 500)] public float Radius;
    [Range(0, 360)] public float Vector;

    public FieldOfView(float radius, float vector)
    {
        Radius = radius;
        Vector = vector;
    }
}
