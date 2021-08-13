using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorBase : MonoBehaviour
{
    [SerializeField] private Color mainColor;

    private static int MainColorID = Shader.PropertyToID("_Color");
    private Renderer renderComp;
    private void Awake()
    {
        renderComp = GetComponent<Renderer>();

        // Long's origin code;
        //renderComp.sharedMaterial.SetColor(MainColorID, mainColor);
        //mine
        renderComp.sharedMaterial.SetColor("_Color", mainColor);

    }
}
