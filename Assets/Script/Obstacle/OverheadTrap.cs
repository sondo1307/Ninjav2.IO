using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OverheadTrap : MonoBehaviour
{
    private float defaultY;
    public float delay;
    public float temp;
    public float time;

    private void Start()
    {
        temp = time;
        //Sequence sequence = DOTween.Sequence();
        defaultY = transform.position.y;
        //sequence.Append(transform.DOMoveY(0.5f, 0.25f).SetEase(Ease.Linear)).Append(transform.DOMoveY(defaultY, 2).SetEase(Ease.Linear))
        //    .Append(transform.DOMove(transform.position, 3)).SetLoops(-1, LoopType.Restart);

    }

    private void Update()
    {
        temp -= Time.deltaTime;
        if (temp<=0)
        {
            StartCoroutine(Delay());
            temp = time;
        }
    }

    IEnumerator Delay()
    {
        MoveDown();
        GetComponent<BoxCollider>().isTrigger = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<BoxCollider>().isTrigger = false;
        yield return new WaitForSeconds(2);
        HoldPosition();
    }

    public void MoveDown()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(0.5f, 0.25f).SetEase(Ease.Linear)).Append(transform.DOMoveY(defaultY, 2).SetEase(Ease.Linear));
    }

    public void HoldPosition()
    {
        transform.DOMove(transform.position, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponentInParent<PlayerManager>().ResetPositionToCheckPoint();
        }
        else if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponentInParent<PlayerManager>().ResetEnemyToCheckPoint();
        }
    }
}
