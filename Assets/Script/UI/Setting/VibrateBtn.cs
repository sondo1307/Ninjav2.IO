using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateBtn : MonoBehaviour
{
    public Sprite defaultSprite1;
    public Sprite spriteMute;

    private void Start()
    {
        if (GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            transform.GetComponent<Image>().sprite = defaultSprite1;
        }
        else
        {
            transform.GetComponent<Image>().sprite = spriteMute;
        }
    }

    public void Click()
    {
        MyScene.Instance.StartVibrate();

        if (GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            transform.GetComponent<Image>().sprite = spriteMute;
        }
        else if (!GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            transform.GetComponent<Image>().sprite = defaultSprite1;
        }
        GameDataManager.Instance.SetVibrate();

    }
}
