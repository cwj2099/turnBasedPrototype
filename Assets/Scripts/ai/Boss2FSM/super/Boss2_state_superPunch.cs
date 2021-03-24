using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_superPunch : Boss2_state
{
    public GameObject effect;
    private GameObject ef;
    public override void EnterState(Boss2_controller boss)
    {
        boss.thisAnim.Play("SuperPunchPre");
        boss.turns = 10;
        boss.waitTurns = 5;
        boss.attackTurns = 1;
        boss.moveTurns = 1;
        boss.pushBack = 4;
        if (boss.thisSpriteRenderer.flipX) { boss.speed = 2; } else { boss.speed = -2; }
        boss.attackRange = 3;
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.waitTurns > 1)
        {
            boss.thisSpriteRenderer.flipX = boss.player.thisSpriteRenderer.flipX;
            boss.facing = boss.thisSpriteRenderer.flipX;
            if (boss.thisSpriteRenderer.flipX) { boss.speed = 1; } else { boss.speed = -1; }
        }

        if (boss.waitTurns == 0 && ef == null)
        {
            ef = Instantiate(effect);
            ef.gameObject.GetComponent<SpriteRenderer>().flipX = boss.thisSpriteRenderer.flipX;
        }
        if (ef != null)
        {
            ef.transform.position = boss.gameObject.transform.position;
        }

        if (boss.turns == 0)
        {
            boss.ChangeState(boss.idle);
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {

    }
}
