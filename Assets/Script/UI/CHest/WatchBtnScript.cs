using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WatchBtnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(6.5f, 9.5f, 1), 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    public void OnClick()
    {

    }
}
