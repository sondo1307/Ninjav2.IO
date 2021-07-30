using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarTxt : MonoBehaviour
{
    private Text txt;

    private void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "" + GameDataManager.Instance.gameDataScrObj.level;
    }
}
