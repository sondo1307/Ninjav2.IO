using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBtnClick : MonoBehaviour
{
    public GameObject outline;
    public void OnItemClick()
    {
        outline.transform.position = transform.position;
        outline.transform.SetParent(transform, true);
    }
}
