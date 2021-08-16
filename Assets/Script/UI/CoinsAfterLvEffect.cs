using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinsAfterLvEffect : MonoBehaviour
{
    public RectTransform coinDestination;
    Sequence s;

    private void Start()
    {
        int a = Random.Range(-444, 444);
        int b = Random.Range(222, 666);
        s = DOTween.Sequence();
        s.Append(transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(a, b), 0.25f))
            .Append(transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(a, b), 0.25f))
         .Append(transform.DOLocalMove(transform.parent.GetChild(0).transform.localPosition, Random.Range(0.75f, 1.5f))).OnComplete(Deactive)
         .SetUpdate(true);

        //transform.DOLocalMove(transform.parent.GetChild(0).transform.localPosition, 10f);
    }

    public void Deactive()
    {
        DOTween.Kill(transform);
        transform.gameObject.SetActive(false);
    }

    public void Debug1()
    {
        Debug.Log(transform.GetComponent<RectTransform>().anchoredPosition);
    }
}
