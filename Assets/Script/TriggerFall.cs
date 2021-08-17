using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetTrigger("fall_idle");
            AudioManager.Instance.StopAudio("footstep");
            other.GetComponentInParent<PlayerMovement>().checkJump = true;
            other.GetComponentInParent<PlayerInput>().enabled = false;
            //Physics.IgnoreCollision(GetComponent<BoxCollider>(), other);
            //other.GetComponentInParent<PlayerMovement>().superJump = true;
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponent<Animator>().SetTrigger("fall_idle");
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), other);
            other.GetComponentInParent<EnemyMovement>().checkJump = true;
        }
    }
}
