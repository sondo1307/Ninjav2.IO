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

    public bool vibrateOn;
    public bool musicOn;

    public List<int> numberOfSkin1Unlocked = new List<int>();
    public List<int> numberOfSkin2Unlocked = new List<int>();

    public int outlineSkin1Cur;
    public int outlineSkin2Cur;
}
