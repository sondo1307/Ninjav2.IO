using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    public GameObject currentFatherObj { get; set; }
    public GameObject content;
    void Awake()
    {
        transform.position = content.transform.GetChild(0).transform.position;
        transform.SetParent(content.transform.GetChild(0).transform);
        currentFatherObj = content.transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    public void SetPosition(Vector3 pos, Transform pa)
    {
        transform.position = pos;
        transform.SetParent(pa.transform, true);
    }
}
