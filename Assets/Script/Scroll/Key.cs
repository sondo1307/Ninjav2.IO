using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Key : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 0.25f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(Vector3.up * 360, 2, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameDataManager.Instance.SetKey();
            AudioManager.Instance.PlayAudio("key");
            DOTween.Kill(transform);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
