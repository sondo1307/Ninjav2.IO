using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    private bool isPushed;
    public Rigidbody rb { get; set; }
    public float rbSpeed;
    public float rbSpeedOrigin;
    public float slowSpeed;
    public Animator animator { get; set; }
    private bool oneTime = true;

    public float jumpForce;
    public GameObject child;
    public LayerMask layer;
    public bool checkJump { get; set; }

    [Header("Slide")]
    public float slideForce;
    [Header("CheckForward")]
    public LayerMask obstacleLayer;
    public LayerMask roadLayer;
    public LayerMask wallLayer;
    public float Y1Rotation;
    private float Y2Rotation;
    private bool rayCastOn;
    public float raycastLength;
    public int numerOfRay;
    public int scanFrequence;
    [Range(1,9)]
    public int intelligent;
    private float desiredX;


    private PlayerManager playerManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
        Y2Rotation = -Y1Rotation;
        rbSpeedOrigin = rbSpeed;
        rayCastOn = true;
    }   

    private void Update()
    {
        if (MyScene.Instance.gameIsStart && !isPushed && playerManager.canMove)
        {
            RayCastCheck();
            RayCastOnOff();
            CheckGround();
        }
    }

    private void FixedUpdate()
    {
        if (MyScene.Instance.gameIsStart && !isPushed)
        {

            if (playerManager.canMove)
            {
                EnemyMovefoward();
            }
            else if (!playerManager.canMove)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y, transform.position.z);
    }


    public void EnemyMovefoward()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 1 * rbSpeed);
    }

    public void RayCastCheck()
    {
        if (rayCastOn)
        {
            DOTween.Kill(transform, false);
            Vector3[] a = new Vector3[numerOfRay];
            float tempRotation = Y1Rotation;
            float step = Mathf.Abs(Y1Rotation - Y2Rotation) / (numerOfRay - 1);
            RaycastHit forwardCheck;
            float angle = 0;
            if (Physics.Raycast(child.transform.position- new Vector3(0,0,0.1f), transform.forward, out forwardCheck, 0.1f, roadLayer))
            {
                angle = Vector3.Angle(forwardCheck.normal,transform.forward)-90;
                //Debug.Log("true");
            }
            //Physics.Raycast(child.transform.position, transform.forward, out forwardCheck, raycastLength, roadLayer);
            for (int i = 0; i < numerOfRay; i++)
            {
                Vector3 dir = Quaternion.Euler(0, tempRotation, 0) * Vector3.forward;
                Vector3 dir2 = Quaternion.Euler(-angle,0,0) * dir;
                RaycastHit hit;
                if (Physics.Raycast(child.transform.position, dir2, out hit, raycastLength, obstacleLayer))
                {
                    a[i] = hit.point;
                    //if (hit.transform.CompareTag("Obstacle"))
                    //{
                    //    a[i] = hit.point;
                    //}
                }
                else
                {
                    a[i] = dir;
                }
                tempRotation += step;
            }

            List<Vector3> listOfVector0 = new List<Vector3>();
            List<Vector3> listOfVectorNotZero = new List<Vector3>();

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].y == 0 && (a[i].x !=0||a[i].z !=0))
                {
                    listOfVector0.Add(a[i]);
                }
                else if (a[i].y != 0)
                {
                    listOfVectorNotZero.Add(a[i]);
                }
            }
            if (intelligent <= 3)
            {
                if (listOfVectorNotZero.Count !=0)
                {
                    int temp = Mathf.FloorToInt(MyRandom(listOfVectorNotZero.Count));
                    transform.DOMoveX(listOfVectorNotZero[temp].x, 1);
                }
                else
                {
                    int temp3 = Random.Range(0, 10);
                    if (temp3 <= 4 && FrameCount(30))
                    {
                        if (RayCastLeft() && !RayCastRight())
                        {
                            transform.DOMoveX(transform.position.x - 0.5f, 1).SetEase(Ease.Linear);

                        }
                        else if (RayCastRight()&& !RayCastLeft())
                        {
                            transform.DOMoveX(transform.position.x + 0.5f, 1).SetEase(Ease.Linear);
                        }
                        else if (RayCastRight() && RayCastLeft())
                        {
                            int b = 0;
                            b = Random.Range(1, 10);
                            if (b < 5)
                            {
                                transform.DOMoveX(transform.position.x + 2f, 2).SetEase(Ease.InCubic);

                            }
                            else if (b >= 5)
                            {
                                transform.DOMoveX(transform.position.x - 2f, 2).SetEase(Ease.InCubic);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            else if (intelligent >= 4 && intelligent <= 6)
            {
                int temp = Random.Range(0, 10);
                if (temp <= 4)
                {
                    if (listOfVectorNotZero.Count != 0)
                    {
                        int temp2 = Mathf.FloorToInt(MyRandom(listOfVectorNotZero.Count));
                        transform.DOMoveX(listOfVectorNotZero[temp2].x, 1).SetEase(Ease.InCubic);
                    }
                    else
                    {
                        int temp3 = Random.Range(0, 10);
                        if (temp3 <= 4 && FrameCount(30))
                        {
                            if (RayCastLeft() && !RayCastRight())
                            {
                                transform.DOMoveX(transform.position.x + 0.5f, 1).SetEase(Ease.Linear);
                            }
                            else if (RayCastRight()&& !RayCastLeft())
                            {
                                transform.DOMoveX(transform.position.x - 0.5f, 1).SetEase(Ease.Linear);
                            }
                            else if (RayCastRight() && RayCastLeft())
                            {
                                int b = 0;
                                b = Random.Range(1, 10);
                                if (b < 5)
                                {
                                    transform.DOMoveX(transform.position.x + 2f, 2).SetEase(Ease.InCubic);

                                }
                                else if (b >= 5)
                                {
                                    transform.DOMoveX(transform.position.x - 2f, 2).SetEase(Ease.InCubic);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (listOfVector0.Count != 0)
                    {
                        int temp2 = Mathf.FloorToInt(MyRandom(listOfVector0.Count));
                        CaculateX(listOfVector0[temp2]);
                    }
                    else
                    {
                        int temp3 = Random.Range(0, 10);
                        if (temp3 <= 7 && FrameCount(30))
                        {
                            if (RayCastLeft() && !RayCastRight())
                            {
                                transform.DOMoveX(transform.position.x + 1f, 1).SetEase(Ease.Linear);

                            }
                            else if (RayCastRight() && !RayCastLeft())
                            {
                                transform.DOMoveX(transform.position.x - 1f, 1).SetEase(Ease.Linear);
                            }
                            else if (RayCastRight() && RayCastLeft())
                            {
                                int b = 0;
                                b = Random.Range(1, 10);
                                if (b < 5)
                                {
                                    transform.DOMoveX(transform.position.x + 2f, 2).SetEase(Ease.InCubic);

                                }
                                else if (b >= 5)
                                {
                                    transform.DOMoveX(transform.position.x - 2f, 2).SetEase(Ease.InCubic);
                                }
                            }
                        }
                    }
                }
            }

            else if (intelligent >=7 && intelligent <=9)
            {
                if (listOfVector0.Count!=0)
                {
                    int temp2 = Mathf.FloorToInt(MyRandom(listOfVector0.Count));
                    CaculateX(listOfVector0[temp2]);
                }
                else
                {
                    if (RayCastLeft() && !RayCastRight())
                    {
                        transform.DOMoveX(transform.position.x + 2f, 2).SetEase(Ease.Linear);
                    }
                    else if (RayCastRight() && !RayCastLeft())
                    {

                        transform.DOMoveX(transform.position.x - 2f, 2).SetEase(Ease.Linear);
                    }
                    else if (RayCastRight() && RayCastLeft())
                    {
                        int b = 0;
                        b = Random.Range(1, 10);
                        if (b<5)
                        {
                            transform.DOMoveX(transform.position.x + 2f, 2).SetEase(Ease.InCubic);

                        }
                        else if (b>=5)
                        {
                            transform.DOMoveX(transform.position.x - 2f, 2).SetEase(Ease.InCubic);
                        }
                    }
                }

            }
        }
    }

    public bool FrameCount(int a)
    {
        if ((int)(Time.frameCount % a )== 0)
        {
            return true;
        }
        return false;
    }

    public bool RayCastLeft()
    {
        if (Physics.Raycast(child.transform.position, -Vector3.right, 3f, wallLayer))
        {
            return true;
        }
        return false;
    }


    public bool RayCastRight()
    {
        if (Physics.Raycast(child.transform.position, Vector3.right, 3f, wallLayer))
        {
            return true;
        }
        return false;
    }

    public int MyRandom(int a)
    {
        return Random.Range(0, a);
    }

    public void CaculateX(Vector3 dir)
    {
        float referenceX = 0;
        referenceX = Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(Vector3.right, dir)) * raycastLength;
        desiredX = transform.position.x + referenceX;
        transform.DOMoveX(desiredX, 1);

    }

    public void RayCastOnOff()
    {
        if ((int)(Time.frameCount % scanFrequence) == 0)
        {
            rayCastOn = true;
        }
        else if ((int)(Time.frameCount % scanFrequence) != 0)
        {
            rayCastOn = false;
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, 3.55f), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(DelayJump());
    }

    public void SuperJump()
    {
        rb.AddForce(new Vector3(rb.velocity.x, 2.5f * jumpForce, 3.55f), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(DelayJump());
    }

    //public bool jump { get; set; }
    //public bool superJump { get; set; }
    public void CheckGround()
    {
        if (Physics.Raycast(child.transform.position, Vector3.down, 0.11f, layer))
        {
            //if (checkJump && jump)
            //{
            //    animator.SetBool("jump", false);
            //    checkJump = false;
            //    playerManager.jumping = false;
            //    //jump = false;
            //}
            if (checkJump)
            {
                animator.SetTrigger("roll");
                checkJump = false;
                //superJump = false;
            }
        }
    }
    public bool slow { get; set; }
    public IEnumerator PushBack(Vector3 dir)
    {
        rb.velocity = Vector3.zero;
        animator.SetBool("stun", true);
        isPushed = true;
        rb.AddForce(dir, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("stun", false);
        isPushed = false;
    }

    public IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(1);
        checkJump = true;

    }

    public Coroutine c { get; set; }

    public void DSP()
    {
        if (!slow)
        {
            c = StartCoroutine(DelaySlowSpeed());
        }
    }

    public IEnumerator DelaySlowSpeed()
    {
        if (!slow)
        {
            slow = true;
            rbSpeed = slowSpeed;
            animator.SetBool("slow_run", true);

            yield return new WaitForSeconds(1f);
            animator.SetBool("slow_run", false);

            rbSpeed = rbSpeedOrigin;
            slow = false;
        }
    }

    public void StopDSP()
    {
        if (c != null)
        {
            StopCoroutine(c);
            animator.SetBool("slow_run", false);
            rbSpeed = rbSpeedOrigin;
            slow = false;
        }

    }

}
