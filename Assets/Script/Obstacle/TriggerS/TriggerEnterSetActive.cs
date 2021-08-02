using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterSetActive : MonoBehaviour
{
    public GameObject objectToActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            objectToActive.SetActive(true);
        }
    }
}
