using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Placement")]
    public static PlayerData Instance;
    public int place;
    public int coinEarnThisRun;
    public int multipleCoin { get; set; }

    private void Awake()
    {
            Instance = this;
    }
    

    public void CoinEarnThisRun(int a)
    {
        if (a==1)
        {
            coinEarnThisRun += 700;
        }
        else if (a==2)
        {
            coinEarnThisRun += 500;

        }
        else if (a==3)
        {
            coinEarnThisRun += 300;

        }
    }

    public void SetTotalCoinThisRun()
    {
        multipleCoin = Mathf.Clamp(GetComponent<ShurikenControl>().totalShuriken, 1, 10);
        coinEarnThisRun *= multipleCoin;
    }
}
