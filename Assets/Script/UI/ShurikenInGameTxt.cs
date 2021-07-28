using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShurikenInGameTxt : MonoBehaviour
{
    public ShurikenControl shurikenControl;
    private Text txt;
    void Start()
    {
        txt = GetComponent<Text>();
        shurikenControl = FindObjectOfType<ShurikenControl>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "" + shurikenControl.shuriken;
    }
}
