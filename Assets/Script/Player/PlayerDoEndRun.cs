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
    private CameraFollow cameraFollow;
    public int a;
    public int defaulta;
    public Vector3 scaleShurikenChannel;
    private Vector3 desiredScaleShurikenChannel;
    [Header("GroupOfFinishInOrder")]
    private GameObject finishRoad;
    private Transform cylinder;
    private GameObject dummy;
    private GameObject gatherPowerParticle;
    private void Start()
    {
        oneTime = true;

        a = defaulta;
        finishRoad = groupOfFinish.transform.GetChild(0).gameObject;
        cylinder = groupOfFinish.transform.GetChild(1);
        dummy = groupOfFinish.transform.GetChild(2).gameObject;
        gatherPowerParticle = groupOfFinish.transform.GetChild(4).gameObject;
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
                shurikenChannel.GetComponentInChildren<ShurikenChannelChild>().force = shurikenControl.totalShuriken;
                gatherPowerParticle.SetActive(false);
                GetComponent<PlayerDoEndRun>().enabled = false;

            }
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    temp = Time.frameCount;
            //    gatherPowerParticle.SetActive(true);
            //    allow = true;

            //}
            // bo? hold
            //if (Input.GetKey(KeyCode.Mouse0) && shurikenControl.shuriken>0 && allow)
            //{
            //    sub = Time.frameCount - temp;

            //    if ((int)(sub % a) == 0)
            //    {
            //        GetComponent<ShurikenControl>().shuriken--;
            //        a-=4;
            //        a = Mathf.Clamp(a, 10, defaulta);
            //        shurikenChannel.GetComponentInChildren<ShurikenChannel>().speed += 70;
            //        shurikenChannel.transform.localScale += scaleShurikenChannel;
            //    }
            //}
            //startChannelShuriken
            //if (Input.GetKeyUp(KeyCode.Mouse0))
            //{
            //    a = defaulta;
            //    gatherPowerParticle.SetActive(false);
            //    temp = 0;
            //}
            
        }
    }

    public IEnumerator PlayerEndRun()
    {
        AudioManager.Instance.StopAudio("footstep");
        rb.velocity = Vector3.zero;
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
