using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterSlower : MonoBehaviour
{
    public float force;
    public enum Mode
    {
        Faster,
        Slower,
    };

    public Mode mode;

    private void OnTriggerStay(Collider other)
    {
        if (mode == Mode.Faster)
        {
            if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
            {
                Rigidbody rb = other.transform.GetComponentInParent<Rigidbody>();
                rb.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
            }
        }
        else if (mode == Mode.Slower)
        {
            if (other.transform.CompareTag("Player") || other.transform.CompareTag("Enemy"))
            {
                Rigidbody rb = other.transform.GetComponentInParent<Rigidbody>();
                rb.AddForce(-Vector3.forward * force, ForceMode.VelocityChange);
            }
        }
    }
}
