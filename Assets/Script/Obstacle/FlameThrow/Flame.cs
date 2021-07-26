using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private List<Transform> list = new List<Transform>();
    public GameObject flamePrefab;
    private List<GameObject> temp = new List<GameObject>();
    public GameObject trigger;

    public DangerSignal dangerSignal1;
    public DangerSignal dangerSignal2;

    public float timeBetween;
    [SerializeField]private float tempTime;
    private void Start()
    {
        tempTime = timeBetween;
        for (int i = 0; i < transform.childCount-1; i++)
        {
            list.Add(transform.GetChild(i));
        }
    }

    private void Update()
    {
        tempTime -= Time.deltaTime;
        if (tempTime <= 0)
        {
            StartCoroutine(InstanceFlame());
            tempTime = timeBetween;
        }
    }

    public IEnumerator InstanceFlame()
    {
        dangerSignal1.SingalControl();
        dangerSignal2.SingalControl();
        yield return new WaitForSeconds(1);
        trigger.SetActive(true);
        for (int i = 0; i < list.Count; i++)
        {
            temp.Add(Instantiate(flamePrefab, list[i].transform.position, Quaternion.Euler(-90, 0, 0), list[i].transform));
        }
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        trigger.SetActive(false);
        for (int i = 0; i < temp.Count; i++)
        {
            Destroy(temp[i].gameObject);
        }
        dangerSignal1.DauChamThanTat();
        dangerSignal2.DauChamThanTat();
    }
}
