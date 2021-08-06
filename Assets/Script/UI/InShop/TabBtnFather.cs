using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabBtnFather : MonoBehaviour
{
    public GameObject tab1;
    public GameObject tab2;
    public GameObject playerSkin1;
    public GameObject playerSkin2;

    public void Tab1Click()
    {
        tab1.SetActive(true);
        tab2.SetActive(false);
        playerSkin1.SetActive(true);
        playerSkin2.SetActive(false);
    }

    public void Tab2Click()
    {
        tab1.SetActive(false);
        tab2.SetActive(true);
        playerSkin1.SetActive(false);
        playerSkin2.SetActive(true);
    }
}
