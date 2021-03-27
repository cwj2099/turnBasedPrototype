using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_controller : BossController
{
    private Boss2_state currentState;
    public Boss2_state idle;
    public Boss2_state attack1;
    public Boss2_state attack2;
    public Boss2_state attack3;
    public Boss2_state crush;
    public Boss2_state recovery;

    public GameObject breakEffect;
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
        idle = gameObject.GetComponent<Boss2_state_idle>();
        attack1 = gameObject.GetComponent<Boss2_state_attack1>();
        attack2 = gameObject.GetComponent<Boss2_state_attack2>();
        attack3 = gameObject.GetComponent<Boss2_state_attack3>();
        crush = gameObject.GetComponent<Boss2_state_crush>();
        recovery = gameObject.GetComponent<Boss2_state_recover>();
        currentState = idle;
        currentState.EnterState(this);

        Hp = MaxHp;
    }
    public override void Update()
    {
        currentState.Process(this);

    }

    public override void endTurn()
    //每次回合结束执行一次
    {
        //根据被推改变位置
        if (pushedTurns > 0) { 
            position += pushedSpeed;
            if ((position >= 3 || position <= -3)&&currentState!=crush)
            {
                //Debug.Log("Break!");
                ChangeState(crush);
            }
        }
        position = Mathf.Max(-3, position); position = Mathf.Min(3, position);

        //实际位置重定位
        transform.position = new Vector3(moveUnit * position, transform.position.y, transform.position.z);

        turns--; turns = Mathf.Max(0, turns);
        //回合数更新
        if (waitTurns > 0) { waitTurns--; }
        else
        {
            if (moveTurns > 0) { moveTurns--; }
            if (attackTurns > 0) { attackTurns--; }
            if (invicibleTurns > 0) { invicibleTurns--; }
        }
        if (pushedTurns > 0) { pushedTurns--; }
    }

}
