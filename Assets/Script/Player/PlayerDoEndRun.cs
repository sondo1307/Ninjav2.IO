using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDoEndRun : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Transform cylinder;
    private ShurikenControl shurikenControl;
    public GameObject shurikenChannel;
    private CameraFollow cameraFollow;
    public int a;
    public int defaulta;
    public Vector3 scaleShurikenChannel;
    public GameObject finishRoad;
    [Header("dummy")]
    public GameObject dummy;
    public GameObject confettiParticle;
    private void Start()
    {
        oneTime = true;
        oneTime2 = true;
        allow = false;
        a = defaulta;
        shurikenControl = GetComponent<ShurikenControl>();
        cameraFollow = GetComponent<PlayerManager>().myCamera;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    int sub;
    private bool oneTime;
    private bool oneTime2;
    int temp;
    bool allow;
    private void Update()
    {
        if (MyScene.Instance.gameIsFinish == true)
        {
            
            if (shurikenControl.shuriken == 0 && oneTime)
            {
                oneTime = false;
                animator.SetTrigger("throw");
                Time.timeScale = 0.3f;
                UIManager.Instance.ThrowShurikenChannel();
                dummy.transform.Find("WindParticle").gameObject.SetActive(true);
                ShurikenChannelThrow();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                temp = Time.frameCount;
                allow = true;

            }
            if (Input.GetKey(KeyCode.Mouse0) && shurikenControl.shuriken>0 && allow)
            {
                sub = Time.frameCount - temp;

                if ((int)(sub % a) == 0)
                {
                    GetComponent<ShurikenControl>().shuriken--;
                    a-=4;
                    a = Mathf.Clamp(a, 10, defaulta);
                    shurikenChannel.GetComponentInChildren<ShurikenChannel>().speed += 70;
                    shurikenChannel.transform.localScale += scaleShurikenChannel;
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                a = defaulta;
                temp = 0;
            }
            if (shurikenChannel.GetComponent<Rigidbody>().velocity.z <= 0 && shurikenChannel.GetComponentInChildren<ShurikenChannel>().check && oneTime2)
            {
                shurikenChannel.GetComponentInChildren<ShurikenChannel>().enabled = false;
                shurikenChannel.GetComponent<Rigidbody>().useGravity = true;
                shurikenChannel.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                dummy.GetComponent<Animator>().SetTrigger("die");
                Instantiate(confettiParticle, dummy.transform.position  + Vector3.up * 10, Quaternion.Euler(90, 0, 0));
                Instantiate(confettiParticle, dummy.transform.position  + Vector3.up * 10, Quaternion.Euler(90, 0, 0));
                dummy.transform.Find("WindParticle").gameObject.SetActive(false);
                PlayerData.Instance.SetTotalCoinThisRun();
                StartCoroutine(Delay());
                oneTime2 = false;
            }
        }
    }

    public IEnumerator PlayerEndRun()
    {
        AudioManager.Instance.StopAudio("footstep");
        rb.velocity = Vector3.zero;
        finishRoad.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<PlayerManager>().DisableAllCapsuleCollider();
        cameraFollow.transform.GetComponent<Camera>().DOFieldOfView(40, 1);
        DOTween.To(() => cameraFollow.offset.z, x => cameraFollow.offset.z = x, -8.5f, 1);
        shurikenControl.totalShuriken = shurikenControl.shuriken;
        Tween a = transform.DOMove(new Vector3(cylinder.position.x, transform.position.y, cylinder.position.z - 2), 1).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        animator.SetTrigger("channel");
        yield return new WaitForSeconds(1);
        shurikenChannel.SetActive(true);
        UIManager.Instance.ShowHoldTxt();
        MyScene.Instance.gameIsFinish = true;
    }

    public void ShurikenChannelThrow()
    {
        GetComponent<PlayerManager>().myCamera.player = shurikenChannel;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(UIManager.Instance.LevelComplete());
    }
}
