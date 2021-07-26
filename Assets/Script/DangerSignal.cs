using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DangerSignal : MonoBehaviour
{
    public bool moveX;
    public bool moveY;
    public bool moveZ;
    public Vector3 pointReach;
    public Vector3 defaultRotation;
    public float second;
    public float waitSecond;

    public GameObject dauChamThan;
    private void Start()
    {
        dauChamThan.SetActive(false);
    }

    public void SingalControl()
    {
        dauChamThan.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(pointReach, second, RotateMode.LocalAxisAdd).SetEase(Ease.Linear))
            .Append(transform.DOMoveX(transform.position.x, waitSecond)).Append(transform.DOLocalRotate(defaultRotation, second, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
    }

    public void DauChamThanTat()
    {
        dauChamThan.SetActive(false);
    }
}
