using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishRoadCollider : MonoBehaviour
{
    public TMP_Text txt;
    public float multiplePoint;

    private void Awake()
    {
        txt = GetComponentInChildren<TMP_Text>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Dummy"))
        {
            PlayerData.Instance.multipleCoin = (int)multiplePoint;
        }
    }
}
