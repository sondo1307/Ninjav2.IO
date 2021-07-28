using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    public float smoothSpeed;
    public float smoothZSpeed;
    public float smoothXSpeed;
    private void Start()
    {
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (player != null)
        {
            float desiredZPosition = player.transform.position.z + offset.z;
            float smoothZPosition = Mathf.Lerp(transform.position.z, desiredZPosition, smoothZSpeed );

            float desiredXPosition = player.transform.position.x + offset.x;
            float smoothXPosition = Mathf.Lerp(transform.position.x, desiredXPosition, smoothXSpeed );
            transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        }
    }
}
