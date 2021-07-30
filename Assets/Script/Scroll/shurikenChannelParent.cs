using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class shurikenChannelParent : MonoBehaviour
{
    public Vector3 desiredScale;
    public Vector3 scaling;
    public ShurikenControl shurikenControl;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredScale.x = Mathf.Clamp(desiredScale.x, 1, 5);
        desiredScale.z = Mathf.Clamp(desiredScale.z, 1, 5);
    }

    public void CaculateScaleUpShurilenChannel()
    {
        shurikenControl.totalShuriken = shurikenControl.shuriken;
        desiredScale += transform.localScale + shurikenControl.totalShuriken * scaling;
        desiredScale.x = Mathf.Clamp(desiredScale.x, 1, 5);
        desiredScale.z = Mathf.Clamp(desiredScale.z, 1, 5);
    }

    public void ScaleUpShurilenChannel()
    {
        DOTween.To(() => transform.localScale, x => transform.localScale = x, desiredScale, 3)
    .SetEase(Ease.InCubic);
        DOTween.To(() => shurikenControl.shuriken, x => shurikenControl.shuriken = x, 0, 3).SetEase(Ease.InCubic);
        DOTween.To(() => transform.GetComponentInChildren<ShurikenChannelChild>().speed, x => transform.GetComponentInChildren<ShurikenChannelChild>().speed = x
        , 700, 3).SetEase(Ease.InCubic);
    }
}
