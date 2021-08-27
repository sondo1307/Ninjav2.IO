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
            transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite1;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = spriteMute;
        }
    }

    public void Click()
    {
        //VibrateManager.Instance.SmallVibrate();

        if (GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = spriteMute;
        }
        else if (!GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite1;
        }
        GameDataManager.Instance.SetVibrate();

    }
}
