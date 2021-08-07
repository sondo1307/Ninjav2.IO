using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGroupManager : MonoBehaviour
{
    public GameObject tab1;
    public GameObject tab2;
    public GameObject skin1;
    public GameObject skin2;

    void Start()
    {
        tab1.SetActive(true);
        skin1.SetActive(true);
        tab2.SetActive(false);
        skin2.SetActive(false);
    }

    public void OnShopEnter()
    {
        tab1.SetActive(true);
        skin1.SetActive(true);
        tab2.SetActive(false);
        skin2.SetActive(false);
    }
}
