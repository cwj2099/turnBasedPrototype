using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_attack3 : Boss2_state
{
    public GameObject effect;
    private GameObject ef;
    public override void EnterState(Boss2_controller boss)
    {
        boss.thisAnim.Play("Attack3Pre");
        boss.turns = 8;
        boss.waitTurns = 3;
        boss.moveTurns = 1;
        boss.pushBack = 2;
        boss.invicibleTurns = 2;
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3Pre"))
        {
            if (boss.waitTurns == 1)
            {
                if (boss.thisSpriteRenderer.flipX) { boss.speed = boss.player.position - boss.position; }
                else { boss.speed = boss.player.position - boss.position; }
            }
            if (boss.waitTurns == 0)
            {
                if (boss.transform.position.y < 5) { boss.transform.Translate(0, 50 * Time.deltaTime, 0); }
            }
        }
        if ( boss.turns == 4&&ef == null )
        {
            ef = Instantiate(effect);
            ef.gameObject.GetComponent<SpriteRenderer>().flipX = boss.thisSpriteRenderer.flipX;
        }
        if(ef != null)
        {
            ef.transform.position = boss.gameObject.transform.position;
        }

        if (boss.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3Pro"))
        {

            if (boss.turns == 4)
            {
                //waitTurns = 2;
                boss.attackTurns = 2;
                boss.attackRange = 1;
            }
            if (boss.transform.position.y > 0) { boss.transform.Translate(0, -50 * Time.deltaTime, 0); }
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
