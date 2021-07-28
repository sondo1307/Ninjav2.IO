using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CylinderDeath : MonoBehaviour
{
    public GameObject prefab;
    public float point2;
    public GameObject vaccumPrefab;


    public IEnumerator Instance(Vector3 point)
    {
        GameObject a = Instantiate(prefab, new Vector3(point.x, 4, point.z), Quaternion.identity);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(a.transform.DOMoveY(point2, 0.5f).SetEase(Ease.Linear).OnComplete(VaccumParticleInstance(point))).Append(a.transform.DOMoveY(point2, 1f))
            .Append(a.transform.DOMoveY(10, 0.5f).SetEase(Ease.Linear));
        yield return sequence.WaitForCompletion();
        DOTween.Kill(a.transform);
        Destroy(a.gameObject);
    }

    public TweenCallback VaccumParticleInstance(Vector3 point)
    {
         Instantiate(vaccumPrefab, new Vector3(point.x, 0, point.z), Quaternion.Euler(90,0,0));
        return null;
    }

    public IEnumerator Instance2(Vector3 point)
    {
        GameObject a = Instantiate(prefab, new Vector3(point.x, 4, point.z), Quaternion.identity);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(a.transform.DOMoveY(point2, 0.5f).SetEase(Ease.Linear)).Append(a.transform.DOMoveY(point2, 1f))
            .Append(a.transform.DOMoveY(10, 0.5f).SetEase(Ease.Linear));
        yield return sequence.WaitForCompletion();
        DOTween.Kill(a.transform);
        Destroy(a.gameObject);
    }
}
