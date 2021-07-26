using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBtn : MonoBehaviour
{
    public Sprite defaultSprite1;
    public Sprite spriteMute;

    private void Start()
    {
        if (GameDataManager.Instance.gameDataScrObj.musicOn)
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
        if (GameDataManager.Instance.gameDataScrObj.musicOn)
        {
            transform.GetComponent<Image>().sprite = spriteMute;
        }
        else if (!GameDataManager.Instance.gameDataScrObj.musicOn)
        {
            transform.GetComponent<Image>().sprite = defaultSprite1;
        }
        GameDataManager.Instance.SetMusic();
    }
}
