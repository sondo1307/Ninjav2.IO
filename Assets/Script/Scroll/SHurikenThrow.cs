using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHurikenThrow : MonoBehaviour
{
    public float force;
    public float speed;
    private Rigidbody rb;
    float a;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        a -= Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(transform.rotation.x, a, transform.rotation.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyHp>().Hp--;
        }
        if (!other.transform.CompareTag("Shuriken"))
        {
            Destroy(gameObject);
        }

    }
}
