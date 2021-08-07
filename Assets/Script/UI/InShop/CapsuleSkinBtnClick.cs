using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSkinBtnClick : MonoBehaviour
{
    public OutlineScript outline;
    public Mesh mesh;
    public Material mat;
    public CapsuleSkinUI capsuleSkinUI;
    private bool isBought;
    public PlayerLoadSkin playerLoadSkin;
    private void Awake()
    {
        playerLoadSkin = FindObjectOfType<PlayerLoadSkin>();
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
            capsuleSkinUI.curMeshF.mesh = mesh;
            capsuleSkinUI.curMeshR.material = mat;
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
        capsuleSkinUI.meshFilter.mesh = mesh;
        capsuleSkinUI.meshRenderer.material = mat;
    }
}
