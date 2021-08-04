using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public Vector3 offset;
    public GameObject player { get; set; }
    public float smoothSpeed;
    public float smoothZSpeed;
    public float smoothXSpeed;
    public bool check;
    private void Start()
    {
        Instance = this;
        player = FindObjectOfType<PlayerInput>().gameObject;
    }

    private float velcity;
    private float velcity2;

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (player != null)
        {
            float desiredZPosition = player.transform.position.z + offset.z;
            //float smoothZPosition = Mathf.Lerp(transform.position.z, desiredZPosition, 1f);
            float smoothZPosition = Mathf.SmoothDamp(transform.position.z, desiredZPosition, ref velcity, 0.125f);

            float desiredXPosition = player.transform.position.x + offset.x;
            float smoothXPosition = Mathf.SmoothDamp(transform.position.x, desiredXPosition, ref velcity2, 0.4f);
            if (!check)
            {
                //transform.position = new Vector3(Mathf.Clamp(smoothXPosition,-1.5f,1.5f), player.transform.position.y + offset.y, player.transform.position.z + offset.z);
                transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
            }
            else
            {
                transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, smoothZPosition);
            }
        }
    }
}
