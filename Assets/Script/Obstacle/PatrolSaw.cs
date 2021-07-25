using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatrolSaw : MonoBehaviour
{
    public float X1;
    public float X2;
    public float patrolTime;
    private float a;
    public float speedSaw;
    public GameObject particle;

    private void Start()
    {
        X2 = -X1;
        //transform.DOMoveX(X1, patrolTime).SetEase(Ease.Linear).OnComplete(StartSparkSawParticle);
        Sequence a = DOTween.Sequence();
        a.Append(transform.DOMoveX(X1, patrolTime).SetEase(Ease.Linear).OnComplete(StartSparkSawParticle))
            .Append(transform.DOMoveX(X2, patrolTime).SetEase(Ease.Linear).OnComplete(StartSparkSawParticle))
            .SetLoops(-1, LoopType.Restart);

    }
    public void FixedUpdate()
    {
        a -= Time.deltaTime*speedSaw;
        transform.localRotation = Quaternion.Euler(a, transform.localRotation.y, transform.localRotation.z);

    }

    public void DoX1()
    {
        transform.DOMoveX(X1, patrolTime).SetEase(Ease.Linear).OnComplete(StartSparkSawParticle);
    }

    public void DoX2()
    {
        transform.DOMoveX(X2, patrolTime).SetEase(Ease.Linear).OnComplete(StartSparkSawParticle);

    }

    public void StartSparkSawParticle()
    {
        Instantiate(particle, transform.position+new Vector3(0,0.2f,0), Quaternion.identity);
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
