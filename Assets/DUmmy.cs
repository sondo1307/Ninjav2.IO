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
                .OnComplete(EndRun);

        }
        else if (dropDown == myE.Case2)
        {
            animator.SetTrigger("Take Damage");
            transform.DORotate(new Vector3(0, 3600, 0), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad);
            transform.DOMoveZ(finishRoad.transform.GetChild(Mathf.Clamp(force, 1, 10)).transform.position.z, 4).SetEase(Ease.OutQuad)
                .OnComplete(EndRun2);

        }

    }


    public void EndRun2()
    {
        PlayerData.Instance.SetTotalCoinThisRun();
        VibrateManager.Instance.SuccesVibrate();
        animator.SetTrigger("Die");
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position - Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, 20, 0));
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position - Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, 20, 0));
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position + Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, -20, 0));
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position + Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, -20, 0));
        Instantiate(MyScene.Instance.confettiPrefab2, transform.position + Vector3.up, Quaternion.identity);
        Instantiate(MyScene.Instance.confettiPrefab2, transform.position + Vector3.up, Quaternion.identity);
        transform.parent.Find("WindParticle").gameObject.SetActive(false);
        StartCoroutine(Delay());
    }
    public void EndRun()
    {
        PlayerData.Instance.SetTotalCoinThisRun();
        VibrateManager.Instance.SuccesVibrate();
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position - Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, 20, 0));
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position - Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, 20, 0));
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position + Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, -20, 0));
        //Instantiate(MyScene.Instance.confettiPrefab, transform.position + Vector3.right * 2 - Vector3.forward * 2, Quaternion.Euler(-45, -20, 0));
        Instantiate(MyScene.Instance.confettiPrefab2, transform.position + Vector3.up, Quaternion.identity);
        Instantiate(MyScene.Instance.confettiPrefab2, transform.position + Vector3.up, Quaternion.identity);

        transform.parent.Find("WindParticle").gameObject.SetActive(false);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.5f);
        Time.timeScale = 0;
        StartCoroutine(UIManager.Instance.LevelComplete());
    }
}
