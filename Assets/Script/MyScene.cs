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
    //public List<GameObject> listOfTeacher = new List<GameObject>();
    public int placeCount = 0;

    private PlayerInput playerInput;
    private List<EnemyManager> enemysManager { get; set; }
    public List<GameObject> listOfPlayer = new List<GameObject>();
    public bool oneTime { get; set; }

    [Header("Particle")]
    public GameObject confettiPrefab;
    public GameObject hitEffect;
    public GameObject smokeEffect;
    public GameObject smokeEffectNoSmokeUp;
    private void Awake()
    {
        //Application.targetFrameRate = 60;
        Instance = this;
        IgnoreCollision();
    }

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        //EnemyManager[] a =  FindObjectsOfType<EnemyManager>();
        enemysManager = new List<EnemyManager>(FindObjectsOfType<EnemyManager>());
    }
    private void Update()
    {
        if (oneTime)
        {
            //StartCoroutine(Delay());
            StartGame();
        }
    }

    public void StartVibrate()
    {
        if (GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            Handheld.Vibrate();
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
                enemysManager[i].animator.SetTrigger("prepare_run");
            }
        }
        playerInput.animator.SetTrigger("prepare_run");
        gameIsStart = true;
    }

    public void IgnoreCollision()
    {
        for (int i = 0; i < listOfPlayer.Count; i++)
        {
            for (int j = 0; j < listOfPlayer.Count; j++)
            {
                Physics.IgnoreCollision(listOfPlayer[i].transform.GetChild(0).GetComponent<CapsuleCollider>()
                    , listOfPlayer[j].transform.GetChild(0).GetComponent<CapsuleCollider>());
            }
        }
    }

    public void StartParticleConfetti(Vector3 position)
    {
        Instantiate(confettiPrefab, position, Quaternion.Euler(-90, 0, 0));
    }
}
