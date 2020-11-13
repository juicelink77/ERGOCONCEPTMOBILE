using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePhysics : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetIsKinematic()
    {
        Rigidbody[] allRigidbody;
        allRigidbody = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childRigidbody in allRigidbody)
        {
            childRigidbody.isKinematic = true;
        }
    }

    public void DisableIsKinematic()
    {
        Rigidbody[] allRigidbody;
        allRigidbody = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childRigidbody in allRigidbody)
        {
            childRigidbody.isKinematic = false;
        }
    }
}
