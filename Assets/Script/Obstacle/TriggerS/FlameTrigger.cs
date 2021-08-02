using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrigger : MonoBehaviour
{
    public Flame f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
        {
            f.check = true;
        }
    }
}
