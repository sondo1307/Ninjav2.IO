using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinBtnClick : MonoBehaviour
{
    public int number;
    public int costToBuy;
    public int theOrderInListSkin1Scrip;
    private GameObject outline;
    [SerializeField] private PlayerSkinUI playerSkinUI;
    private PlayerLoadSkin playerLoadSkin;
    public Mesh mesh;
    public Material mat;
    [SerializeField] private bool isBought;
    private ContentManager contentManager;

    public Animator animator;

    private Text costToBuyTxt;

    public enum BuyMode
    {
        Coin,
        Video,
    };

    public BuyMode buyMode;
    //public bool a;

    private void Awake()
    {
        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
        outline = transform.parent.GetChild(3).gameObject;
        contentManager = transform.parent.parent.GetComponent<ContentManager>();
    }

    private void Start()
    {
        for (int i = 0; i < GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked.Count; i++)
        {
            if (GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked[i] == number)
            {
                isBought = true;
            }
        }
        if (isBought)
        {
            transform.parent.GetChild(2).gameObject.SetActive(false);
        }
        if (buyMode == BuyMode.Coin)
        {
            costToBuyTxt = transform.parent.Find("Buy").transform.GetComponentInChildren<Text>();
            costToBuyTxt.text = "" + costToBuy;
        }
        else if (buyMode == BuyMode.Video)
        {
            costToBuyTxt = transform.parent.Find("Buy").transform.GetComponentInChildren<Text>();
            costToBuyTxt.text = "" + GameDataManager.Instance.gameDataScrObj.listOfSkin1BuyByVideo[theOrderInListSkin1Scrip] + "/" + 3;
        }

    }

    public void OnItemClick()
    {
        SetOutline(true);
        contentManager.SetOutlineChildOff(number);
        playerSkinUI.SetSkin(mesh, mat);
        AudioManager.Instance.PlayAudio("tab");

        int a = 0;
        a = Random.Range(1, 5);
        animator.SetTrigger("" + a);
        if (isBought)
        {
            playerLoadSkin.SetRealSkin(mesh, mat);

            GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
            GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
            GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur = number;
        }
    }

    public void SetOutline(bool a)
    {
        outline.SetActive(a);
    }

    public void SetSkinIsBought()
    {
        if (buyMode == BuyMode.Video)
        {
            MyScene.Instance.lastSkin1BuyByVideoClicked = number;
            //if (GameDataManager.Instance.gameDataScrObj.listOfSkin1BuyByVideo[theOrderInListSkin1Scrip] == 2)
            //{
            //    isBought = true;
            //    SetOutline(true);
            //    contentManager.SetOutlineChildOff(number);

            //    playerSkinUI.SetSkin(mesh, mat);
            //    transform.parent.GetChild(2).gameObject.SetActive(false);
            //    playerLoadSkin.SetRealSkin(mesh, mat);

            //    GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
            //    GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
            //    GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur = number;
            //    GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked.Add(number);
            //}
        }
        else if (buyMode == BuyMode.Coin)
        {
            AudioManager.Instance.PlayAudio("tab");
            isBought = true;
            GameDataManager.Instance.SetCoin(-costToBuy);
            SetOutline(true);
            contentManager.SetOutlineChildOff(number);

            playerSkinUI.SetSkin(mesh, mat);
            transform.parent.GetChild(2).gameObject.SetActive(false);
            playerLoadSkin.SetRealSkin(mesh, mat);

            GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
            GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
            GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur = number;
            GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked.Add(number);
        }
    }

    public void SetAfterSawRewardAd()
    {
        if (GameDataManager.Instance.gameDataScrObj.listOfSkin1BuyByVideo[theOrderInListSkin1Scrip] == 2)
        {
            isBought = true;
            SetOutline(true);
            contentManager.SetOutlineChildOff(number);

            playerSkinUI.SetSkin(mesh, mat);
            transform.parent.GetChild(2).gameObject.SetActive(false);
            playerLoadSkin.SetRealSkin(mesh, mat);

            GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
            GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
            GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur = number;
            GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked.Add(number);
        }
        GameDataManager.Instance.SetSkin1VideoCount(theOrderInListSkin1Scrip);
        costToBuyTxt.text = "" + GameDataManager.Instance.gameDataScrObj.listOfSkin1BuyByVideo[theOrderInListSkin1Scrip] + "/" + 3;
    }
}
