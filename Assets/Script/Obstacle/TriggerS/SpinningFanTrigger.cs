using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningFanTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
