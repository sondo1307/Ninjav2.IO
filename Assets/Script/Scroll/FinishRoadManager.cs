using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRoadManager : MonoBehaviour
{
    void Start()
    {
        SetChildTxt();
    }

    public void SetChildTxt()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<FinishRoadCollider>().multiplePoint = i ;
            transform.GetChild(i).GetComponent<FinishRoadCollider>().txt.SetText("x" + transform.GetChild(i).GetComponent<FinishRoadCollider>().multiplePoint);
        }
    }
}
