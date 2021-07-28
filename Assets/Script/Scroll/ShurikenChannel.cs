using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenChannel : MonoBehaviour
{
    public float a;
    public float speed;
    public bool check;

    private void Update()
    {
        if (check)
        {
            speed -= 0.5f;
        }
        a -= Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, a, 0);
        speed = Mathf.Clamp(speed, 100, 800);
    }
}
