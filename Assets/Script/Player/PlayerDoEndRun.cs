using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDoEndRun : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private ShurikenControl shurikenControl;
    public GameObject groupOfFinish;
    public GameObject shurikenChannel;
    public CameraFollow cameraFollow;
    [Header("GroupOfFinishInOrder")]
    private GameObject finishRoad;
    private Transform cylinder;
    private GameObject dummy;
    private GameObject gatherPowerParticle;

    private void Awake()
    {
        groupOfFinish = FindObjectOfType<DUmmy>().transform.parent.gameObject;
    }

    private void Start()
    {
        oneTime = true;

        finishRoad = groupOfFinish.transform.GetChild(0).gameObject;
        cylinder = groupOfFinish.transform.GetChild(1);
        gatherPowerParticle = groupOfFinish.transform.GetChild(2).gameObject;

        dummy = FindObjectOfType<DUmmy>().gameObject;
        shurikenControl = GetComponent<ShurikenControl>();
        cameraFollow = GetComponent<PlayerManager>().myCamera;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    int sub;
    private bool oneTime;
    private void Update()
    {
        if (MyScene.Instance.runIsFinish == true)
        {
            if (shurikenControl.shuriken == 0 && oneTime)
            {
                UIManager.Instance.ShowHoldTxt();
                oneTime = false;

            }
            if (shurikenControl.shuriken == 0  && Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("throw");
                shurikenChannel.GetComponent<shurikenChannelParent>().force = shurikenControl.totalShuriken;
                gatherPowerParticle.SetActive(false);
                GetComponent<PlayerDoEndRun>().enabled = false;

            }
        }
    }

    public IEnumerator PlayerEndRun()
    {
        AudioManager.Instance.StopAudio("footstep");
        //Destroy(GetComponentInChildren<BoxCollider>());
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        finishRoad.SetActive(true);
        CameraFollow.Instance.player = shurikenChannel;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<PlayerManager>().DisableAllCapsuleCollider();
        cameraFollow.transform.GetComponent<Camera>().DOFieldOfView(40, 1);
        DOTween.To(() => cameraFollow.offset.z, x => cameraFollow.offset.z = x, -8.5f, 1);
        shurikenChannel.GetComponent<shurikenChannelParent>().CaculateScaleUpShurilenChannel();
        Tween a = transform.DOMove(new Vector3(cylinder.position.x, transform.position.y, cylinder.position.z - 2), 1).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        gatherPowerParticle.SetActive(true);
        animator.SetTrigger("channel");
        yield return new WaitForSeconds(1);
        shurikenChannel.SetActive(true);
        shurikenChannel.GetComponent<shurikenChannelParent>().ScaleUpShurilenChannel();
        MyScene.Instance.runIsFinish = true;
    }

}
