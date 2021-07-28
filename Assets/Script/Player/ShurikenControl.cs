using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenControl : MonoBehaviour
{
    public int shuriken;
    public int totalShuriken;
    public GameObject shurikenPrefab;
    public GameObject throwPoint;


    public float throwShurikenRange;
    public float delayThrow;
    public LayerMask layer;
    private void Update()
    {
        if (MyScene.Instance.gameIsStart)
        {
            ThrowShuriken();
        }
    }

    public void ThrowShuriken()
    {
        RaycastHit hit;
        if (GetComponent<PlayerManager>().isSkin1 && !GetComponent<PlayerManager>().playerIsDead)
        {
            if (Physics.Raycast(throwPoint.transform.position, transform.forward, out hit, throwShurikenRange, layer) 
                && (int)(Time.frameCount % delayThrow) == 0
                && shuriken>0)
            {
                if (hit.transform.GetComponentInParent<EnemyHp>())
                {
                    Instantiate(shurikenPrefab, throwPoint.transform.position, Quaternion.identity);
                    hit.transform.GetComponentInParent<EnemyHp>().count--;
                    shuriken--;

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(throwPoint.transform.position, transform.forward * throwShurikenRange);
    }


}
