using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinBtnClick : MonoBehaviour
{
    public int number;

    private GameObject outline;
    [SerializeField] private PlayerSkinUI playerSkinUI;
    private PlayerLoadSkin playerLoadSkin;
    public Mesh mesh;
    public Material mat;
    [SerializeField] private bool isBought;
    private ContentManager contentManager;

    public Animator animator;
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

            //outline.GetComponent<OutlineScript>().currentFatherObj = gameObject;

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
        isBought = true;

        SetOutline(true);
        contentManager.SetOutlineChildOff(number);

        playerSkinUI.SetSkin(mesh, mat);
        transform.parent.GetChild(1).gameObject.SetActive(false);
        playerLoadSkin.SetRealSkin(mesh, mat);

        GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
        GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
        GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur = number;
        GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked.Add(number);

    }
}
