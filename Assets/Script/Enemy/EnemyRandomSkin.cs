using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSkin : MonoBehaviour
{
    public SkinnedMeshRenderer enemySkin;
    public MeshFilter capsuleReplace1;
    public MeshRenderer capsuleReplace2;

    private Mesh[] a;
    private Material[] b;
    public int skin1Total;
    public int skin2Total;
    int random;
    int random2;
    void Awake()
    {
        random = Random.Range(1, skin1Total);
        random2 = Random.Range(1, skin2Total);
        //a = Resources.LoadAll<Mesh>("Skin"+random);
        //b = Resources.LoadAll<Material>("Skin"+ random);
        enemySkin.sharedMesh = Resources.LoadAll<Mesh>("Skin" + random)[0];
        enemySkin.sharedMaterial = Resources.LoadAll<Material>("Skin" + random)[0];
        capsuleReplace1.mesh = Resources.LoadAll<Mesh>("CapsuleSkin" + random2)[0];
        capsuleReplace2.material = Resources.LoadAll<Material>("CapsuleSkin" + random2)[0];
        GetComponent<EnemyRandomSkin>().enabled = false;
    }
}
