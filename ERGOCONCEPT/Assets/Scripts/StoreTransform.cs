using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTransform : MonoBehaviour
{
    public Vector3 initialPos;
    public Vector3 initialRot;
    void Start()
    {
        initialPos = transform.localPosition;
        initialRot = transform.localEulerAngles;
    }
}
