using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.EndBonusRoad();
            StartCoroutine(other.GetComponentInParent<PlayerDoEndRun>().PlayerEndRun());
            CameraFollow.Instance.transferToLate = true;
        }
    }
}
