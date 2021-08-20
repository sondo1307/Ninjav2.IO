using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerInput : MonoBehaviour
{
    private PlayerManager playerManager;
    private Vector3 skin1OriginSize;
    private Vector3 skin2OriginSize;
    public float scaleTime;
    public bool checkAnimationRun { get; set; }

    public Animator animator { get; set; }
    public bool oneTime { get; set; }
    private void Start()
    {
        checkAnimationRun = true;
        oneTime = true;
        animator = GetComponentInChildren<Animator>();
        playerManager = GetComponent<PlayerManager>();
        skin1OriginSize = playerManager.skin1.transform.localScale;
        skin2OriginSize = playerManager.skin2.transform.localScale;
        fireBaseTrackerPlay = false;
    }
    private bool fireBaseTrackerPlay;
    private void Update()
    {
        if (MyScene.Instance.gameIsStart == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && checkAnimationRun && playerManager.canMove && oneTime)
            {
                animator.SetTrigger("run");
                oneTime = false;
                checkAnimationRun = false;
            }
            if (Input.GetKey(KeyCode.Mouse0)&&!fireBaseTrackerPlay)
            {
                FirebaseAnalystic.Instance.PlayLevel(GameDataManager.Instance.gameDataScrObj.level);
                fireBaseTrackerPlay = true;
            }
            InputReceive();
        }
    }
    public void InputReceive()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerManager.isSkin2)
        {
            StartCoroutine(Skin2ToSkin1());
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) )
        {
            checkAnimationRun = true;
            StartCoroutine(Skin1ToSkin2());
            StartParticleSystem();
        }
    }

    public void StartParticleSystem()
    {
        Instantiate(playerManager.particle, transform.position +  Vector3.up * 0.5f, Quaternion.Euler(90,0,0));
    }

    public IEnumerator Skin1ToSkin2()
    {
        playerManager.canMove = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        playerManager.isSkin1 = false;
        //playerManager.skin1.transform.DOScale(Vector3.zero, scaleTime);
        playerManager.skin2.GetComponent<CapsuleCollider>().enabled = true;
        playerManager.skin2.GetComponent<MeshRenderer>().enabled = true;
        playerManager.skin1.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.skin1.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(scaleTime);
        playerManager.isSkin2 = true;
        playerManager.skin2.transform.localScale = skin2OriginSize;

    }

    public IEnumerator Skin2ToSkin1()
    {
        playerManager.canMove = true;
        playerManager.isSkin2 = false;
        GetComponent<Rigidbody>().constraints = playerManager.constraintAllRotation;
        //playerManager.skin2.transform.DOScale(Vector3.zero, scaleTime);
        playerManager.skin1.GetComponent<CapsuleCollider>().enabled = true;
        playerManager.skin1.GetComponentInChildren<SkinnedMeshRenderer>().enabled    = true;
        playerManager.skin2.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.skin2.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(scaleTime);

        playerManager.isSkin1 = true;

        playerManager.skin1.transform.localScale = skin1OriginSize;

    }

    public void ResetAllTrigger()
    {
        animator.ResetTrigger("jump");
        animator.ResetTrigger("idle");
        animator.ResetTrigger("die");
        animator.ResetTrigger("angry");
        animator.ResetTrigger("fall_idle");
        animator.ResetTrigger("run");
        animator.ResetTrigger("roll");

    }
}
