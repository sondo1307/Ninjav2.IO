using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerSetDestroy : MonoBehaviour
{
    public GameObject objectToDestroy;
    //public TriggerEnterSetActive triggerEnterSetActive;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {

            if (objectToDestroy.transform.CompareTag("Teacher"))
            {
                objectToDestroy.GetComponent<TeacherAI>().KillTeacher();
            }
            else
            {
                DOTween.Kill(gameObject.transform);
                Destroy(gameObject);
            }
        }
    }
}
