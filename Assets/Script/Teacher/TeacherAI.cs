using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeacherAI : MonoBehaviour
{
    public bool start { get; set; }

    public float timeLerp;
    private int i = 0;
    private bool allowPatrol;
    public FieldOfView fieldOfView;
    private bool oneTime = true;
    private bool oneTimeStartPatrol = true;
    private bool controlDelayToPatrol = true;

    public float idleToPatrolTime = 5f;
    private Animator animator;
    public Transform target;
    private Vector3 defaultTargetPosition;
    public Transform child;
    [Range(-2.5f, 2.5f)]
    public List<float> targetsXs = new List<float>();
    private bool fieldOfViewOneTime = true;
    private bool startFieldOfView = false;
    private bool stopToCheckPlayer;
    public LayerMask layer;
    public BoxCollider boxForEnemyIOScan;

    [Header("Signal")]
    public GameObject dangerSignal;

    private void Start()
    {
        stopToCheckPlayer = true;
        defaultTargetPosition = target.position;
        animator = GetComponentInChildren<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        allowPatrol = true;
        fieldOfView.enabled = false;
        fieldOfView.viewRadius = 0;
        fieldOfView.viewAngle = 0;
        InvokeRepeating("ScanBool", 2, 2);
    }

    private bool end;

    private void Update()
    {
        if (start)
        {
            if (oneTimeStartPatrol)
            {
                StartCoroutine(StartPatrol());
                oneTimeStartPatrol = false;
            }
            if (startFieldOfView)
            {
                fieldOfView.viewRadius = Mathf.SmoothStep(fieldOfView.viewRadius, fieldOfView.defaultViewRadius, 0.1f);
                //DOTween.To(() => fieldOfView.viewRadius, x => fieldOfView.viewRadius = x, fieldOfView.defaultViewRadius, 1.5f);
            }
            if (i < targetsXs.Count && allowPatrol)
            {
                CheckReachPatrolPoint();
            }
            else if (i == targetsXs.Count&&!end)
            {
                ReachEndOfList();
            }
            if (i < targetsXs.Count && !allowPatrol && controlDelayToPatrol)
            {
                StartCoroutine(DelayToPatrol());
            }
        }

    }

    IEnumerator StartPatrol()
    {
        dangerSignal.SetActive(true);
        boxForEnemyIOScan.enabled = true;
        Tween a = transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetEase(Ease.Linear);
        animator.SetBool("turn", true);
        yield return a.WaitForCompletion();
        startFieldOfView = true;

        fieldOfView.enabled = true;
        animator.SetTrigger("lookup");
        fieldOfView.viewAngle = fieldOfView.defaultViewAngle;
        yield return new WaitForSeconds(1);
        startFieldOfView = false;
        Patrol();
    }

    public void Patrol()
    {
        int a = Mathf.FloorToInt(Random.Range(0, 5));
        if (a == 0)
        {
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.Linear);
        }
        else if (a == 1)
        {

            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.OutSine);
        }
        else if (a == 2)
        { 
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.OutCirc);
        }
        else if (a == 3)
        {
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.InQuart);
        }
        else if (a ==4 )
        {
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.InBack);
        }
    }

    private bool scanBool;

    public void CheckReachPatrolPoint()
    {
        if (Vector3.Distance(new Vector3(target.transform.position.x, 0, 0), new Vector3(targetsXs[i], 0, 0)) <= 0.05f)
        {
            i++;
            if (i < targetsXs.Count)
            {
                DOTween.Kill(transform);
                DOTween.Kill(target.transform);
                Patrol();
            }
        }

        ScanBool();

        if (scanBool)
        {
            if (Physics.Raycast(child.position, new Vector3(target.position.x, child.position.y, target.position.z) - child.position, fieldOfView.viewRadius - 0.25f, layer) 
                && stopToCheckPlayer )
            {
                //DOTween.Pause(target.transform);
                fieldOfView.viewAngle = Mathf.SmoothStep(10, fieldOfView.viewAngle, 0.8f);
                //DOTween.To(() => fieldOfView.viewAngle, x => fieldOfView.viewAngle = x, 5, 0.5f);
                StartCoroutine(DelayFieldOfView());
            }
        }
        if (!stopToCheckPlayer)
        {
            fieldOfView.viewAngle = Mathf.SmoothStep(fieldOfView.viewAngle, fieldOfView.defaultViewAngle, 0.1f);
            //DOTween.To(() => fieldOfView.viewAngle, x => fieldOfView.viewAngle = x, fieldOfView.defaultViewAngle, 0.5f);
        }
    }

    public void ScanBool()
    {
        float a = Random.Range(0, 10);

        if (Time.frameCount% 30 == 0)
        {
            if (a <= 7)
            {
                scanBool = true;
            }
            else
            {
                scanBool = false;
            }
        }

    }

    IEnumerator DelayFieldOfView()
    {
        if (fieldOfViewOneTime)
        {
            fieldOfViewOneTime = false;
            yield return new WaitForSeconds(1);
            DOTween.Play(target.transform);
            stopToCheckPlayer = false;
            yield return new WaitForSeconds(1);
            fieldOfViewOneTime = true;
            stopToCheckPlayer = true;
        }
    }


    public void ReachEndOfList()
    {
        dangerSignal.SetActive(false);
        end = true;

        //fieldOfView.viewAngle = Mathf.Lerp(0, fieldOfView.viewAngle, 0.9f);
        //fieldOfView.viewAngle = Mathf.SmoothStep(0, fieldOfView.viewAngle, 0.1f);
        //fieldOfView.viewRadius = Mathf.SmoothStep(0, fieldOfView.viewRadius, 0.8f);
        StartCoroutine(End());
        //allowPatrol = false;
        //if (oneTime)
        //{
        //    boxForEnemyIOScan.enabled = false;

        //    DOTween.Kill(transform);
        //    DOTween.Kill(target.transform);
        //    DOTween.To(() => fieldOfView.viewRadius, (x) => fieldOfView.viewRadius = x, 0, 0.2f);
        //    //fieldOfView.viewRadius = 0;
        //    animator.SetTrigger("looktoturn");
        //    transform.DORotate(Vector3.zero, 2).SetEase(Ease.Linear);
        //    StartCoroutine(Delay(2));
        //    oneTime = false;
        //}
        //if (fieldOfView.viewAngle <= 0.0001f || fieldOfView.viewRadius <= 0.001f)
        //{
        //    i = 0;
        //    fieldOfView.enabled = false;
        //}
    }

    IEnumerator End()
    {
        Tween a = DOTween.To(() => fieldOfView.viewRadius, (x) => fieldOfView.viewRadius = x, 0, 0.2f);
        yield return a.WaitForCompletion();
        animator.SetTrigger("hi");
        yield return new WaitForSeconds(1);
        KillTeacher();
    }

    IEnumerator Delay(float a)
    {
        yield return new WaitForSeconds(a);
        target.transform.position = defaultTargetPosition;
        animator.SetBool("turn", false);
    }

    IEnumerator DelayToPatrol()
    {
        oneTime = true;
        controlDelayToPatrol = false;
        yield return new WaitForSeconds(idleToPatrolTime);
        DOTween.Kill(transform, false);
        DOTween.Kill(target.transform, false);
        StartCoroutine(StartPatrol());
        target.transform.position = defaultTargetPosition;

        allowPatrol = true;
        controlDelayToPatrol = true;
    }

    public void KillTeacher()
    {
        DOTween.Kill(transform, false);
        DOTween.Kill(target.transform, false);
        DOTween.Kill(dangerSignal.transform, false);
        Instantiate(MyScene.Instance.smokeEffect, transform.position, Quaternion.Euler(-90, 0, 0));
        //MyScene.Instance.listOfTeacher.RemoveAt(0);
        Destroy(gameObject);
    }
}
