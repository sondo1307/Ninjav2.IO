using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject confettiParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            MyScene.Instance.placeCount++;

            VibrateManager.Instance.LongVibrate();
            Collider[] list = other.transform.parent.GetComponentsInChildren<Collider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
            StartParticle(transform.position - Vector3.right * 2.5f, transform.position + Vector3.right * 2.5f);
            int a =  MyScene.Instance.placeCount;
            PlayerData.Instance.place = a;
            UIManager.Instance.ReachFinishLine();
            FindObjectOfType<BonusGroupControl>().GetBonus(a);
            MyScene.Instance.bonusRun = true;
            other.GetComponentInParent<PlayerInput>().enabled = false;
            other.GetComponentInParent<PlayerDoEndRun>().FreezeY();
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            MyScene.Instance.placeCount++;

            other.GetComponentInParent<EnemyMovement>().rb.velocity = Vector3.zero;
            other.GetComponentInParent<EnemyMovement>().rb.isKinematic = true;
            //other.GetComponentInParent<EnemyMovement>().animator.SetTrigger("victory");
            other.GetComponentInParent<EnemyManager>().StartParticleSystem();
            other.GetComponentInParent<EnemyManager>().KillEnemy();
            other.GetComponentInParent<EnemyMovement>().enabled = false;
            Collider[] list = other.transform.parent.GetComponentsInChildren<Collider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
        }
    }


    public void StartParticle(Vector3 position1, Vector3 position2)
    {
         Instantiate(confettiParticle, position1, Quaternion.Euler(-45, 0, 0));
         Instantiate(confettiParticle, position2, Quaternion.Euler(-45, 0, 0));
         Instantiate(confettiParticle, transform.position, Quaternion.Euler(-45, 0, 0));
    }

}
