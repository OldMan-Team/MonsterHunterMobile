using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : FSMState
{
    public WalkState(Transform[] wp)
    {
        wayPoints = wp;
        stateID = FSMStateID.Walk;

        curRotSpeed = 6.0f;
        curMoveSpeed = 2.0f;

    }
    public override void Judge(Transform player, Transform npc)
    {

    }

    public override void Action(Transform player, Transform npc)
    {

    }
}
