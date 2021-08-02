using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour
{
    public GameObject electricParticle;
    public GameObject roundParticle;
    public CapsuleCollider deadZone;
    public bool check;
    public DangerSignal signal1;
    public DangerSignal signal2;
    private bool allow;
    public float electroSecond;
    public float delayOn;

    private void Start()
    {
        check = false;
        allow = true;
        deadZone.enabled = false;
    }

    private void Update()
    {
        if (check)
        {
            StartCoroutine(StartElectric());
        }
    }

    public IEnumerator StartElectric()
    {
        if (allow)
        {
            allow = false;
            deadZone.enabled = true;
            signal1.SingalControl();
            signal2.SingalControl();
            yield return new WaitForSecondsRealtime(0.5f);
            GameObject a = Instantiate(electricParticle, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            GameObject b = Instantiate(roundParticle, transform.position, Quaternion.Euler(-90, 0, 0));
            yield return new WaitForSeconds(electroSecond);
            Destroy(a);
            Destroy(b);
            deadZone.enabled = false;

            yield return new WaitForSeconds(0.5f);
            signal1.DauChamThanTat();
            signal2.DauChamThanTat();
            yield return new WaitForSecondsRealtime(delayOn);
            allow = true;
        }

    }
}
