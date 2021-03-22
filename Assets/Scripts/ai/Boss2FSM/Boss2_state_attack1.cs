using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss2_state_attack1 : Boss2_state
{
    public override void EnterState(Boss2_controller boss)
    {
        boss.thisAnim.Play("Attack1Pre");
        boss.turns = 8;
        boss.waitTurns = 3;
        boss.attackTurns = 1;
        boss.moveTurns = 1;
        boss.pushBack = 2;
        if (boss.thisSpriteRenderer.flipX) { boss.speed = 1; } else { boss.speed = -1; }
        boss.attackRange = 2;
    }

    public override void Process(Boss2_controller boss)
    {

        if (boss.turns == 0)
        {
            boss.ChangeState(boss.idle);
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {

    }
}
