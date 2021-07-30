using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DUmmy : MonoBehaviour
{
    public GameObject finishRoad;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PushDummy(int force)
    {
        //if (force <= 9)
        //{
        //    transform.DOMoveZ(finishRoad.transform.GetChild(force + 1).transform.position.z, 4);
        //}
        //else if (force >= 10)
        //{
        //    transform.DOMoveZ(finishRoad.transform.GetChild(11).transform.position.z, 4);
        //}
        transform.DOMoveZ(finishRoad.transform.GetChild(Mathf.Clamp(force+1, 1, 10)).transform.position.z, 4).OnComplete(EndRun);

    }

    public void EndRun()
    {
        PlayerData.Instance.SetTotalCoinThisRun();
        animator.SetTrigger("die");
        MyScene.Instance.StartParticleConfetti(transform.position );
        MyScene.Instance.StartParticleConfetti(transform.position );
        transform.Find("WindParticle").gameObject.SetActive(false);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
        StartCoroutine(UIManager.Instance.LevelComplete());
    }
}
