using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_superAi : Boss2_state_idle
{
    public override void EnterState(Boss2_controller boss)
    {
        boss.chain = 0;
        boss.turns = 2;
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.turns <= 0)
        {
            boss.thisSpriteRenderer.flipX = boss.player.thisSpriteRenderer.flipX;
            boss.facing = boss.thisSpriteRenderer.flipX;

            if (Mathf.Abs(boss.player.position - boss.position) <=2)
            {
                boss.ChangeState(boss.gameObject.GetComponent<Boss2_state_lightPunch>());
            }

            else if (boss.position % 2 == 0)
            {
                boss.ChangeState(boss.attack3);
                
            }
            else
            {
                boss.ChangeState(boss.gameObject.GetComponent<Boss2_state_retreat>());
            }
        }
    }
    public override void LeaveState(Boss2_controller boss)
    {

    }
}
