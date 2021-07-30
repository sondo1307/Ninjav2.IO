using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject confettiParticle;
    private void OnTriggerEnter(Collider other)
    {
        MyScene.Instance.placeCount++;
        if (other.transform.tag == "Player")
        {
            MyScene.Instance.StartVibrate();
            Collider[] list = other.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
            StartParticle(transform.position - Vector3.right * 2.5f, transform.position + Vector3.right * 2.5f);
            int a =  MyScene.Instance.placeCount;
            PlayerData.Instance.place = a;
            //PlayerData.Instance.CoinEarnThisRun(a);
            UIManager.Instance.ReachFinishLine();

            StartCoroutine(other.GetComponentInParent<PlayerDoEndRun>().PlayerEndRun());
        }
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyMovement>().rb.velocity = Vector3.zero;
            other.GetComponentInParent<EnemyMovement>().rb.isKinematic = true;
            //other.GetComponentInParent<EnemyMovement>().animator.SetTrigger("victory");
            other.GetComponentInParent<EnemyManager>().StartParticleSystem();
            other.GetComponentInParent<EnemyManager>().KillEnemy();
            other.GetComponentInParent<EnemyMovement>().enabled = false;
            Collider[] list = other.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
        }
    }


    public void StartParticle(Vector3 position1, Vector3 position2)
    {
         Instantiate(confettiParticle, position1, Quaternion.Euler(-90, 0, 0));
         Instantiate(confettiParticle, position2, Quaternion.Euler(-90, 0, 0));
         Instantiate(confettiParticle, transform.position, Quaternion.Euler(-90, 0, 0));
    }

}
