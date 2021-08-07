using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinBtnClick : MonoBehaviour
{
    public GameObject outline;
    [SerializeField] private PlayerSkinUI playerSkinUI;
    private PlayerLoadSkin playerLoadSkin;
    public Mesh mesh;
    public Material mat;
    private bool isBought;
    private void Awake()
    {
        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
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
            playerSkinUI.curMesh = mesh;
            playerSkinUI.curMat = mat;
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
        playerSkinUI.curMesh = mesh;
        playerSkinUI.curMat = mat;
    }
}
