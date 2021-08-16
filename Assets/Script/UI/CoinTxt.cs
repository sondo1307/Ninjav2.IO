using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinTxt : MonoBehaviour
{
    public Text coinTxt;
    private bool oneTime;
    private void Start()
    {
        if (!oneTime)
        {
            coinTxt.text = "" + GameDataManager.Instance.gameDataScrObj.totalCoin;
            oneTime = true;
        }
    }

    public void SetText(int a)
    {
        coinTxt.text = "" + a;
    }
}
