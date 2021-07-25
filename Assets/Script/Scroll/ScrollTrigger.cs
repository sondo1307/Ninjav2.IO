using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollTrigger : MonoBehaviour
{
    public GameObject grandPa;
    private void OnTriggerEnter(Collider other)
    {
        DOTween.Kill(transform.parent);
        Destroy(grandPa.gameObject);
    }
}
