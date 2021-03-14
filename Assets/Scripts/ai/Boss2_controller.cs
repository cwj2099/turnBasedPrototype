using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_controller : BossController
{
    private Boss2_state currentState;
    public Boss2_state_idle idle;
    public Boss2_state_attack1 attack1;
    public Boss2_state_attack2 attack2;
    public Boss2_state_attack3 attack3;
    public void ChangeState(Boss2_state newState)
    {
        if (currentState != null)
        {
            currentState.LeaveState(this);
        }
        currentState = newState;
        if(currentState != null)
        {
            currentState.EnterState(this);
        }
    }
    public void Start()
    {
        timeUnit = GM.timeUnit;
        speedUnit *= moveUnit / timeUnit;
        thisAnim.SetFloat("unit", 1 / timeUnit);
        idle = new Boss2_state_idle();
        attack1 = new Boss2_state_attack1();
        attack2 = new Boss2_state_attack2();
        attack3 = new Boss2_state_attack3();
        currentState = idle;
    }
    public override void Update()
    {
        currentState.Update(this);
    }

}
