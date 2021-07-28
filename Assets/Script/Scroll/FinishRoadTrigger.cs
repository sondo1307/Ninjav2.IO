using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRoadTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayAudio("coin");
    }
}
