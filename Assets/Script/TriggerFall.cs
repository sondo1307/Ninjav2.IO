using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
        {
            other.GetComponent<Animator>().SetTrigger("fall_idle");
            other.GetComponentInParent<PlayerMovement>().checkJump = true;
            other.GetComponentInParent<PlayerMovement>().superJump = true;
        }
    }
}
