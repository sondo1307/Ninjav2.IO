using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShurikenItem : MonoBehaviour
{
    public int plus = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio("shuriken");
            VibrateManager.Instance.SmallVibrate();
            other.GetComponentInParent<ShurikenControl>().shuriken+=plus;
            DOTween.Kill(transform);
            Destroy(transform.parent.gameObject);
        }

    }
}
