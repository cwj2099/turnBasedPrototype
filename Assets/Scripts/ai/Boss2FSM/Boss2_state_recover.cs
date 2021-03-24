using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_recover : Boss2_state
{
    public override void EnterState(Boss2_controller boss)
    {
        boss.thisAnim.Play("Idle");
        boss.chain++;
        boss.turns = 2;
        boss.waitTurns = 1;
        boss.moveTurns = 1;
        boss.invicibleTurns = 1;
        if (boss.position<=-3) { boss.speed = 3; } else { boss.speed = -3; }
    }

    public override void Process(Boss2_controller boss)
    {
        //boss.transform.rotation = Quaternion.Euler(0, 0, boss.transform.rotation.z+360*Time.deltaTime);

        /*if (boss.waitTurns == 0)
        {
            if (boss.moveTurns == 3)
            {
                boss.gameObject.transform.Translate(0, 4 * Time.deltaTime, 0);
            }
            else if (boss.moveTurns == 2)
            {
                boss.gameObject.transform.Translate(0, -4 * Time.deltaTime, 0);
            }
        }*/

        if (boss.turns == 0)
        {
            boss.ChangeState(boss.idle);
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {
        
    }
}
