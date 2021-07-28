using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    public float scaleTime = 0.15f;
    public float smartThreshHold;
    public float stupidThreshHold;
    public float timeBetweenInvoke;
    public Transform player;
    private PlayerManager playerManager;
    private EnemyMovement enemyMovement;
    private Vector3 skin1OriginSize;
    private Vector3 skin2OriginSize;
    public Animator animator { get; set; }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerManager = GetComponent<PlayerManager>();
        enemyMovement = GetComponent<EnemyMovement>();
        skin1OriginSize = playerManager.skin1.transform.localScale;
        skin2OriginSize = playerManager.skin2.transform.localScale;
        InvokeRepeating("SmartStupidControl", 0, timeBetweenInvoke);
    }


    public void SmartStupidControl()
    {
        // enemy dang sau hoac bang vi tri nguoi choi
        if (Mathf.Abs(MyScene.Instance.finishZ - transform.position.z) >= Mathf.Abs(MyScene.Instance.finishZ - player.position.z))
        {
            int a = Random.Range(1, 100);
            if (a >= smartThreshHold)
            {
                enemyMovement.intelligent = 7;
            }
        }
        // enemy di truoc
        else if (Mathf.Abs(MyScene.Instance.finishZ - transform.position.z) < Mathf.Abs(MyScene.Instance.finishZ - player.position.z))
        {
            int a = Random.Range(1, 100);
            if (a<smartThreshHold && a > stupidThreshHold)
            {
                enemyMovement.intelligent = 5;
            }
            else if (a <= stupidThreshHold)
            {
                enemyMovement.intelligent = 3;
            }
        }
    }

    public void StartParticleSystem()
    {
        Instantiate(playerManager.particle, transform.position, Quaternion.Euler(-90, 0, 0));
    }

    public IEnumerator EnemySkin1ToSkin2()
    {
        playerManager.canMove = false;
        playerManager.isSkin1 = false;
        GetComponentInParent<Rigidbody>().velocity = Vector3.zero;

        //playerManager.skin1.transform.DOScale(Vector3.zero, scaleTime);
        playerManager.skin2.GetComponent<CapsuleCollider>().enabled = true;
        playerManager.skin2.GetComponent<MeshRenderer>().enabled = true;
        playerManager.skin1.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.skin1.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(scaleTime);
        playerManager.isSkin2 = true;
        playerManager.skin2.transform.localScale = skin2OriginSize;

    }

    public IEnumerator EnemySkin2ToSkin1()
    {
        playerManager.canMove = true;
        playerManager.isSkin2 = false;
        //playerManager.skin2.transform.DOScale(Vector3.zero, scaleTime);
        playerManager.skin1.GetComponent<CapsuleCollider>().enabled = true;
        playerManager.skin1.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        playerManager.skin2.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.skin2.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(scaleTime);
        playerManager.isSkin1 = true;

        playerManager.skin1.transform.localScale = skin1OriginSize;

    }

    public void KillEnemy()
    {
        DOTween.Kill(transform);
        Destroy(gameObject);
    }

}
