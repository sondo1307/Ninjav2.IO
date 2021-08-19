using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public int numberOfKey;
    public GameObject groupOfKey;
    public GameObject watchBtn;
    public GameObject leaveBtn;
    private void Start()
    {
        numberOfKey = 3;
    }

    public void SubtractOneKey()
    {
        if (numberOfKey == 1)
        {
            StartCoroutine(Delay());
        }
        numberOfKey--;
        groupOfKey.transform.GetChild(numberOfKey).transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        watchBtn.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        leaveBtn.SetActive(true);
    }
}
