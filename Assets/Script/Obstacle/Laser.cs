using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public ParticleSystem particle;
    public LineRenderer lineRenderer;
    public GameObject laserSparkParticle;
    public DangerSignal dangerSignal;
    public bool check { get; set; }
    private bool allowRaycast;
    public LayerMask layer;
    public BoxCollider boxForEnemyScan;

    private void Start()
    {
    }

    private void Update()
    {
        if (check)
        {
            StartCoroutine(Delay());
        }
        RaycastHit hit;
        if (allowRaycast)
        {
            if (Physics.Raycast(transform.position, Vector3.left,out hit ,7, layer))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    hit.transform.GetComponent<PlayerManager>().ResetPositionToCheckPoint();
                }
                else if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<PlayerManager>().ResetEnemyToCheckPoint();
                }
            }
        }
    }

    IEnumerator Delay()
    {
        check = false;
        particle.gameObject.SetActive(true);
        boxForEnemyScan.enabled = true;
        dangerSignal.SingalControl();

        yield return new WaitForSecondsRealtime(1);
        var rol = particle.rotationOverLifetime;
        rol.enabled = true;
        var sol = particle.sizeOverLifetime;
        sol.enabled = true;
        yield return new WaitForSeconds(1);

        //rol.enabled = false;
        //var sol = particle.sizeOverLifetime;
        //sol.enabled = true;
        allowRaycast = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        Instantiate(laserSparkParticle, transform.position - transform.right * 5.5f, Quaternion.Euler(0,90,0));
        //Instantiate(laserSparkParticle, transform.position , Quaternion.Euler(0,-90,0));
        lineRenderer.SetPosition(1, transform.position-transform.right*5.5f);
        yield return new WaitForSeconds(2);
        dangerSignal.DauChamThanTat();
        boxForEnemyScan.enabled = false;

        allowRaycast = false;
        sol.enabled = false;
        particle.gameObject.SetActive(false);
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(3);
        check = true;
    }
}
