using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isPushed;
    [Header("MoveSpeed")]
    public float moveSpeed;
    public float slowSpeed;
    public float originMoveSpeed;

    [Header("Jump")]
    public float jumpForce;
    public GameObject child;
    public LayerMask layer;
    public LayerMask wallLayer;
    public bool checkJump { get; set; }
    [Header("Component")]
    public Rigidbody rb;
    private PlayerManager playerManager;
    private Animator animator;

    public bool oneTime { get; set; }
    private void Start()
    {
        oneTime = true;
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }
    //public float defaultSpeedForward = 5;
    //public float speedForward = 5;
    public float halfRange;

    public float sensitive = 1;

    protected Vector2 lastCursorPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastCursorPosition = WorldMousePos();
        }
        else if (Input.GetMouseButton(0) && playerManager.canMove)
        {
            Vector2 delta = WorldMousePos() - lastCursorPosition;

            if (MyScene.Instance.gameIsStart == true && !isPushed && playerManager.canMove)
            {
                MoveHorizontal(delta.x / Screen.width * sensitive * halfRange);
            }
            lastCursorPosition = Input.mousePosition;
        }
        CheckGround();
    }
    private void FixedUpdate()
    {
        if (MyScene.Instance.gameIsStart == true && !isPushed && !MyScene.Instance.bonusRun)
        {
            if (MoveForward())
            {
                rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(1 * moveSpeed, 3, moveSpeed));
                //rb.position += Vector3.forward * Time.deltaTime * moveSpeed;
                if (oneTime)
                {
                    //AudioManager.Instance.PlayAudio("footstep");
                    oneTime = false;
                }
            }
            if (!MoveForward() && !playerManager.jumping)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                oneTime = true;
                AudioManager.Instance.StopAudio("footstep");
            }
        }
        else if (MyScene.Instance.gameIsStart == true && !isPushed && MyScene.Instance.bonusRun)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);
        }
    }
    public bool MoveForward()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return false;
        }
        if (playerManager.canMove && Input.GetMouseButton(0) && !Physics.Raycast(child.transform.position, transform.forward, 0.4f, wallLayer))
        {
            return true;
        }
        return false;
    }

    public bool slow { get; set; }
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
        //if (!slow)
        //{
        slow = true;
        moveSpeed = slowSpeed;
        animator.SetBool("slow_run", true);
        AudioManager.Instance.StopAudio("footstep");
        yield return new WaitForSeconds(1f);
        //AudioManager.Instance.PlayAudio("footstep");
        animator.SetBool("slow_run", false);
        moveSpeed = originMoveSpeed;
        slow = false;
        //}
    }

    public void StopDSP()
    {
        if (c!=null)
        {
            StopCoroutine(c);
            animator.SetBool("slow_run", false);
            moveSpeed = originMoveSpeed;
            slow = false;
        }

    }

    public Vector2 WorldMousePos() => Input.mousePosition;


    protected void MoveHorizontal(float move)
    {
        RaycastHit hitRight;
        RaycastHit hitLeft;
        if (Physics.Raycast(child.transform.position, -transform.right, out hitLeft, 2, layer)
            && Physics.Raycast(child.transform.position, transform.right, out hitRight, 2, layer))
        {
            rb.position = new Vector3(Mathf.Clamp(transform.position.x + move, hitLeft.point.x + 0.3f, hitRight.point.x - 0.3f), rb.position.y, rb.position.z);

        }
        //left
        else if (Physics.Raycast(child.transform.position, -transform.right, out hitLeft, 2, layer))
        {
            rb.position = new Vector3(Mathf.Clamp(transform.position.x + move, hitLeft.point.x + 0.3f, halfRange), rb.position.y, rb.position.z);
        }
        //right
        else if (Physics.Raycast(child.transform.position, transform.right, out hitRight, 2, layer))
        {
            rb.position = new Vector3(Mathf.Clamp(transform.position.x + move, -halfRange, hitRight.point.x - 0.3f), rb.position.y, rb.position.z);
        }
        else if (!Physics.Raycast(child.transform.position, -transform.right, 2f, layer)
            && !Physics.Raycast(child.transform.position, transform.right, 2, layer))
        {
            rb.position = new Vector3(Mathf.Clamp(transform.position.x + move, -halfRange, halfRange), rb.position.y, rb.position.z);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        checkJump = true;

    }

    public void Jump()
    {
        playerManager.jumping = true;
        //jump = true;
        AudioManager.Instance.StopAudio("footstep");
        GetComponent<PlayerInput>().enabled = false;
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, rb.velocity.z), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(Delay());
    }

    public void SuperJump()
    {
        playerManager.jumping = true;
        //superJump = true;
        AudioManager.Instance.StopAudio("footstep");
        GetComponent<PlayerInput>().enabled = false;
        rb.AddForce(new Vector3(rb.velocity.x, 2.5f * jumpForce, rb.velocity.z), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(Delay());
    }

    public void CheckGround()
    {
        if (Physics.Raycast(child.transform.position, Vector3.down, 0.11f, layer))
        {
            if (checkJump)
            {
                animator.SetTrigger("roll");
                //AudioManager.Instance.PlayAudio("footstep");
                GetComponent<PlayerInput>().enabled = true;
                checkJump = false;
                playerManager.jumping = false;
                //superJump = false;
                StartCoroutine(ResetRoll());
            }
        }
    }
    IEnumerator ResetRoll()
    {
        yield return new WaitForEndOfFrame();
        animator.ResetTrigger("roll");
    }
    public IEnumerator DelayFreezeY()
    {
        yield return new WaitForSeconds(0.5f);
        rb.constraints = playerManager.constraintAllRotation;
    }

    public IEnumerator PushBack(Vector3 dir)
    {
        rb.velocity = Vector3.zero;
        animator.SetBool("stun", true);
        isPushed = true;
        rb.AddForce(dir, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("stun", false);
        isPushed = false;
        rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(1 * moveSpeed, 0, moveSpeed));
    }
}
