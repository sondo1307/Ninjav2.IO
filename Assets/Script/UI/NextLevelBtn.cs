using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelBtn : MonoBehaviour
{
    public PlayerLoadSkin playerLoadSkin;
    public GameObject coinsAfterLvGroups;
    public GameObject coinTxt;

    private void Awake()
    {
        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
    }


    private void Update()
    {
    }

    public void NextLevelBtnClick()
    {
        StartCoroutine(CoinSound());
        GameDataManager.Instance.SetCoin(PlayerData.Instance.coinEarnThisRun);
        coinTxt.GetComponent<CoinTxt>().SetText(GameDataManager.Instance.gameDataScrObj.totalCoin);
        coinsAfterLvGroups.SetActive(true);

        GameDataManager.Instance.SetSkin1Material(playerLoadSkin.skin1.sharedMaterial);
        GameDataManager.Instance.SetSkin1Mesh(playerLoadSkin.skin1.sharedMesh);
        GameDataManager.Instance.SetSkin2Mesh(playerLoadSkin.skin2_1.sharedMesh);
        GameDataManager.Instance.SetSkin2Material(playerLoadSkin.skin2_2.sharedMaterial);
        if (GameDataManager.Instance.gameDataScrObj.keys!=3)
        {
            AdsManager.instance.ShowInterAd();
            // save game phai de cuoi cung va trc chuyen scene
            GameDataManager.Instance.SaveGameData();
            StartCoroutine(SaveGame());
        }
        else if(GameDataManager.Instance.gameDataScrObj.keys == 3)
        {
            StartCoroutine(UIManager.Instance.LevelCompleteThreeKey());
        }
    }

    IEnumerator CoinSound()
    {
        AudioManager.Instance.PlayAudio("coin_after_1");
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.Instance.PlayAudio("coin_after_2");

    }

    IEnumerator SaveGame()
    {
        yield return new WaitForSecondsRealtime(2);
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
