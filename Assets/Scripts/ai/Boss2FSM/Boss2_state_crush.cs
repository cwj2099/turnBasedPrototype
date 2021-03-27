using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_state_crush : Boss2_state
{
    public override void EnterState(Boss2_controller boss)
    {
        boss.Hp -= 5;
        boss.healthBar.fillAmount = boss.Hp / boss.MaxHp;
        boss.GM.effector.hitStun(1f);
        boss.GM.effector.camZoon();
        Instantiate(boss.breakEffect, boss.transform.position, boss.transform.rotation);
        boss.turns = 10;
        boss.thisAnim.Play("Idle");
        boss.speed = 0;
        boss.attakcing = false;
        boss.invicible = false;
        boss.waitTurns = 0;
        boss.transform.rotation = Quaternion.Euler(0, 0, -45*Mathf.Sign(boss.pushedSpeed));
        boss.transform.position = new Vector3(boss.transform.position.x, -0.5f, boss.transform.position.z);
    }

    public override void Process(Boss2_controller boss)
    {
        if (boss.turns == 0)
        {
            boss.ChangeState(boss.recovery);
        }
    }

    public override void LeaveState(Boss2_controller boss)
    {
        boss.transform.rotation = new Quaternion(0, 0, 0, 0);
        boss.transform.position = new Vector3(boss.transform.position.x, 0, boss.transform.position.z);
    }
}
