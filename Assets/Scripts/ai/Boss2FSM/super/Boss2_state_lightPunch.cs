using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_lightPunch : Boss2_state
{
    public override void EnterState(Boss2_controller boss)
    {
        boss.chain ++;
        boss.thisAnim.Play("LightPunchPre");
        boss.turns = 6;
        boss.waitTurns = 3;
        boss.attackTurns = 1;
        boss.moveTurns = 1;
        boss.pushBack = 1;
        if (boss.thisSpriteRenderer.flipX) { boss.speed = 1; } else { boss.speed = -1; }
        boss.attackRange = 2;
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.turns<=0) {
            if (boss.player.facing == boss.facing)
            {
                if(Mathf.Abs(boss.player.position - boss.position) <= 2)
                {
                    boss.ChangeState(boss.gameObject.GetComponent<Boss2_state_superPunch>());
                }
                else
                {
                    boss.ChangeState(boss.gameObject.GetComponent<Boss2_state_retreat>());
                }
                
            }
            else
            {
                if (boss.chain <2)
                {
                    boss.ChangeState(boss.gameObject.GetComponent<Boss2_state_lightPunch>());
                }
                else
                {
                    boss.ChangeState(boss.attack3);
                }
            }
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {
        boss.thisSpriteRenderer.flipX = boss.player.thisSpriteRenderer.flipX;
        boss.facing = boss.thisSpriteRenderer.flipX;
    }
}
