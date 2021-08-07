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
    public PlayerLoadSkin playerLoadSkin;

    private void Awake()
    {
        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
    }

    private void Start()
    {
        nextLevelTMP = GetComponentInChildren<Text>();
    }

    private void Update()
    {
    }

    public void NextLevelBtnClick()
    {
        DOTween.KillAll();

        GameDataManager.Instance.SetSkin1Material(playerLoadSkin.skin1.sharedMaterial);
        GameDataManager.Instance.SetSkin1Mesh(playerLoadSkin.skin1.sharedMesh);
        GameDataManager.Instance.SetSkin2Mesh(playerLoadSkin.skin2_1.sharedMesh);
        GameDataManager.Instance.SetSkin2Material(playerLoadSkin.skin2_2.sharedMaterial);

        //if (GameDataManager.Instance.gameDataScrObj.keys == 3)
        //{
        //    GameDataManager.Instance.ChestThreeKeyOpen();
        //}
        //else if (GameDataManager.Instance.gameDataScrObj.keys < 3)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}

        // save game phai de cuoi cung
        GameDataManager.Instance.SaveGameData();


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
