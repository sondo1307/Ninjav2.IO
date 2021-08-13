using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyScene : MonoBehaviour
{
    public static MyScene Instance;
    public float rangeOfActive;
    public bool gameIsStart;
    public bool runIsFinish { get; set; }
    public float finishZ;

    public bool bonusRun { get; set; }

    public Transform finishLine;
    public int placeCount = 0;

    private List<EnemyManager> enemysManager;
    private List<PlayerManager> listOfPlayer;
    public bool oneTime { get; set; }

    [Header("Particle")]
    public GameObject confettiPrefab;
    public GameObject hitEffect;
    public GameObject smokeEffect;
    public GameObject smokeEffectNoSmokeUp;
    public GameObject floatingText;
    public GameObject floatingText5;
    private void Awake()
    {
        Instance = this;
        finishLine = FindObjectOfType<FinishLine>().transform;
        Application.targetFrameRate = 60;
        listOfPlayer = new List<PlayerManager>(FindObjectsOfType<PlayerManager>());
        enemysManager = new List<EnemyManager>(FindObjectsOfType<EnemyManager>());
        IgnoreCollision();

    }

    private void Start()
    {
        finishZ = finishLine.position.z;
    }
    private void Update()
    {
        if (oneTime)
        {
            //StartCoroutine(Delay());
            StartGame();
        }
    }


    public void StartGame()
    {
        oneTime = false;
        UIManager.Instance.StartGame();

        if (enemysManager.Count > 0)
        {
            for (int i = 0; i < enemysManager.Count; i++)
            {
                enemysManager[i].animator.SetTrigger("run");
            }
        }
        gameIsStart = true;
    }

    private List<Collider> a = new List<Collider>();
    public void IgnoreCollision()
    {
        for (int i = 0; i < listOfPlayer.Count; i++)
        {
            Collider[] b = listOfPlayer[i].GetComponentsInChildren<Collider>();
            for (int j = 0; j < b.Length; j++)
            {
                a.Add(b[j]);
            }
        }
        for (int i = 0; i < a.Count; i++)
        {
            for (int j = 0; j < a.Count; j++)
            {
                Physics.IgnoreCollision(a[i], a[j]);
            }
        }
    }

    public void StartParticleConfetti(Vector3 position)
    {
        Instantiate(confettiPrefab, position, Quaternion.Euler(-90, 0, 0));
    }
}
