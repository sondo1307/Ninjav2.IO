using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spring : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            AudioManager.Instance.StopAudio("footstep");
            other.GetComponentInParent<PlayerMovement>().Jump();
            //other.GetComponentInParent<PlayerInput>().enabled = false;
            //other.GetComponentInParent<PlayerManager>().rb.constraints = other.GetComponentInParent<PlayerManager>().constraintAllRotation;

        }
        else if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyMovement>().Jump();
            //other.GetComponentInParent<PlayerManager>().rb.constraints = other.GetComponentInParent<PlayerManager>().constraintAllRotation;

        }
    }
}
