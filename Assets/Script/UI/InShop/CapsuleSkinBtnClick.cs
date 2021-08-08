using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSkinBtnClick : MonoBehaviour
{
    public int number;

    public OutlineScript outline;
    public Mesh mesh;
    public Material mat;
    public CapsuleSkinUI capsuleSkinUI;
    [SerializeField]private bool isBought;
    public PlayerLoadSkin playerLoadSkin;
    private void Awake()
    {
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
        //outline.transform.position = transform.position;
        //outline.transform.SetParent(transform, true);
        outline.GetComponent<OutlineScript>().SetPosition(transform.position, transform);
        capsuleSkinUI.SetCapsuleSkin(mesh, mat);
        if (isBought)
        {
            outline.GetComponent<OutlineScript>().currentFatherObj = gameObject;
            playerLoadSkin.SetRealCapsuleSkin(mesh, mat);
            //capsuleSkinUI.curMeshF.mesh = mesh;
            //capsuleSkinUI.curMeshR.material = mat;
            GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh = mesh;
            GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat = mat;
            GameDataManager.Instance.gameDataScrObj.outlineSkin2Position = transform.position;
            GameDataManager.Instance.gameDataScrObj.outlineSkin2FatherInContentGroup = number;
        }
    }

    public void SetSkinIsBought()
    {
        isBought = true;
        outline.GetComponent<OutlineScript>().SetPosition(transform.position, transform);
        capsuleSkinUI.SetCapsuleSkin(mesh, mat);
        transform.parent.GetChild(1).gameObject.SetActive(false);
        outline.GetComponent<OutlineScript>().currentFatherObj = gameObject;
        playerLoadSkin.SetRealCapsuleSkin(mesh, mat);
        //capsuleSkinUI.meshFilter.mesh = mesh;
        //capsuleSkinUI.meshRenderer.material = mat;
        GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh = mesh;
        GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat = mat;
        GameDataManager.Instance.gameDataScrObj.outlineSkin2Position = transform.position;
        GameDataManager.Instance.gameDataScrObj.outlineSkin2FatherInContentGroup = number;
        GameDataManager.Instance.gameDataScrObj.numberOfSkin2Unlocked.Add(number);
    }
}
