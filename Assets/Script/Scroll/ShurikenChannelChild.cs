using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenChannelChild : MonoBehaviour
{
    public float a;
    public float speed;
    public bool check { get; set; }
    public int force { get; set; }

    private void Update()
    {
        //if (check)
        //{
        //    speed -= 0.5f;
        //}
        a -= Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, a, 0);
        speed = Mathf.Clamp(speed, 100, 800);
    }

    public void PushShurikenChannel()
    {
        //GetComponent<ShurikenChannel>().enabled = false;
        GetComponentInParent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.VelocityChange);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("Dummy"))
    //    {
    //        UIManager.Instance.ThrowShurikenChannel();
    //        Instantiate(MyScene.Instance.hitEffect, transform.position + Vector3.forward, Quaternion.identity);
    //        Time.timeScale = 0.3f;
    //        CameraFollow.Instance.check = true;
    //        StartCoroutine(Delay());
    //        GetComponentInParent<Rigidbody>().useGravity = true;
    //        GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //        transform.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
    //        other.transform.Find("WindParticle").gameObject.SetActive(true);
    //        other.transform.GetComponent<DUmmy>().PushDummy(force);
    //        CameraFollow.Instance.player = other.transform.gameObject;

    //    }
    //}

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSecondsRealtime(1);
    //    Time.timeScale = 1;

    //}
}
