using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoPatrol : MonoBehaviour
{
    public bool moveX;
    public bool moveY;
    public bool moveZ;
    public float pointReach;
    public float second;
    private void Start()
    {
        if (moveX)
        {
            transform.DOLocalMoveX(pointReach, second).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        }
        else if (moveY)
        {
            transform.DOLocalMoveY(pointReach, second).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);

        }
        else if (moveZ)
        {
            transform.DOLocalMoveZ(pointReach, second).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        }
    }
}
