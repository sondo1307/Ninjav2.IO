using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShurikenControl : MonoBehaviour
{
    public int shuriken;
    public int totalShuriken;
    public GameObject shurikenPrefab;
    public GameObject throwPoint;


    public float throwShurikenRange;
    public float delayThrow;
    public LayerMask layer;

    private void Start()
    {
    }
    private void Update()
    {
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
    public GameObject a;
    public void PlusShurikenFloatingTxt()
    {
        //-0.12 0.12 1.5 1.65 -0.2
        if (a!=null)
        {
            a.GetComponent<FloatingText>().Destroyed();
        }
        Vector3 offset = new Vector3();
        offset.x = Random.Range(-0.12f, 0.12f);
        offset.y = Random.Range(1f, 1.15f);
        offset.z = -0.2f;
        a = Instantiate(MyScene.Instance.floatingText, transform.position + offset, Quaternion.identity, transform);
    }    
    public void PlusShurikenFloatingTxt5()
    {
        //-0.12 0.12 1.5 1.65 -0.2
        if (a!=null)
        {
            a.GetComponent<FloatingText>().Destroyed();
        }
        Vector3 offset = new Vector3();
        offset.x = Random.Range(-0.12f, 0.12f);
        offset.y = Random.Range(1f, 1.15f);
        offset.z = -0.2f;
        a = Instantiate(MyScene.Instance.floatingText5, transform.position + offset, Quaternion.identity, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(throwPoint.transform.position, transform.forward * throwShurikenRange);
    }


}
