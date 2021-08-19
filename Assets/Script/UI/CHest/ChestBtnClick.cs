using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestBtnClick : MonoBehaviour
{
    public GameObject chest;
    private float chestOriginScale;
    public ChestManager chestManager;
    private bool oneTime;
    public int coinInChest;
    private Text coinInChestTxt;
    private void Start()
    {
        chestOriginScale = chest.GetComponent<RectTransform>().localScale.x;
        coinInChest = Random.Range(100, 500);
        coinInChest /= 10;
        Mathf.RoundToInt(coinInChest);
        coinInChest *= 10;
        coinInChestTxt = transform.parent.GetChild(1).GetChild(1).GetComponentInChildren<Text>();
        coinInChestTxt.text = "" + coinInChest;
    }
    public void OnClick()
    {
        if (!oneTime && chestManager.numberOfKey > 0)
        {
            Sequence s = DOTween.Sequence();
            s.Append(chest.GetComponent<RectTransform>().DOScaleX(0, 0.25f).SetEase(Ease.Linear).OnComplete(ABC))
                .Append(chest.GetComponent<RectTransform>().DOScaleX(chestOriginScale, 0.25f)).SetUpdate(true);
            oneTime = true;
            chestManager.SubtractOneKey();
            GameDataManager.Instance.SetCoin(coinInChest);
        }
    }

    public void ABC()
    {
        transform.parent.GetChild(0).gameObject.SetActive(false);
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }
}
