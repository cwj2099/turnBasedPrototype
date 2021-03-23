using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_retreat : Boss2_state
{

        public override void EnterState(Boss2_controller boss)
        {
        boss.thisAnim.Play("Idle");
            boss.chain++;
            boss.turns = 4;
            boss.waitTurns = 1;
            boss.moveTurns = 2;
            if (boss.thisSpriteRenderer.flipX) { boss.speed = -1; } else { boss.speed = 1; }
        }

        public override void Process(Boss2_controller boss)
        {

        if (boss.waitTurns == 0)
        {
            if (boss.moveTurns == 2)
            {
                boss.gameObject.transform.Translate(0, 4 * Time.deltaTime, 0);
            }
            else if (boss.moveTurns == 1)
            {
                boss.gameObject.transform.Translate(0, -4 * Time.deltaTime, 0);
            }
        }
            if (boss.turns <= 0)
            {
                if (boss.player.facing == boss.facing)
                {
                    boss.ChangeState(boss.gameObject.GetComponent<Boss2_state_quickDash>());
                }
                else
                {
                    if (boss.chain < 3)
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
