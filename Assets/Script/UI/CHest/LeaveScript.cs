using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LeaveScript : MonoBehaviour
{
    public void LeaveBtnOnClick()
    {
        AdsManager.instance.ShowInterAd();

        GameDataManager.Instance.SetKey(-3);

        // save game phai de cuoi cung va trc chuyen scene
        GameDataManager.Instance.SaveGameData();
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Debug.Log("NewS");
    }
}
