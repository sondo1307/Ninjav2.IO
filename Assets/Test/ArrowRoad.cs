using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRoad : MonoBehaviour
{
    [SerializeField] private float speed;
    private Material material;
    private float offsetX;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = 0;
        this.material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offsetX += Time.deltaTime * speed;
        this.material.SetTextureOffset("_MainTex",new Vector3(offsetX, 0));
    }
}
