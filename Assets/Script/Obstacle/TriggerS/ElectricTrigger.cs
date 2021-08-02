using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrigger : MonoBehaviour
{
    private Electric electric;

    private void Start()
    {
        electric = GetComponentInParent<Electric>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")||other.transform.CompareTag("Enemy"))
        {
            electric.check = true;
        }
    }
}
