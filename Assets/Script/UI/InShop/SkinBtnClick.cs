using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinBtnClick : MonoBehaviour
{
    public int number;

    public GameObject outline;
    [SerializeField] private PlayerSkinUI playerSkinUI;
    private PlayerLoadSkin playerLoadSkin;
    public Mesh mesh;
    public Material mat;
    [SerializeField] private bool isBought;
    private void Awake()
    {
        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
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
            transform.parent.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void OnItemClick()
    {
        //outline.transform.position = transform.position;
        //outline.transform.SetParent(transform, true);
        outline.GetComponent<OutlineScript>().SetPosition(transform.position, transform);
        playerSkinUI.SetSkin(mesh, mat);
        if (isBought)
        {
            playerLoadSkin.SetRealSkin(mesh, mat);
            outline.GetComponent<OutlineScript>().currentFatherObj = gameObject;
            //playerSkinUI.curMesh = mesh;
            //playerSkinUI.curMat = mat;
            GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
            GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
            GameDataManager.Instance.gameDataScrObj.outlineSkin1Position = transform.position;
            GameDataManager.Instance.gameDataScrObj.outlineSkin1FatherInContentGroup = number;
        }
    }

    public void SetSkinIsBought()
    {
        isBought = true;
        playerSkinUI.SetSkin(mesh, mat);
        outline.GetComponent<OutlineScript>().SetPosition(transform.position, transform);
        transform.parent.GetChild(1).gameObject.SetActive(false);
        playerLoadSkin.SetRealSkin(mesh, mat);
        outline.GetComponent<OutlineScript>().currentFatherObj = gameObject;
        //playerSkinUI.curMesh = mesh;
        //playerSkinUI.curMat = mat;

        GameDataManager.Instance.gameDataScrObj.skin1Mesh = mesh;
        GameDataManager.Instance.gameDataScrObj.skin1Material = mat;
        GameDataManager.Instance.gameDataScrObj.outlineSkin1Position = transform.position;
        GameDataManager.Instance.gameDataScrObj.outlineSkin1FatherInContentGroup = number;
        GameDataManager.Instance.gameDataScrObj.numberOfSkin1Unlocked.Add(number);

    }
}
