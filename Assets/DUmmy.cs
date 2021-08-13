using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DUmmy : MonoBehaviour
{
    public GameObject finishRoad;
    private Animator animator;

    public enum myE
    {
        Case1,
        Case2
    };
    public myE dropDown;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PushDummy(int force)
    {
        //animator.SetTrigger("Take Damage");
        Sequence s = DOTween.Sequence();
        Sequence s2 = DOTween.Sequence();
 
        if (dropDown == myE.Case1)
        {
            animator.SetTrigger("Die");
            s2.Append(transform.DORotate(new Vector3(0, 0, 0), 0.25f, RotateMode.LocalAxisAdd))
                .Append(transform.DORotate(new Vector3(360*5, 0, 0), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad));

            s.Append(transform.DOLocalMoveY(8, 2f).SetEase(Ease.Linear)).Append(transform.DOLocalMoveY(1.5f, 1.5f).SetEase(Ease.OutBounce))
    .Append(transform.DOLocalMoveY(1, 0.5f).SetEase(Ease.Linear));
            transform.DOMoveZ(finishRoad.transform.GetChild(Mathf.Clamp(force, 1, 10)).transform.position.z, 4).SetEase(Ease.OutQuad)
                .OnComplete(EndRun2);

        }
        else if (dropDown == myE.Case2)
        {
            animator.SetTrigger("Take Damage");
            transform.DORotate(new Vector3(0, 3600, 0), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad);
            transform.DOMoveZ(finishRoad.transform.GetChild(Mathf.Clamp(force, 1, 10)).transform.position.z, 4).SetEase(Ease.OutQuad)
                .OnComplete(EndRun);

        }

    }


    public void EndRun()
    {
        PlayerData.Instance.SetTotalCoinThisRun();
        animator.SetTrigger("Die");
        MyScene.Instance.StartParticleConfetti(transform.position );
        MyScene.Instance.StartParticleConfetti(transform.position );
        transform.parent.Find("WindParticle").gameObject.SetActive(false);
        StartCoroutine(Delay());
    }
        public void EndRun2()
    {
        PlayerData.Instance.SetTotalCoinThisRun();
        MyScene.Instance.StartParticleConfetti(transform.position );
        MyScene.Instance.StartParticleConfetti(transform.position );
        transform.parent.Find("WindParticle").gameObject.SetActive(false);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
        StartCoroutine(UIManager.Instance.LevelComplete());
    }
}
