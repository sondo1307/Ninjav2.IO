using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Placement")]
    public static PlayerData Instance;
    public int place;
    public int coinEarnThisRun { get; set; }
    public int multipleCoin { get; set; }

    private void Awake()
    {
            Instance = this;
    }

    public void SetTotalCoinThisRun()
    {
        multipleCoin = Mathf.Clamp(GetComponent<ShurikenControl>().totalShuriken, 1, 10);
        coinEarnThisRun *= multipleCoin;
    }
}
