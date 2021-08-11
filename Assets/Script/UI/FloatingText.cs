using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TMP_Text t;
    Sequence s;
    private void Start()
    {
        s = DOTween.Sequence();
        s.Append(transform.DOScale(transform.localScale, 0.5f)).Append(transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear).OnComplete(Destroyed));
    }
    public void Destroyed()
    {
        s.Kill();
        Destroy(gameObject);
    }
}
