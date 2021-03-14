using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_state_idle_minion : Boss2_state_idle
{
    public int type;
    public override void EnterState(Boss2_controller boss)
    {

    }

    public override void Process(Boss2_controller boss)
    {
        boss.thisSpriteRenderer.flipX = boss.player.thisSpriteRenderer.flipX;
        boss.facing = boss.thisSpriteRenderer.flipX;

        if (type==3)
        {
            boss.ChangeState(boss.attack3);
        }

        else if (type==2)
        {
            boss.ChangeState(boss.attack2);
        }
        else
        {
            boss.ChangeState(boss.attack1);
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {

    }
}
