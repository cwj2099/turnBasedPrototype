//the whole fsm is based on what we learned in class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss2_state
{
    public abstract void EnterState(Boss2_controller boss);
    public abstract void Update(Boss2_controller boss);
    public abstract void LeaveState(Boss2_controller boss);
}
