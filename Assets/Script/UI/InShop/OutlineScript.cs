using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    public GameObject currentFatherObj { get; set; }
    public GameObject content;
    void Start()
    {
        if (transform.CompareTag("OutlineSkin1"))
        {
            transform.position = content.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin1FatherInContentGroup).transform.position;
            transform.SetParent(content.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin1FatherInContentGroup).GetChild(0).transform);
            currentFatherObj = content.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin1FatherInContentGroup).GetChild(0).gameObject;
        }
        else if (transform.CompareTag("OutlineSkin2"))
        {
            transform.position = content.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin2FatherInContentGroup).transform.position;
            transform.SetParent(content.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin2FatherInContentGroup).GetChild(0).transform);
            currentFatherObj = content.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin2FatherInContentGroup).GetChild(0).gameObject;
        }
    }

    public void SetPosition(Vector3 pos, Transform pa)
    {
        transform.position = pos;
        transform.SetParent(pa.transform, true);
    }
}
