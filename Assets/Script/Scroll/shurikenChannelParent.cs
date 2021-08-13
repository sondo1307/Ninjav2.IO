using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class shurikenChannelParent : MonoBehaviour
{
    public Vector3 desiredScale;
    public Vector3 scaling;
    public ShurikenControl shurikenControl;
    public int force { get; set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredScale.x = Mathf.Clamp(desiredScale.x, 1, 7);
        desiredScale.z = Mathf.Clamp(desiredScale.z, 1, 7);
    }

    public void CaculateScaleUpShurilenChannel()
    {
        shurikenControl.totalShuriken = shurikenControl.shuriken;
        desiredScale = transform.localScale + shurikenControl.totalShuriken * scaling;
        desiredScale.x = Mathf.Clamp(desiredScale.x, 1, 7);
        desiredScale.z = Mathf.Clamp(desiredScale.z, 1, 7);
    }

    public void ScaleUpShurilenChannel()
    {
        DOTween.To(() => transform.localScale, x => transform.localScale = x, desiredScale, 3)
    .SetEase(Ease.InCubic);
        DOTween.To(() => shurikenControl.shuriken, x => shurikenControl.shuriken = x, 0, 3).SetEase(Ease.InCubic);
        DOTween.To(() => transform.GetComponentInChildren<ShurikenChannelChild>().speed, x => transform.GetComponentInChildren<ShurikenChannelChild>().speed = x
        , 700, 3).SetEase(Ease.InCubic);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Dummy"))
        {
            UIManager.Instance.ThrowShurikenChannel();
            Time.timeScale = 0.2f;
            CameraFollow.Instance.check = true;
            StartCoroutine(Delay(other));
            Instantiate(MyScene.Instance.hitEffect, transform.position + Vector3.forward * 3f, Quaternion.identity);
            VibrateManager.Instance.HeavyVibrate();
            GetComponentInParent<Rigidbody>().useGravity = true;
            GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
            other.transform.parent.Find("WindParticle").gameObject.SetActive(true);
            CameraFollow.Instance.player = other.transform.gameObject;

        }
    }

    IEnumerator Delay(Collider other)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        other.transform.GetComponent<DUmmy>().PushDummy(force/10);

        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;

    }
}
