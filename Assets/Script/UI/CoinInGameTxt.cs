using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinInGameTxt : MonoBehaviour
{
    private Text txt;
    private void Start()
    {
        txt = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        txt.text = "" + PlayerData.Instance.coinEarnThisRun;
    }
}
