using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<TeacherAI>().start = true;
    }
}
