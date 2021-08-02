using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelBtn : MonoBehaviour
{
    private Text nextLevelTMP;

    private void Start()
    {
        nextLevelTMP = GetComponentInChildren<Text>();
    }

    public void NextLevelBtnClick()
    {
        DOTween.KillAll();
        GameDataManager.Instance.SaveGameData();
        //if (GameDataManager.Instance.gameDataScrObj.keys == 3)
        //{
        //    GameDataManager.Instance.ChestThreeKeyOpen();
        //}
        //else if (GameDataManager.Instance.gameDataScrObj.keys < 3)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
