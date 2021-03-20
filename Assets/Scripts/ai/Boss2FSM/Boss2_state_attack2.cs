using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_attack2 : Boss2_state
{
    public GameObject effect;
    private GameObject ef;
    public override void EnterState(Boss2_controller boss)
    {
        boss.thisAnim.Play("Attack2Pre");
        boss.turns = 12;
        boss.waitTurns = 5;
        boss.moveTurns = 5;
        boss.attackTurns = 5;
        boss.attackRange = -2;
        boss.pushBack = 1;
        if (boss.thisSpriteRenderer.flipX) { boss.speed = 1; } else { boss.speed = -1; }
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.turns == 7&&ef==null)
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
