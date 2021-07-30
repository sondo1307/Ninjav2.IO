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
    private MyScene dataManager;
    public GameObject teacher;
    public FieldOfView fov;
    [Range(0, 100)]
    public float dodgePercent;

    public bool oneTime { get; set; }
    private void Start()
    {
        oneTime = true;
        dataManager = FindObjectOfType<MyScene>();
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        enemyManager = GetComponent<EnemyManager>();
        teacher = dataManager.listOfTeacher[0];
        fov = teacher.GetComponent<TeacherAI>().fieldOfView;
        enemyMovement = GetComponent<EnemyMovement>();

    }

    private void Update()
    {
        if (MyScene.Instance.listOfTeacher.Count != 0)
        {
            teacher = dataManager.listOfTeacher[0];
            fov = teacher.GetComponent<TeacherAI>().fieldOfView;
        }

        EnemyDodgeControl();
    }

    public void EnemyDodgeControl()
    {
        if (MyScene.Instance.listOfTeacher.Count == 0)
        {
            return;
        }
        if (Vector3.Angle((transform.position - teacher.transform.position), fov.transform.forward) < fov.viewAngle / 2 + 5
            && Vector3.Distance(transform.position, teacher.transform.position) <= fov.viewRadius && oneTime && playerManager.isSkin1)
        {
            if (enemyMovement.intelligent <=5)
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
        }
        else if (Vector3.Angle((transform.position - teacher.transform.position), fov.transform.forward) > fov.viewAngle / 2 + 6
            /*&& Vector3.Distance(transform.position, teacher.transform.position) <= fov.viewRadius*/ && !oneTime && playerManager.isSkin2)
        {
            if (enemyMovement.intelligent <= 5)
            {
                int a = Random.Range(0, 3);
                StartCoroutine(Delay(a));
            }
            else if (enemyMovement.intelligent > 5)
            {
                StartCoroutine(enemyManager.EnemySkin2ToSkin1());
            }
            oneTime = true;
        }
    }

    IEnumerator Delay(int a)
    {
        yield return new WaitForSeconds(a);
        StartCoroutine(enemyManager.EnemySkin2ToSkin1());
    }
}
