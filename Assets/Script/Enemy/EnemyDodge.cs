using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyDodge : MonoBehaviour
{
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private EnemyMovement enemyMovement;
    private Rigidbody rb;
    [Range(0, 100)]
    public float dodgePercent;
    public LayerMask layer;
    private int marker;

    public bool oneTime { get; set; }
    private void Start()
    {
        oneTime = true;
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        enemyManager = GetComponent<EnemyManager>();
        enemyMovement = GetComponent<EnemyMovement>();

    }

    private void Update()
    {
        if (MyScene.Instance.gameIsStart)
        {
            EnemyDodgeControl2();
        }
    }

    public void EnemyDodgeControl2()
    {
        if ((int)(Time.frameCount % 30) == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemyMovement.child.transform.position
                , enemyMovement.child.transform.forward
                , out hit, MyScene.Instance.rangeOfActive, layer)
                && oneTime)
            {
                if (enemyMovement.intelligent <= 5)
                {
                    int a = Random.Range(0, 100);
                    if (a < dodgePercent)
                    {
                        rb.velocity = Vector3.zero;
                        DOTween.Kill(transform);

                        StartCoroutine(enemyManager.EnemySkin1ToSkin2());
                        enemyManager.StartParticleSystem();
                    }
                }
                else if (enemyMovement.intelligent > 5)
                {
                    rb.velocity = Vector3.zero;
                    DOTween.Kill(transform);
                    StartCoroutine(enemyManager.EnemySkin1ToSkin2());
                    enemyManager.StartParticleSystem();
                }
                oneTime = false;
                marker = Time.frameCount;

            }
            else if (!Physics.Raycast(enemyMovement.child.transform.position
                , enemyMovement.child.transform.forward, out hit, MyScene.Instance.rangeOfActive, layer)
                && !oneTime && Time.frameCount - marker >= 60)
            {
                if (enemyMovement.intelligent <= 5)
                {
                    int a = Random.Range(1, 3);
                    StartCoroutine(Delay(a));
                }

                else if (enemyMovement.intelligent > 5)
                {
                    StartCoroutine(Delay(0.5f));
                }
                oneTime = true;
                marker = 0;
            }
        }
    }

    IEnumerator Delay(float a)
    {
        yield return new WaitForSeconds(a);
        StartCoroutine(enemyManager.EnemySkin2ToSkin1());
    }
}
