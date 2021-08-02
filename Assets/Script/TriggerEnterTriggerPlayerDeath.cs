using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterTriggerPlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerManager>().ResetPositionToCheckPoint();
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<PlayerManager>().ResetEnemyToCheckPoint();
        }
    }
}
