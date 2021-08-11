using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Rigidbody rb;
    private bool check = true;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.CompareTag("Player") && check))
        {
            rb.AddForce((transform.position - collision.transform.position) * 10, ForceMode.Impulse);

            Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), collision.transform.GetComponentInChildren<CapsuleCollider>());
            transform.GetComponent<BoxCollider>().isTrigger = true;
            StartCoroutine(collision.transform.GetComponent<PlayerMovement>().DelaySlowSpeed());
            check = false;
        }

        else if (collision.transform.CompareTag("Enemy") && check)
        {
            rb.AddForce((transform.position - collision.transform.position) * 10, ForceMode.Impulse);
            Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), collision.transform.GetComponentInChildren<CapsuleCollider>());
            StartCoroutine(collision.transform.GetComponent<EnemyMovement>().DelaySlowSpeed());
            transform.GetComponent<BoxCollider>().isTrigger = true;
            check = false;
        }
    }

}
