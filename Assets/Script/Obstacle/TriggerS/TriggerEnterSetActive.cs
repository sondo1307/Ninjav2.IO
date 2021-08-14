using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterSetActive : MonoBehaviour
{
    private bool oneTime;

    public enum Trap
    {
        Fist,
        MovingGrinder,
        MovingPlatform,
        PatrolSaw,
        SpinningSaw,
        SwingingMace,
        OverheadTrap,
    };

    public Trap trap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && !oneTime)
        {
            oneTime = true;
            if (trap == Trap.Fist)
            {
                //Swing;
                transform.parent.GetComponentInChildren<Swing>().enabled = true;
            }
            else if (trap == Trap.MovingGrinder)
            {
                //All Patrol
                transform.parent.GetComponentInChildren<AllPatrol>().enabled = true;

                //Moving Grinder
                transform.parent.GetComponentInChildren<MovingGrinder>().enabled = true;

            }
            else if (trap == Trap.MovingPlatform)
            {
                //Moving Platform
                transform.parent.GetComponentInChildren<MovingPlatform>().enabled = true;

            }
            else if (trap == Trap.PatrolSaw)
            {
                //Patrol Saw
                transform.parent.GetComponentInChildren<PatrolSaw>().enabled = true;

            }
            else if (trap == Trap.SpinningSaw)
            {
                // Moving Grinder
                transform.parent.GetComponentInChildren<MovingGrinder>().enabled = true;

            }
            else if (trap == Trap.SwingingMace)
            {
                //Swing2
                transform.parent.GetComponentInChildren<Swing2>().enabled = true;

            }
            else if (trap == Trap.OverheadTrap)
            {
                transform.parent.GetComponentInChildren<OverheadTrap>().enabled = true;

            }
        }
    }
}
