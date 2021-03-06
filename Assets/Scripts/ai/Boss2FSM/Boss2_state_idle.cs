using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_idle : Boss2_state
{
    public override void EnterState(Boss2_controller boss)
    {
        boss.turns = 4;
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.turns <= 0)
        {
            boss.thisSpriteRenderer.flipX = boss.player.thisSpriteRenderer.flipX;
            boss.facing = boss.thisSpriteRenderer.flipX;

            if (Mathf.Abs(boss.player.position - boss.position) >= 3)
            {
                boss.ChangeState(boss.attack3);
            }

            else if (boss.position % 2 == 0)
            {
                boss.ChangeState(boss.attack2);
            }
            else
            {
                boss.ChangeState(boss.attack1);
            }
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {
        
    }


}
