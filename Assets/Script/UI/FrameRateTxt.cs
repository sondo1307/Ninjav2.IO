using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateTxt : MonoBehaviour
{
    private TMP_Text txt;
    float deltaTime;
    void Start()
    {
        txt = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        txt.SetText("" + (int)(1.0f / deltaTime));
        //Debug.Log((int)(1.0f / deltaTime));
    }
}
