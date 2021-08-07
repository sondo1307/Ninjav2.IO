using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class GameDataScrObj : ScriptableObject
{
    public int keys;
    public int level;
    public int totalCoin;
    public Mesh skin1Mesh;
    public Material skin1Material;
    public Mesh skin2MeshFilterMesh;
    public Material skin2MeshRendererMat;

    public int howManySkin1Unlock;
    public int howManySkin2Unlock;


    public bool vibrateOn;
    public bool musicOn;
    //public List<MeshRenderer> listSkin2_1 = new List<MeshRenderer>();
    //public List<MeshFilter> listSkin2_2 = new List<MeshFilter>();
}
