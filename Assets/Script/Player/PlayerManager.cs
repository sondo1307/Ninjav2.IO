using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public class PlayerManager : MonoBehaviour
{
    public bool canMove;
    public bool canSlide { get; set; }
    public bool jumping { get; set; }
    public GameObject skin1;
    public GameObject skin2;
    public GameObject particle;

    public CameraFollow myCamera;
    public bool isSkin1 { get; set; }
    public bool isSkin2 { get; set; }
    //private NavMeshAgent agent;
    public float timeBetweenResurrect = 2;

    public Vector3 checkPointPosition { get; set; }
    public bool playerIsDead = false;

    public bool enemyIsDead = false;
    private EnemyManager enemyManager;
    private EnemyDodge enemyDodge;

    public Rigidbody rb { get; set; }
    private PlayerInput playerInput;
    public Animator animator { get; set; }
    public RigidbodyConstraints constraintAllRotation = RigidbodyConstraints.FreezeRotation;
    public RigidbodyConstraints constraintAllRotationAndY { get; set; }

    [Header("Death")]
    public CylinderDeath deathCylinder;
    public Vector3 defaultScale;

    private void Awake()
    {
        myCamera = FindObjectOfType<CameraFollow>();

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        deathCylinder = GetComponent<CylinderDeath>();
        playerInput = GetComponent<PlayerInput>();
        enemyManager = GetComponent<EnemyManager>();
        enemyDodge = GetComponent<EnemyDodge>();
        canMove = true;
        isSkin1 = true;
        //constraintAllRotationAndY = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        defaultScale = transform.localScale;
        //rb.constraints = constraintAllRotationAndY;
        checkPointPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    public void AngryResetToCheckPoint()
    {
        StartCoroutine(DelayTeacherHit(timeBetweenResurrect, "angry"));
        //yield return new WaitForSeconds(timeBetweenResurrect - 0.5f);
        //playerInput.StartParticleSystem();
    }

    public IEnumerator DelayTeacherHit(float delay, string a)
    {
        if (!playerIsDead)
        {
            AudioManager.Instance.StopAudio("footstep");
            StartCoroutine(playerInput.Skin2ToSkin1());
            canMove = false;

            animator.SetTrigger("angry");
            playerIsDead = true;
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            transform.GetComponent<PlayerInput>().enabled = false;
            animator.SetBool("run", false);

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(deathCylinder.Instance(transform.position));
            myCamera.player = null;

            yield return new WaitForSeconds(0.5f);
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear);
            Tween tween1 = transform.DOMoveY(5, 0.5f).SetEase(Ease.Linear);
            Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
            yield return tween1.WaitForCompletion();

            StartCoroutine(deathCylinder.Instance2(checkPointPosition));

            myCamera.transform.position = checkPointPosition + myCamera.offset;

            Tween tween2 = transform.DOScale(defaultScale, 0.5f).SetEase(Ease.Linear);
            yield return tween2.WaitForCompletion();
            rb.velocity = Vector3.zero;
            transform.position = checkPointPosition + new Vector3(0, 2, 0);

            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Player");
            animator.SetTrigger("get_up");

            playerInput.checkAnimationRun = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            b[0].enabled = true;
            rb.constraints = constraintAllRotation;
            yield return new WaitForSecondsRealtime(0.7f);
            Instantiate(MyScene.Instance.smokeEffectNoSmokeUp, checkPointPosition + Vector3.up*0.25f, Quaternion.Euler(90, 0, 0));
            yield return new WaitForSeconds(2f);
            myCamera.player = transform.gameObject;
            rb.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("idle");
            transform.GetComponent<PlayerInput>().enabled = true;

            canMove = true;
            playerIsDead = false;
            GetComponent<PlayerMovement>().oneTime = true;
        }
    }
    public void ResetPositionToCheckPoint()
    {
        //StartCoroutine(playerInput.Skin2ToSkin1());
        StartCoroutine(Delay(timeBetweenResurrect, "die"));
    }

    public void PlayerFall()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = false;
        }
        StartCoroutine(Delay(timeBetweenResurrect, "fall"));

    }

    public void PlayerKick(Vector3 kickDirection)
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(kickDirection, ForceMode.VelocityChange);
        StartCoroutine(Delay(timeBetweenResurrect, "die"));
    }

    IEnumerator Delay(float delay, string a)
    {
        if (!playerIsDead)
        {
            AudioManager.Instance.StopAudio("footstep");
            StartCoroutine(playerInput.Skin2ToSkin1());
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            playerIsDead = true;
            canMove = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            transform.GetComponent<PlayerInput>().enabled = false;
            transform.GetComponent<PlayerMovement>().enabled = false;
            animator.SetBool("run", false);
            animator.SetTrigger(a);
            myCamera.player = null;
            Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
            yield return new WaitForSeconds(delay);
            playerInput.StartParticleSystem();
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Player");
            animator.SetTrigger("idle");
            rb.velocity = new Vector3(0, 0, 0);
            transform.GetComponent<PlayerInput>().enabled = true;
            transform.GetComponent<PlayerMovement>().enabled = true;
            playerInput.checkAnimationRun = true;
            myCamera.player = transform.gameObject;
            myCamera.transform.position = checkPointPosition + myCamera.offset;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = checkPointPosition;
            //for (int i = 0; i < b.Length; i++)
            //{
            //    b[i].enabled = true;
            //}
            b[0].enabled = true;

            rb.constraints = constraintAllRotation;

            yield return new WaitForSeconds(0.5f);
            canMove = true;
            playerIsDead = false;
            GetComponent<PlayerMovement>().oneTime = true;

        }

    }

    public IEnumerator EnemyDelayTeacherHit(float delay, string a)
    {
        if (!enemyIsDead)
        {
            DOTween.Kill(transform);

            StopCoroutine(GetComponent<EnemyMovement>().DelayJump());
            GetComponent<EnemyDodge>().enabled = false;
            GetComponent<EnemyMovement>().enabled = false;
            StartCoroutine(enemyManager.EnemySkin2ToSkin1());

            animator.SetTrigger("angry");
            enemyIsDead = true;
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            canMove = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("run", false);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(deathCylinder.Instance(transform.position));
            yield return new WaitForSeconds(0.5f);
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear);
            Tween tween1 = transform.DOMoveY(5, 0.5f).SetEase(Ease.Linear);
            Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
            yield return tween1.WaitForCompletion();

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(deathCylinder.Instance2(checkPointPosition));
            transform.position = checkPointPosition + new Vector3(0, 5, 0);

            Tween tween2 = transform.DOScale(defaultScale, 0.5f).SetEase(Ease.Linear);
            rb.AddForce(Vector3.down, ForceMode.Impulse);
            animator.SetTrigger("get_up");

            yield return tween2.WaitForCompletion();

            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");

            transform.rotation = Quaternion.Euler(0, 0, 0);
            //for (int i = 0; i < b.Length; i++)
            //{
            //    b[i].enabled = true;
            //}
            b[0].enabled = true;

            rb.constraints = constraintAllRotation;
            yield return new WaitForSeconds(2f);

            rb.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("idle");
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("run", true);
            GetComponent<EnemyMovement>().enabled = true;
            GetComponent<EnemyDodge>().enabled = true;

            enemyDodge.oneTime = true;
            rb.velocity = new Vector3(0, 0, GetComponent<EnemyMovement>().rbSpeed);
            canMove = true;
            enemyIsDead = false;
        }
    }


    public IEnumerator EnemyDelay(float delay, string a)
    {
        if (!enemyIsDead)
        {
            DOTween.Kill(transform);
            enemyIsDead = true;
            GetComponent<EnemyDodge>().enabled = false;
            canMove = false;
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            StartCoroutine(enemyManager.EnemySkin2ToSkin1());
            StopCoroutine(GetComponent<EnemyMovement>().DelayJump());
            rb.velocity = Vector3.zero;
            transform.GetComponent<EnemyMovement>().enabled = false;
            animator.SetBool("run", false);
            animator.SetTrigger(a);
            Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
            // sau delay giay thi sinh ra cho moi
            yield return new WaitForSeconds(delay);
            StartCoroutine(enemyManager.EnemySkin2ToSkin1());

            enemyManager.StartParticleSystem();
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");

            rb.velocity = new Vector3(0, 0, 0);

            //for (int i = 0; i < b.Length; i++)
            //{
            //    b[i].enabled = true;
            //}
            b[0].enabled = true;

            animator.SetTrigger("idle");
            rb.constraints = constraintAllRotation;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = checkPointPosition;

            // sau 0.5s idle thi sang run
            yield return new WaitForSeconds(0.5f);
            enemyDodge.oneTime = true;
            GetComponent<EnemyDodge>().enabled = true;
            transform.GetComponent<EnemyMovement>().enabled = true;
            rb.velocity = new Vector3(0, 0, GetComponent<EnemyMovement>().rbSpeed);
            animator.SetBool("run", true);
            enemyIsDead = false;
            canMove = true;
        }

    }

    public void EnemyAngryResetToCheckPoint()
    {
        StartCoroutine(EnemyDelayTeacherHit(timeBetweenResurrect, "angry"));
        //yield return new WaitForSeconds(timeBetweenResurrect - 0.5f);
        //enemyManager.StartParticleSystem();
    }

    public void EnemyKick(Vector3 kickDirection)
    {
        //rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(kickDirection, ForceMode.VelocityChange);
        StartCoroutine(EnemyDelay(timeBetweenResurrect, "die"));
    }

    public void ResetEnemyToCheckPoint()
    {
        StartCoroutine(enemyManager.EnemySkin2ToSkin1());
        StartCoroutine(EnemyDelay(timeBetweenResurrect, "die"));
    }

    public void EnemyFall()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = false;
        }
        StartCoroutine(EnemyDelay(timeBetweenResurrect, "fall"));
    }

    public void DisableAllCapsuleCollider()
    {
        transform.GetChild(1).GetComponent<CapsuleCollider>().enabled = false;
    }
}
