using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishRoadCollider : MonoBehaviour
{
    public TMP_Text txt;
    public float multiplePoint;
    public Material color;

    private void Awake()
    {
        txt = GetComponentInChildren<TMP_Text>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Dummy"))
        {
            GetComponent<Renderer>().material = color;
        }
    }
}
