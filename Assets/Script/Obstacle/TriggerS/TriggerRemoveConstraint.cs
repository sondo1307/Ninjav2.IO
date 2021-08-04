using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRemoveConstraint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")||other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<PlayerManager>().rb.constraints = other.GetComponentInParent<PlayerManager>().constraintAllRotation;
        }
    }
}
