using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float a;
    public float speed;

    private void Update()
    {
        a -= Time.deltaTime*speed;
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, a, transform.localRotation.x);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio("coin");
            PlayerData.Instance.coinEarnThisRun++;
            Destroy(transform.parent.gameObject);
        }
    }
}
