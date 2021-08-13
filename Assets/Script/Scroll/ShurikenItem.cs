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
            VibrateManager.Instance.RigidBibrate();
            other.GetComponentInParent<ShurikenControl>().shuriken+=plus;
            DOTween.Kill(transform);
            Destroy(transform.parent.gameObject);
            if (plus==1)
            {
                other.GetComponentInParent<ShurikenControl>().PlusShurikenFloatingTxt();

            }
            else
            {
                other.GetComponentInParent<ShurikenControl>().PlusShurikenFloatingTxt5();

            }
        }

    }
}
