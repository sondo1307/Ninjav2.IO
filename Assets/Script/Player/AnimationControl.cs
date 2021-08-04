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
        shurikenChannel.GetComponentInChildren<ShurikenChannelChild>().PushShurikenChannel();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        shurikenChannel.GetComponentInChildren<ShurikenChannelChild>().check = true;
    }

    public void RollFinish()
    {
        GetComponent<Animator>().SetBool("jump", false);
    }
}
