using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSkinBtnClick : MonoBehaviour
{
    public int number;

    private GameObject outline;
    public Mesh mesh;
    public Material mat;
    public CapsuleSkinUI capsuleSkinUI;
    [SerializeField]private bool isBought;
    public PlayerLoadSkin playerLoadSkin;
    private ContentManager contentManager;
    private void Awake()
    {
        outline = transform.parent.GetChild(2).gameObject;
        contentManager = transform.parent.parent.GetComponent<ContentManager>();

        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
    }

    private void Start()
    {
        for (int i = 0; i < GameDataManager.Instance.gameDataScrObj.numberOfSkin2Unlocked.Count; i++)
        {
            if (GameDataManager.Instance.gameDataScrObj.numberOfSkin2Unlocked[i] == number)
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
        SetOutline(true);
        contentManager.SetOutlineChildOff(number);

        capsuleSkinUI.SetCapsuleSkin(mesh, mat);
        AudioManager.Instance.PlayAudio("tab");

        if (isBought)
        {
            playerLoadSkin.SetRealCapsuleSkin(mesh, mat);

            GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh = mesh;
            GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat = mat;
            GameDataManager.Instance.gameDataScrObj.outlineSkin2Cur = number;
        }
    }

    public void SetOutline(bool a)
    {
        outline.SetActive(a);
    }

    public void SetSkinIsBought()
    {
        AudioManager.Instance.PlayAudio("tab");

        isBought = true;

        SetOutline(true);
        contentManager.SetOutlineChildOff(number);

        capsuleSkinUI.SetCapsuleSkin(mesh, mat);
        transform.parent.GetChild(1).gameObject.SetActive(false);
        playerLoadSkin.SetRealCapsuleSkin(mesh, mat);

        GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh = mesh;
        GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat = mat;
        GameDataManager.Instance.gameDataScrObj.outlineSkin2Cur = number;
        GameDataManager.Instance.gameDataScrObj.numberOfSkin2Unlocked.Add(number);
    }
}
