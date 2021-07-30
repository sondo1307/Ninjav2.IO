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
    private bool checkJump;
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
        if ( Input.GetMouseButtonDown(0))
        {
            lastCursorPosition = WorldMousePos();
        }
        else if ( Input.GetMouseButton(0))
        {
            Vector2 delta = WorldMousePos() - lastCursorPosition;

            if (MyScene.Instance.gameIsStart == true && !isPushed )
            {
                MoveHorizontal(delta.x / Screen.width * sensitive * halfRange);

                //if (delta.x > 0 && !checkRight)
                //{
                //    MoveHorizontal(delta.x / Screen.width * sensitive * halfRange);
                //}
                //else if (delta.x < 0)
                //{
                //    MoveHorizontal(delta.x / Screen.width * sensitive * halfRange);
                //}
            }
            lastCursorPosition = Input.mousePosition;
        }

        CheckGround();
    }
    private void FixedUpdate()
    {
        if (MyScene.Instance.gameIsStart == true && !isPushed)
        {
            //CheckLeft();
            //CheckRight();
            if (MoveForward())
            {
                rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(1 * moveSpeed, 0, moveSpeed));
                if (oneTime)
                {
                    AudioManager.Instance.PlayAudio("footstep");
                    oneTime = false;
                }
            }
            if (!MoveForward())
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                oneTime = true;
                AudioManager.Instance.StopAudio("footstep");
            }
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

    public Vector2 WorldMousePos() => Input.mousePosition;


    protected void MoveHorizontal(float move)
    {
        RaycastHit hitRight;
        RaycastHit hitLeft;
        //left
        if (Physics.Raycast(child.transform.position, -transform.right, out hitLeft, 2, layer))
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
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, rb.velocity.z), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(Delay());
    }

    public void SuperJump()
    {
        playerManager.jumping = true;
        rb.AddForce(new Vector3(rb.velocity.x, 2.5f * jumpForce, rb.velocity.z), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(Delay());
    }

    public void CheckGround()
    {
        if (Physics.Raycast(child.transform.position, Vector3.down, 0.05f, layer))
        {
            if (checkJump)
            {
                animator.SetBool("jump", false);
                checkJump = false;
                playerManager.jumping = false;
            }
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(child.transform.position, transform.forward*0.25f);
        Gizmos.DrawRay(child.transform.position, Vector3.left * 2f);
        Gizmos.DrawRay(child.transform.position, Vector3.right * 2f);
    }
}
