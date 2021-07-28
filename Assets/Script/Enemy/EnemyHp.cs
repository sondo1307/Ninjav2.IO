using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int count;
    public int Hp;

    private void Start()
    {
        count = Hp;
    }
    private void Update()
    {
        if (count == 0)
        {
            gameObject.transform.GetChild(0).gameObject.layer = 0;
        }
        if (Hp == 0)
        {
            if (transform.CompareTag("Teacher"))
            {
                GetComponentInParent<TeacherAI>().KillTeacher();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
