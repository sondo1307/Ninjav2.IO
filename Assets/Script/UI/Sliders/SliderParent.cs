using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderParent : MonoBehaviour
{
    public Transform finishLine;

    private void Awake()
    {
        finishLine = FindObjectOfType<FinishLine>().transform;
    }
}
