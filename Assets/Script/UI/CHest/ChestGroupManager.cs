using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGroupManager : MonoBehaviour
{
    public GameObject chestLayoutGroup;
    public GameObject watchBtn;
    public GameObject leaveBtn;

    public void NoTurnToOpenChestLeft()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
    }
}
