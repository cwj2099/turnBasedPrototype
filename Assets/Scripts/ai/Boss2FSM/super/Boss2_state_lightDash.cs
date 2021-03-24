using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_lightDash : Boss2_state_attack2
{
    public override void EnterState(Boss2_controller boss)
    {
        
        base.EnterState(boss);
    }
    public override void Process(Boss2_controller boss)
    {
        if (boss.waitTurns > 1)
        {
            boss.thisSpriteRenderer.flipX = (boss.position < boss.player.position);
            boss.facing = boss.thisSpriteRenderer.flipX;
            if (boss.thisSpriteRenderer.flipX) { boss.speed = 1; } else { boss.speed = -1; }
        }

        if (boss.turns <= 2)
        {
            if (boss.player.facing == boss.facing)
            {
                if (Mathf.Abs(boss.player.position - boss.position) <= 2)
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
                if (boss.chain < 2)
                {
                    boss.ChangeState(boss.attack1);
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
