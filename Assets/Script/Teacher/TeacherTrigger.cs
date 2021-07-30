using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GetComponentInParent<TeacherAI>().start = true;
        }
    }
}
