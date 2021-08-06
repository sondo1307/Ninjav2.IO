using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public Vector3 offset;
    public GameObject player;
    public float smoothSpeed;
    public float smoothZSpeed;
    public float smoothXSpeed;
    public bool check { get; set; }
    public bool transferToLate { get; set; }
    private void Start()
    {
        Instance = this;
        player = FindObjectOfType<PlayerInput>().gameObject;
    }

    private float velcity;
    private float velcity2;

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (player != null)
        {
            float desiredZPosition = player.transform.position.z + offset.z;
            float smoothZPosition = Mathf.SmoothDamp(transform.position.z, desiredZPosition, ref velcity, 0.1f);

            float desiredXPosition = player.transform.position.x + offset.x;
            float smoothXPosition = Mathf.SmoothDamp(transform.position.x, desiredXPosition, ref velcity2, 0.4f);
            if (!transferToLate)
            {
                //transform.position = new Vector3(Mathf.Clamp(smoothXPosition, -1.5f, 1.5f), player.transform.position.y + offset.y, player.transform.position.z + offset.z);
                transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
            }
        }
    }

    float velocity3;
    private bool oneTime;
    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (player != null)
        {
            float desiredZPosition = player.transform.position.z + offset.z;
            float smoothZPosition = Mathf.SmoothDamp(transform.position.z, desiredZPosition, ref velocity3, 0.2f);

            float desiredXPosition = player.transform.position.x + offset.x;
            float smoothXPosition = Mathf.SmoothDamp(transform.position.x, desiredXPosition, ref velcity2, 0.4f);
            if (!check && transferToLate)
            {
                //transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, smoothZPosition);
                //transform.position = new Vector3(Mathf.Clamp(smoothXPosition, -1.5f, 1.5f), player.transform.position.y + offset.y, player.transform.position.z + offset.z);
                transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
            }
            else if (check && transferToLate )
            {
                transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, smoothZPosition);
                //transform.DOMoveZ(player.transform.position.z, 5f).SetUpdate(true);
            }
        }
    }
}
