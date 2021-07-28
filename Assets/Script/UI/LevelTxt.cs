using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTxt : MonoBehaviour
{
    public Text levelTxt;

    private void Start()
    {
        levelTxt = GetComponent<Text>();    
        levelTxt.text = "Level " + GameDataManager.Instance.gameDataScrObj.level;
    }

}
