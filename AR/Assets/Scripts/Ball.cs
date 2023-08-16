using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;    

    [SerializeField]    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fire(float force)
    {
        transform.parent = null;

        rb.isKinematic = false;

        rb.AddForce(force * (transform.forward + Vector3.up), ForceMode.Impulse);
    }
    public void RestBall(Transform parent)
    {        
        transform.parent = parent;
        transform.position = parent.position;
        transform.rotation = parent.rotation;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
