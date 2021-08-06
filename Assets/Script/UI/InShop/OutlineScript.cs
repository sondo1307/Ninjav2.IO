using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    public Transform currentSkin;
    public GameObject content;
    void Start()
    {
        transform.position = content.transform.GetChild(0).transform.position;
        transform.SetParent(content.transform.GetChild(0).transform);
    }
}
