using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public GameObject shurikenChannel;
    public ShurikenControl shurikenControl;

    public void NextLevel()
    {
        StartCoroutine(UIManager.Instance.LevelComplete());
    }

    public void PushChannelShuriken()
    {
        Time.timeScale = 1;
        shurikenChannel.GetComponent<Rigidbody>().AddForce(Vector3.forward * shurikenControl.totalShuriken * 1.5f, ForceMode.VelocityChange);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        shurikenChannel.GetComponentInChildren<ShurikenChannel>().check = true;
    }
}
